using System.ComponentModel.DataAnnotations;
using System.Diagnostics.CodeAnalysis;
using System.IO;
using System.Linq;
using BenchmarkDotNet.Attributes;
using ldy985.FileMagic;
using ldy985.FileMagic.Abstracts;
using ldy985.FileMagic.Core;
using ldy985.FileMagic.Core.Rules.Rules;
using ldy985.FileMagic.Core.Rules.Rules.Media;
using Microsoft.Extensions.Options;

namespace FileMagic.Benchmarks
{
    [MemoryDiagnoser]
    [InProcess]
    [SuppressMessage("Design", "CA1001", MessageId = "Types that own disposable fields should be disposable")]
    [SuppressMessage("IDisposableAnalyzers.Correctness", "IDISP003", MessageId = "Dispose previous before re-assigning.")]
    [SuppressMessage("Security", "CA5394", MessageId = "Do not use insecure randomness")]
    [SuppressMessage("IDisposableAnalyzers.Correctness", "IDISP006", MessageId = "Implement IDisposable.")]
    public class ByteComparison
    {
        private ldy985.FileMagic.FileMagic _fileMagic;

        private Stream _memoryStream;
        private static readonly byte[] _exeHeader = "MZ"u8.ToArray();

        public static string BasePath(uint id)
        {
            return $"../../../../../resources/test{id}.";
        }

        [GlobalSetup]
        public void Setup()
        {
            _memoryStream = File.OpenRead(BasePath(0) + "exe");

            FileMagicConfig fileMagicConfig = new FileMagicConfig();
            fileMagicConfig.ParserCheck = false;
            fileMagicConfig.StructureCheck = false;
            fileMagicConfig.ParserHandle = false;
            fileMagicConfig.PatternCheck = true;

            _fileMagic = new ldy985.FileMagic.FileMagic(Options.Create(fileMagicConfig));
        }

        [GlobalCleanup]
        public void Dispose()
        {
            _fileMagic?.Dispose();
            _memoryStream?.Dispose();
        }

        [Benchmark(Baseline = true)]
        public bool IsPeFile()
        {
            return CheckHeader(_memoryStream, _exeHeader);
        }

        [Benchmark]
        public bool FileTesterDirect()
        {
            return _fileMagic.StreamMatches<EXERule>(_memoryStream, out IResult _);
        }

        [Benchmark]
        public bool FileLen()
        {
            return _memoryStream.Length > 0;
        }

        private static bool CheckHeader(Stream stream, byte[] expectedHeader, byte[]? secondaryExpectedHeader = null)
        {
            //Save the position of the steam in order to reset it back afterward. This is to make sure people that call these methods don't get bugs in their code
            long prevPosition = stream.Position;

            byte[] header = new byte[expectedHeader.Length];
            bool result = stream.Read(header, 0, expectedHeader.Length) > 0 &&
                          (expectedHeader.SequenceEqual(header) || (secondaryExpectedHeader != null && secondaryExpectedHeader.SequenceEqual(header)));

            stream.Position = prevPosition;
            return result;
        }
    }
}