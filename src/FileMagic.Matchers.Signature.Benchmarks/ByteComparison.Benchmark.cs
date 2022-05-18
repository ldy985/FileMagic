using System.Diagnostics.CodeAnalysis;
using System.IO;
using BenchmarkDotNet.Attributes;
using ldy985.FileMagic;
using ldy985.FileMagic.Abstracts;
using ldy985.FileMagic.Core;
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

        public static string BasePath(uint id)
        {
            return $"../../../../../resources/test{id}.";
        }

        [GlobalSetup]
        public void Setup()
        {
            _memoryStream = new FileStream(BasePath(0) + "bmp", FileMode.Open);

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

        [Benchmark]
        public bool FileTesterDirect()
        {
            return _fileMagic.StreamMatches<BitmapRule>(_memoryStream, out IResult _);
        }
    }
}