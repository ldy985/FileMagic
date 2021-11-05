using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using BenchmarkDotNet.Attributes;
using ldy985.FileMagic.Abstracts;
using ldy985.FileMagic.Core.Rules;
using Microsoft.Extensions.Logging.Abstractions;

namespace ldy985.FileMagic.Benchmarks
{
    [MemoryDiagnoser]
    [InProcess]
    public class RuleBenchmark
    {
        private Stream[] _streams = null!;

        [Params(16, 1024, 1024 * 1024)] public int N { get; set; }

        [GlobalSetup]
        public void Setup()
        {
            Random random = new Random(1);

            _streams = new Stream[100];

            for (int i = 0; i < _streams.Length; i++)
            {
                byte[] bytes = new byte[N];
                random.NextBytes(bytes);
                _streams[i] = new MemoryStream(bytes);
            }
        }

        [GlobalCleanup]
        public void Dispose()
        {
            if (_streams == null)
                return;

            foreach (Stream stream in _streams)
                stream.Dispose();
        }

        public IEnumerable<object[]> WorstCaseData()
        {
            var timeSpans = FileMagicRuleHelpers.CreateRules<IRule>(NullLoggerFactory.Instance, typeof(FileMagicBuilderExtensions).Assembly);

            foreach (IRule timeSpan in timeSpans)
            {
                if (!timeSpan.HasMagic)
                    continue;

                var a = Enumerable.Empty<byte>();
                if (timeSpan.Magic!.Offset != 0)
                    a = Enumerable.Repeat((byte)0x1, (int)timeSpan.Magic!.Offset);

                byte[] array = a.Concat(timeSpan.Magic!.MagicBytes.Select(x => x ?? 0)).ToArray();
                MemoryStream st = new MemoryStream(array);

                yield return new object[] { timeSpan, st };
            }
        }

        [Benchmark]
        [ArgumentsSource(nameof(Rules))]
        public bool BestCaseNoMatch(IRule rule)
        {
            Stream stream = _streams[0];
            bool a = false;

            for (int i = 0; i < _streams.Length; i++)
            {
                stream.Position = 0;
                a = rule.TryMagic(stream);
            }

            return a;
        }

        public IEnumerable<object> Rules()
        {
            var timeSpans = FileMagicRuleHelpers.CreateRules<IRule>(NullLoggerFactory.Instance, typeof(FileMagicBuilderExtensions).Assembly);

            foreach (IRule timeSpan in timeSpans)
            {
                if (!timeSpan.HasMagic)
                    continue;

                yield return timeSpan;
            }
        }

        [Benchmark]
        [ArgumentsSource(nameof(WorstCaseData))]
        public bool WorstCaseMatch(IRule rule, Stream st)
        {
            bool a = false;

            for (int i = 0; i < _streams.Length; i++)
            {
                st.Position = 0;
                a = rule.TryMagic(st);
            }

            return a;
        }

        [Benchmark]
        [ArgumentsSource(nameof(Rules))]
        public bool AverageCaseMatch(IRule rule)
        {
            bool a = false;

            foreach (Stream stream in _streams)
            {
                stream.Position = 0;
                a = rule.TryMagic(stream);
            }

            return a;
        }
    }
}