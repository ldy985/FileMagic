using System.Collections.Generic;
using System.Linq;
using ldy985.FileMagic.Abstracts;
using ldy985.FileMagic.Core.Rules.Rules;
using Microsoft.Extensions.Logging.Abstractions;
using Xunit;

namespace ldy985.FileMagic.Core.Rules.Tests;

public sealed class RuleHelperTest
{
    [Fact]
    public void TestMagic()
    {
        var rules = FileMagicRuleHelpers.CreateRules<IRule>(NullLoggerFactory.Instance, typeof(FileMagicRuleHelpers).Assembly).ToList();

        Assert.True(rules.Count > 0);

        EXERule exeRule = FileMagicRuleHelpers.CreateRule<EXERule>();

        IRule[] defaultFileMagicRules = FileMagicRuleHelpers.GetDefaultFileMagicRules(NullLoggerFactory.Instance);
        Assert.True(defaultFileMagicRules.Length > 0);
    }
}