//using System.IO;
//using BenchmarkDotNet.Attributes;
//using ldy985.FileMagic.Core;
//using ldy985.FileMagic.Matchers.Signature.Benchmarks.Helpers;
//using ldy985.FileMagic.Matchers.Signature.Simple;
//using ldy985.FileMagic.Matchers.Signature.Trie;
//using Microsoft.Extensions.Logging.Abstractions;
//using Shared.Interfaces;

//namespace ldy985.FileMagic.Matchers.Signature.Benchmarks
//{
//    [MemoryDiagnoser]
//    [InProcess]
//    public class ByteComparison
//    {
//        private MemoryStream _memoryStream;

//        private TestRule _testRule;
//        private byte[] _buffer;
//        private ISingleRuleMatcher _simpleByteTester;
//        private BinaryReader _binaryReader;

//        [GlobalSetup]
//        public void Setup()
//        {
//            _buffer = new byte[] { 0x0, 0x0, 0x0, 0x0, 0x0, 0x1, 0x2 };
//            _memoryStream = new MemoryStream(_buffer);
//            _binaryReader = new BinaryReader(_memoryStream);

//            _simpleByteTester = new SimpleSignatureMatcher();

//            _testRule = new TestRule(new NullLogger<TestRule>());
//        }

//        [GlobalCleanup]
//        public void Dispose()
//        {
//            _binaryReader?.Dispose();
//            _memoryStream?.Dispose();
//        }

//        /// <summary>
//        /// Simple
//        /// </summary>
//        /// <returns></returns>
//        /// <exception cref="System.ObjectDisposedException">Ignore.</exception>
//        [Benchmark(Baseline = true)]
//        public bool Simple()
//        {
//            foreach (byte b in _buffer)
//            {
//                byte readByte = (byte)_memoryStream.ReadByte();
//                if (readByte != b)
//                    return false;
//            }

//            return true;
//        }

//        [Benchmark]
//        public bool FileTesterDirect()
//        {
//            return _simpleByteTester.TestRule(_binaryReader, _testRule);
//        }
//    }
//}