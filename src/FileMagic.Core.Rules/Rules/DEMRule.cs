using ldy985.FileMagic.Abstracts;
using Microsoft.Extensions.Logging;

namespace ldy985.FileMagic.Core.Rules.Rules;

/// <summary>
///     https://github.com/StatsHelix/demoinfo
/// </summary>
public class DEMRule : BaseRule
{
    /// <inheritdoc />
    public DEMRule(ILogger<DEMRule> logger) : base(logger) { }

    /// <inheritdoc />
    public override IMagic Magic { get; } = new Magic("484C3244454D4F");

    /// <inheritdoc />
    public override Quality Quality => Quality.Medium;

    /// <inheritdoc />
    public override ITypeInfo TypeInfo { get; } = new TypeInfo("CS:GO-Demo", "DEM");
}