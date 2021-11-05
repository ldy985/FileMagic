//using System;
//using System.IO;
//using Xunit;

//namespace ldy985.FileMagic.Tests
//{
//    public class UnitTest1 : IDisposable
//    {
//        public UnitTest1()
//        {
//            Options options = new Options
//            {
//                ParserCheck = false,
//                StructureCheck = false,
//                ParserHandle = false,
//                PatternCheck = true
//            };
//            _fileMagic = new FileMagic(Microsoft.Extensions.Options.Options.Create(options));

//            string[] files = Directory.GetFiles("./Resources/", "*");
//            _testfiles = new Stream[files.Length];

//            for (int index = 0; index < files.Length; index++)
//            {
//                string file = files[index];
//                _testfiles[index] = new MemoryStream(File.ReadAllBytes(file));
//            }
//        }

//        public void Dispose()
//        {
//            Dispose(true);
//            GC.SuppressFinalize(this);
//        }

//        private readonly FileMagic _fileMagic;
//        private readonly Stream[] _testfiles;

//        private void Dispose(bool disposing)
//        {
//            if (disposing)
//                _fileMagic?.Dispose();
//        }

//        ~UnitTest1()
//        {
//            Dispose(false);
//        }

//        [Fact]
//        public void Test1()
//        {
//            foreach (Stream stream in _testfiles)
//            {
//                bool a = _fileMagic.IdentifyStream(stream, out _);
//            }
//        }
//    }
//}

