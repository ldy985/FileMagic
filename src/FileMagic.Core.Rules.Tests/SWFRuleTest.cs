using ldy985.FileMagic.Core.Rules.Rules;
using ldy985.FileMagic.Core.Rules.Tests.Utils;
using Microsoft.Extensions.Logging.Abstractions;
using Xunit;

namespace ldy985.FileMagic.Core.Rules.Tests
{
    public sealed class SWFRuleTest
    {
        public SWFRuleTest()
        {
            _rule = new SWFRule(NullLogger<SWFRule>.Instance);
        }

        private const string _extension = "swf";
        private readonly SWFRule _rule;

        [Fact]
        public void TestMagic()
        {
            Assert.True(_rule.MatchMagic(Utilities.BasePath(0) + _extension));
        }
    }
}