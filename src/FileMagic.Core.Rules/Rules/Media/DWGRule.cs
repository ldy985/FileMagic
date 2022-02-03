using ldy985.FileMagic.Abstracts;
using Microsoft.Extensions.Logging;

namespace ldy985.FileMagic.Core.Rules.Rules.Media;

public class DWGRule : BaseRule
{
    /// <inheritdoc />
    public DWGRule(ILogger<DWGRule> logger) : base(logger) { }

    /// <inheritdoc />
    public override IMagic Magic { get; } = new Magic("41433130????0000000000");

    /// <inheritdoc />
    public override Quality Quality => Quality.High;

    public override ITypeInfo TypeInfo { get; } = new TypeInfo("Generic AutoCAD drawing file", "DWG");
}