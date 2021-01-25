using ldy985.FileMagic.Abstracts;
using Microsoft.Extensions.Logging;

namespace ldy985.FileMagic.Core.Rules.Rules
{
    public class ATFRule : BaseRule
    {
        /// <inheritdoc />
        public override IMagic Magic { get; } = new Magic("415446000002FF02", 0);

        public override ITypeInfo TypeInfo { get; } = new TypeInfo("Unknown atf file", "ATF");

        /// <inheritdoc />
        public ATFRule(ILogger<ATFRule> logger) : base(logger) { }
    }
}