using ldy985.FileMagic.Abstracts;
using Microsoft.Extensions.Logging;

namespace ldy985.FileMagic.Core.Rules.Rules.Media;

public class SVGRule : BaseRule
{
    /// <inheritdoc />
    public SVGRule(ILogger<SVGRule> logger) : base(logger) { }

    /// <inheritdoc />
    public override IMagic Magic { get; } = new Magic("3C73766720");

    /// <inheritdoc />
    public override Quality Quality => Quality.Medium;

    public override ITypeInfo TypeInfo { get; } = new TypeInfo("SVG vector graphics", "SVG");
}

public class SVGBOMRule : BaseRule
{
    /// <inheritdoc />
    public SVGBOMRule(ILogger<SVGBOMRule> logger) : base(logger) { }

    /// <inheritdoc />
    public override IMagic Magic { get; } = new Magic("EFBBBF3C73766720");

    /// <inheritdoc />
    public override Quality Quality => Quality.Medium;

    public override ITypeInfo TypeInfo { get; } = new TypeInfo("SVG vector graphics with BOM", "SVG");
}