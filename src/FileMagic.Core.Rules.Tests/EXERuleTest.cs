using ldy985.FileMagic.Core.Extensions;
using ldy985.FileMagic.Core.Rules.Rules;
using ldy985.FileMagic.Core.Rules.Tests.Utils;
using Microsoft.Extensions.Logging.Abstractions;
using Xunit;

namespace ldy985.FileMagic.Core.Rules.Tests
{
    public sealed class EXERuleTest
    {
        private const string Extension = "exe";
        private readonly EXERule _rule;

        public EXERuleTest()
        {
            _rule = FileMagicRuleHelpers.CreateRule<EXERule>(NullLoggerFactory.Instance);
        }

        [Fact]
        public void TestMagic()
        {
            Assert.True(_rule.TryMagic(Utilities.BasePath(0) + Extension));
        }
    }
}