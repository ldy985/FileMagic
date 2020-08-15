using System.IO;
using ldy985.FileMagic.Core.Rules.Rules;
using ldy985.FileMagic.Core.Rules.Tests.Utils;
using Microsoft.Extensions.Logging.Abstractions;
using Xunit;

namespace ldy985.FileMagic.Core.Rules.Tests
{
    public sealed class BitmapRuleTest
    {
        private const string _extension = "bmp";
        private readonly BitmapRule _rule;

        public BitmapRuleTest()
        {
            _rule = new BitmapRule(NullLogger<BitmapRule>.Instance);
        }

        [Fact]
        public void TestMagic()
        {
            Assert.True(_rule.MatchMagic(Utilities.BasePath(0) + _extension));
        }

        [Fact]
        public void TestStructure()
        {
            Result result = new Result();
            using (BinaryReader binaryReader = Utilities.GetReader(Utilities.BasePath(0) + _extension))
            {
                bool match = _rule.TryStructure(binaryReader, result);
                Assert.True(match);
            }
        }

        [Fact]
        public void TestParsing()
        {
            Result result = new Result();
            using (BinaryReader binaryReader = Utilities.GetReader(Utilities.BasePath(0) + _extension))
            {
                bool match = _rule.TryParse(binaryReader, result, out var parsedObject);
                Assert.True(match);

                BitmapRule.BMP bmp = (BitmapRule.BMP)parsedObject;

                Assert.Equal(BitmapRule.BMPType.BM, bmp.Type);
                Assert.Equal(73926u, bmp.Size);
                Assert.Equal(114u, bmp.Height);
                Assert.Equal(215u, bmp.Width);
            }
        }
    }
}