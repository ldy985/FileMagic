using ldy985.FileMagic.Abstracts;
using Microsoft.Extensions.Logging;

namespace ldy985.FileMagic.Core.Rules.Rules
{
    public class PSDRule : BaseRule
    {
        /// <inheritdoc />
        public override IMagic Magic { get; } = new Magic("38425053", 0);

        public override ITypeInfo TypeInfo { get; } = new TypeInfo("Photoshop image file", "PSD", "PDD");

        /// <inheritdoc />
        public PSDRule(ILogger<PSDRule> logger) : base(logger) { }
    }
}