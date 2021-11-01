using System.IO;
using BenchmarkDotNet.Attributes;
using ldy985.FileMagic.Abstracts;
using ldy985.FileMagic.Core;
using ldy985.FileMagic.Core.Rules.Rules.Media;

namespace ldy985.FileMagic.Benchmarks
{
    [MemoryDiagnoser]
    [InProcess]
    public class ByteComparison
    {
        public static string BasePath(uint id) => $"../../../../../resources/test{id}.";

        private Stream _memoryStream;

        private FileMagic _fileMagic;

        [GlobalSetup]
        public void Setup()
        {
            _memoryStream = new FileStream(BasePath(0) + "bmp", FileMode.Open);

            FileMagicConfig fileMagicConfig = new FileMagicConfig();
            fileMagicConfig.ParserCheck = false;
            fileMagicConfig.StructureCheck = false;
            fileMagicConfig.ParserHandle = false;
            fileMagicConfig.PatternCheck = true;

            _fileMagic = new FileMagic(Microsoft.Extensions.Options.Options.Create(fileMagicConfig));
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