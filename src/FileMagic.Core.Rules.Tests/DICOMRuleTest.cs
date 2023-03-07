using ldy985.FileMagic.Core.Extensions;
using ldy985.FileMagic.Core.Rules.Rules.Media;
using Xunit;

namespace ldy985.FileMagic.Core.Rules.Tests;

public sealed class DICOMRuleTest : RuleTestBase<DICOMRule>
{
    public DICOMRuleTest() : base("dcm") { }

    public override void TestMagic()
    {
        Assert.True(_rule.TryMagic(BasePath));
    }
}