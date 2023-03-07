using System;
using System.IO;
using ldy985.FileMagic.Abstracts;
using ldy985.FileMagic.Core.Extensions;
using ldy985.FileMagic.Core.Rules.Rules.Media;
using Xunit;

namespace ldy985.FileMagic.Core.Rules.Tests;

public sealed class BitmapRuleTest : RuleTestBase<BitmapRule>
{
    public BitmapRuleTest() : base("bmp") { }

    public override void TestMagic()
    {
        Assert.True(_rule.TryMagic(BasePath));
    }

    public override void TestStructure()
    {
        IResult result = new Result();

        using (BinaryReader binaryReader = GetBinaryReader())
        {
            bool match = _rule.TryStructure(binaryReader, ref result);
            Assert.True(match);
        }
    }

    public override void TestParsing()
    {
        IResult result = new Result();

        using (BinaryReader binaryReader = GetBinaryReader())
        {
            if (_rule.TryParse(binaryReader, ref result, out IParsed? parsedObject))
            {
                BitmapRule.Bmp bmp = (BitmapRule.Bmp)parsedObject;

                Assert.Equal(BitmapRule.BmpType.Bm, bmp.Type);
                Assert.Equal(73926u, bmp.Size);
                Assert.Equal(114u, bmp.Height);
                Assert.Equal(215u, bmp.Width);
            }
            else
            {
                throw new ArgumentException("Didn't succeed");
            }
        }
    }
}