using ldy985.FileMagic.Abstracts;
using Microsoft.Extensions.Logging;

namespace ldy985.FileMagic.Core.Rules.Rules.Media;

public class TIF_LERule : BaseRule
{
    /// <inheritdoc />
    public TIF_LERule(ILogger<TIF_LERule> logger) : base(logger) { }

    /// <inheritdoc />
    public override IMagic Magic { get; } = new Magic("49492A00");

    /// <inheritdoc />
    public override Quality Quality => Quality.Medium;

    public override ITypeInfo TypeInfo { get; } = new TypeInfo("Tif image format", "TIF");
}

public class TIF_BERule : BaseRule
{
    /// <inheritdoc />
    public TIF_BERule(ILogger<TIF_BERule> logger) : base(logger) { }

    /// <inheritdoc />
    public override IMagic Magic { get; } = new Magic("4D4D002A");

    /// <inheritdoc />
    public override Quality Quality => Quality.Medium;

    public override ITypeInfo TypeInfo { get; } = new TypeInfo("Tif image format", "TIF");
}