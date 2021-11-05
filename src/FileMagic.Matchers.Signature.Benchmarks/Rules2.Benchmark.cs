using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using BenchmarkDotNet.Attributes;
using ldy985.FileMagic.Abstracts;
using ldy985.FileMagic.Core;
using ldy985.FileMagic.Core.Rules;
using Microsoft.Extensions.Logging.Abstractions;

namespace ldy985.FileMagic.Benchmarks
{
    [MemoryDiagnoser]
    [InProcess]
    public class Rule2Benchmark
    {
        private FileMagic _fileMagic;

        public static IEnumerable<object[]> TestFiles
        {
            get
            {
                var testFiles = new List<object[]>();

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
            bool bestCaseNoMatch = _fileMagic.IdentifyStream(stream, out var result);
            return (bestCaseNoMatch, result);
        }
    }
}