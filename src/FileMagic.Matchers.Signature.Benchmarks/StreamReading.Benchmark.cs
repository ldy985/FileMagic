using System;
using System.Diagnostics.CodeAnalysis;
using System.IO;
using System.Runtime.InteropServices;
using System.Text;
using BenchmarkDotNet.Attributes;
using Microsoft.Toolkit.HighPerformance;

namespace ldy985.FileMagic.Benchmarks
{
    [MemoryDiagnoser]
    [InProcess]
    [SuppressMessage("Design", "CA1001", MessageId = "Types that own disposable fields should be disposable")]
    [SuppressMessage("IDisposableAnalyzers.Correctness", "IDISP003", MessageId = "Dispose previous before re-assigning.")]
    [SuppressMessage("Security", "CA5394", MessageId = "Do not use insecure randomness")]
    [SuppressMessage("IDisposableAnalyzers.Correctness", "IDISP006", MessageId = "Implement IDisposable.")]
    public class StreamReading
    {
        private Stream _memoryStream;

        [Params(512, 1024 * 10, 1024 * 1024 * 10)]
        public int N { get; set; }

        [GlobalSetup]
        public void Setup()
        {
            Random random = new Random(1);
            byte[] bytes = new byte[N];
            random.NextBytes(bytes);
            _memoryStream = new MemoryStream(bytes);
        }

        [GlobalCleanup]
        public void Dispose()
        {
            _memoryStream?.Dispose();
        }

        [Benchmark]
        public int StreamPerfByte()
        {
            _memoryStream.Position = 0;
            int b = 0;
            for (int i = 0; i < N; i++)
                b = _memoryStream.Read<byte>();

            return b;
        }

        [Benchmark]
        public int StreamByte()
        {
            _memoryStream.Position = 0;
            int b = 0;
            for (int i = 0; i < N; i++)
                b = _memoryStream.ReadByte();

            return b;
        }

        [Benchmark]
        public int BinaryReaderByte()
        {
            int b;
            _memoryStream.Position = 0;

            using (BinaryReader binaryReader = new BinaryReader(_memoryStream, Encoding.Default, true))
            {
                b = 0;
                for (int i = 0; i < N; i++)
                    b = binaryReader.ReadByte();
            }

            return b;
        }

        [Benchmark]
        public long StreamPerfLong()
        {
            _memoryStream.Position = 0;
            long b = 0;
            for (int i = 0; i < N / sizeof(long); i++)
                b = _memoryStream.Read<long>();

            return b;
        }

        [Benchmark]
        public long StreamLong()
        {
            _memoryStream.Position = 0;
            long b = 0;
            Span<byte> span = stackalloc byte[8];

            for (int i = 0; i < N / sizeof(long); i++)
            {
                _memoryStream.Read(span);
                b = MemoryMarshal.Read<long>(span);
            }

            return b;
        }

        [Benchmark]
        public long BinaryReaderLong()
        {
            long b;
            _memoryStream.Position = 0;

            using (BinaryReader binaryReader = new BinaryReader(_memoryStream, Encoding.Default, true))
            {
                b = 0;
                for (int i = 0; i < N / sizeof(long); i++)
                    b = binaryReader.ReadInt64();
            }

            return b;
        }
    }
}