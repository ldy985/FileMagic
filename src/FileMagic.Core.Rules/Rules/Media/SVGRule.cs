using ldy985.FileMagic.Abstracts;
using Microsoft.Extensions.Logging;

namespace ldy985.FileMagic.Core.Rules.Rules
{
    public class SVGRule : BaseRule
    {
        /// <inheritdoc />
        public override IMagic Magic { get; } = new Magic("3C73766720786D6C6E73", 0);

        public override ITypeInfo TypeInfo { get; } = new TypeInfo("SVG vector graphics", "SVG");

        /// <inheritdoc />
        public SVGRule(ILogger<SVGRule> logger) : base(logger)
        {
        }
    }

    public class SVGBOMRule : BaseRule
    {
        /// <inheritdoc />
        public override IMagic Magic { get; } = new Magic("EFBBBF3C73766720786D6C6E73", 0);

        public override ITypeInfo TypeInfo { get; } = new TypeInfo("SVG vector graphics with BOM", "SVG");

        /// <inheritdoc />
        public SVGBOMRule(ILogger<SVGBOMRule> logger) : base(logger)
        {
        }
    }
}