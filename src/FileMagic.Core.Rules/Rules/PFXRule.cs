using ldy985.FileMagic.Abstracts;
using Microsoft.Extensions.Logging;

namespace ldy985.FileMagic.Core.Rules.Rules
{
    public class PFXRule : BaseRule
    {
        /// <inheritdoc />
        public override IMagic Magic { get; } = new Magic("3082????02010330", 0);

        public override ITypeInfo TypeInfo { get; } = new TypeInfo("PFX certificate format", "PFX");

        /// <inheritdoc />
        public PFXRule(ILogger<PFXRule> logger) : base(logger) { }
    }
}