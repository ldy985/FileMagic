using ldy985.FileMagic.Abstracts;
using Microsoft.Extensions.Logging;

namespace ldy985.FileMagic.Core.Rules.Rules.Media
{
    public class PNGRule : BaseRule
    {
        /// <inheritdoc />
        public override IMagic Magic { get; } = new Magic("89504E470D0A1A0A0000000D49484452", 0);

        public override ITypeInfo TypeInfo { get; } = new TypeInfo("Standard PNG image", "PNG");

        /// <inheritdoc />
        public PNGRule(ILogger<PNGRule> logger) : base(logger) { }
    }
}