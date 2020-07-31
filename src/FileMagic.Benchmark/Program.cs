using System.Diagnostics;
using System.IO;
using BenchmarkDotNet.Attributes;
using BenchmarkDotNet.Running;
using Shared;

namespace FileIdentifier.Benchmark
{
    [MemoryDiagnoser]
    [CoreJob]
    public class IdentificationsModes
    {
        private FileGuesser _patternOnly;
        private FileGuesser _fullIdentifier;
        private Stream[] _testfiles;

        [GlobalSetup]
        public void Setup()
        {
            _fullIdentifier = new FileGuesserBuilder(DetectionOptions.All).AddDefault().Build();
            _patternOnly = new FileGuesserBuilder(DetectionOptions.EnablePatternCheck).AddDefault().Build();

            string[] files = Directory.GetFiles("./Resources/", "*");
            _testfiles = new Stream[files.Length];

            for (int index = 0; index < files.Length; index++)
            {
                string file = files[index];
                _testfiles[index] = new MemoryStream(File.ReadAllBytes(file));
            }
        }

        [GlobalCleanup]
        public void GlobalCleanup()
        {
            foreach (Stream stream in _testfiles)
            {
                stream.Dispose();
            }
        }

        [Benchmark]
        public bool Full()
        {
            var a = false;
            foreach (Stream stream in _testfiles)
            {
                a = _fullIdentifier.IdentifyStream(stream, out var _);
            }

            return a;
        }

        [Benchmark(Baseline = true)]
        public bool Pattern()
        {
            var a = false;
            foreach (Stream stream in _testfiles)
            {
                a = _patternOnly.IdentifyStream(stream, out var _);
            }

            return a;
        }
    }

    public class Program
    {
        public static void Main(string[] args)
        {
            var summary = BenchmarkRunner.Run<IdentificationsModes>();
        }
    }
}