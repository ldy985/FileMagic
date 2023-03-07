using System.IO;
using JetBrains.Annotations;
using ldy985.FileMagic.Abstracts;
using ldy985.FileMagic.Core.Rules.Tests.Utils;
using Xunit;

namespace ldy985.FileMagic.Core.Rules.Tests;

[UsedImplicitly(ImplicitUseTargetFlags.WithInheritors)]
public abstract class RuleTestBase<T> where T : class, IRule
{
    protected readonly T _rule;

    protected RuleTestBase(string extension)
    {
        _rule = FileMagicRuleHelpers.CreateRule<T>();
        Extension = extension;
    }

    protected string Extension { get; }

    protected string BasePath => Utilities.BasePath(0) + Extension;

    [Fact]
    public virtual void TestMagic() { }

    [Fact]
    public virtual void TestStructure() { }

    [Fact]
    public virtual void TestParsing() { }

    protected BinaryReader GetBinaryReader()
    {
        return Utilities.GetReader(BasePath);
    }
}