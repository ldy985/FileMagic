using ldy985.FileMagic.Abstracts;
using Microsoft.Extensions.Logging;

namespace ldy985.FileMagic.Core.Rules.Rules
{
    public class PSDRule : BaseRule
    {
        /// <inheritdoc />
        public PSDRule(ILogger<PSDRule> logger) : base(logger) { }

        /// <inheritdoc />
        public override IMagic Magic { get; } = new Magic("38425053");

        public override ITypeInfo TypeInfo { get; } = new TypeInfo("Photoshop image file", "PSD", "PDD");
    }
}