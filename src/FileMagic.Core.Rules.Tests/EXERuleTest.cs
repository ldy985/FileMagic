using ldy985.FileMagic.Core.Rules.Rules;
using ldy985.FileMagic.Core.Rules.Tests.Utils;
using Microsoft.Extensions.Logging.Abstractions;
using Xunit;

namespace ldy985.FileMagic.Core.Rules.Tests
{
    // ReSharper disable once InconsistentNaming
    public sealed class EXERuleTest
    {
        public EXERuleTest()
        {
            _rule = new EXERule(NullLogger<EXERule>.Instance);
        }

        private const string _extension = "exe";
        private readonly EXERule _rule;

        [Fact]
        public void TestMagic()
        {
            Assert.True(_rule.MatchMagic(Utilities.BasePath(0) + _extension));
        }
    }
}