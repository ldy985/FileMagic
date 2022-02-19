using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.IO;
using BenchmarkDotNet.Attributes;
using ldy985.FileMagic.Abstracts;
using ldy985.FileMagic.Core;

namespace ldy985.FileMagic.Benchmarks
{
    [MemoryDiagnoser]
    [InProcess]
    [SuppressMessage("Design", "CA1001", MessageId = "Types that own disposable fields should be disposable")]
    [SuppressMessage("IDisposableAnalyzers.Correctness", "IDISP003", MessageId = "Dispose previous before re-assigning.")]
    [SuppressMessage("Security", "CA5394", MessageId = "Do not use insecure randomness")]
    [SuppressMessage("IDisposableAnalyzers.Correctness", "IDISP006", MessageId = "Implement IDisposable.")]
    public class Rule2Benchmark
    {
        private FileMagic _fileMagic;

        public static IEnumerable<object[]> TestFiles
        {
            get
            {
                List<object[]> testFiles = new List<object[]>();

                string[] files = Directory.GetFiles("../../../../../resources/", "*");

                foreach (string file in files)
                {
                    testFiles.Add(new object[] { Path.GetExtension(file), new MemoryStream(File.ReadAllBytes(file)) });
                }

                return testFiles;
            }
        }

        [GlobalCleanup]
        public void Dispose()
        {
            if (_fileMagic == null)
                return;

            _fileMagic.Dispose();
        }

        [GlobalSetup]
        public void Setup()
        {
            FileMagicConfig fileMagicConfig = new FileMagicConfig()
            {
                ParserCheck = true,
                PatternCheck = true,
                StructureCheck = true
            };

            _fileMagic = new FileMagic(fileMagicConfig);
        }

        [Benchmark]
        [ArgumentsSource(nameof(TestFiles))]
        public (bool bestCaseNoMatch, IResult result) RuleTest(string extension, Stream stream)
        {
            bool bestCaseNoMatch = _fileMagic.IdentifyStream(stream, out IResult result);
            return (bestCaseNoMatch, result);
        }
    }
}