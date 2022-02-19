using System;
using System.Collections.Generic;
using System.IO;
using ldy985.FileMagic.Abstracts;
using ldy985.FileMagic.Core;
using Microsoft.Extensions.Options;
using Xunit;

namespace ldy985.FileMagic.Tests
{
    public sealed class FullMatchTest : IDisposable
    {
        private readonly FileMagic _fileMagic;

        public FullMatchTest()
        {
            FileMagicConfig fileMagicConfig = new FileMagicConfig();
            fileMagicConfig.ParserCheck = true;
            fileMagicConfig.StructureCheck = true;
            fileMagicConfig.ParserHandle = false;
            fileMagicConfig.PatternCheck = true;
            _fileMagic = new FileMagic(Options.Create(fileMagicConfig));
        }

        public static IEnumerable<object[]> TestFiles
        {
            get
            {
                List<object[]> testFiles = new List<object[]>();

                string[] files = Directory.GetFiles("../../../../../resources/", "*");

                foreach (string file in files)
                {
                    TestFile testFile = new TestFile(file);
                    testFiles.Add(new object[] { testFile });
                }

                return testFiles;
            }
        }

        public void Dispose()
        {
            _fileMagic.Dispose();
        }

        /// <summary>DetectAllCorrectly</summary>
        /// <param name="testFile"></param>
        [Theory]
        [MemberData(nameof(TestFiles))]
        public void DetectAllCorrectly(TestFile testFile)
        {
            string filePath = testFile.Path;
            Assert.True(_fileMagic.IdentifyFile(filePath, out IResult? result), filePath);
            Assert.Contains(testFile.GetExtension(), result!.Extensions!, StringComparer.Ordinal);
        }
    }
}