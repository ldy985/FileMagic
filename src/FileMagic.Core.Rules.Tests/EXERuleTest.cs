using ldy985.FileMagic.Core.Extensions;
using ldy985.FileMagic.Core.Rules.Rules;
using Xunit;

namespace ldy985.FileMagic.Core.Rules.Tests;

public sealed class EXERuleTest : RuleTestBase<EXERule>
{
    public EXERuleTest() : base("exe") { }

    public override void TestMagic()
    {
        Assert.True(_rule.TryMagic(BasePath));
    }
}