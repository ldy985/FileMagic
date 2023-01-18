using ldy985.FileMagic.Abstracts;
using Microsoft.Extensions.Logging;

namespace ldy985.FileMagic.Core.Rules.Rules.Media;

/// <summary>
/// https://www.w3.org/TR/2022/WD-png-3-20221025/#5PNG-file-signature
/// </summary>
public class PNGRule : BaseRule
{
    /// <inheritdoc />
    public PNGRule(ILogger<PNGRule> logger) : base(logger) { }

    /// <inheritdoc />
    public override IMagic Magic { get; } = new Magic("89504E470D0A1A0A");

    /// <inheritdoc />
    public override Quality Quality => Quality.High;

    public override ITypeInfo TypeInfo { get; } = new TypeInfo("Standard PNG image", "PNG");
}