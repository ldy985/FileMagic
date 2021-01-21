using ldy985.FileMagic.Core.Extensions;
using ldy985.FileMagic.Core.Rules.Rules;
using ldy985.FileMagic.Core.Rules.Tests.Utils;
using Microsoft.Extensions.Logging.Abstractions;
using Xunit;

namespace ldy985.FileMagic.Core.Rules.Tests
{
    public sealed class SWFRuleTest
    {
        private const string _extension = "swf";
        private readonly SWFRule _rule;

        public SWFRuleTest()
        {
            _rule = new SWFRule(NullLogger<SWFRule>.Instance);
        }

        [Fact]
        public void TestMagic()
        {
            Assert.True(_rule.TryMagic(Utilities.BasePath(0) + _extension));
        }
    }
}