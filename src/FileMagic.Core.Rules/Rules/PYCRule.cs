using ldy985.FileMagic.Abstracts;
using Microsoft.Extensions.Logging;

namespace ldy985.FileMagic.Core.Rules.Rules
{
    public class PYCRule : BaseRule
    {
        /// <inheritdoc />
        public override IMagic Magic { get; } = new Magic("????0D0A????????630000000000000000", 0);

        public override ITypeInfo TypeInfo { get; } = new TypeInfo("Python bytecode", "PYC");

        /// <inheritdoc />
        public PYCRule(ILogger<PYCRule> logger) : base(logger) { }
    }
}