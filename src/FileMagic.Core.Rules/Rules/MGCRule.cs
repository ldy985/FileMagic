using ldy985.FileMagic.Abstracts;
using Microsoft.Extensions.Logging;

namespace ldy985.FileMagic.Core.Rules.Rules;

/// <summary>
///     https://github.com/file/file/blob/ec05878d72180287d84397819c5e3e127551ce46/magic/Magdir/magic
/// </summary>
public class MGCRule : BaseRule
{
    /// <inheritdoc />
    public MGCRule(ILogger<MGCRule> logger) : base(logger) { }

    /// <inheritdoc />
    public override IMagic Magic { get; } = new Magic("1C041EF1");

    /// <inheritdoc />
    public override Quality Quality => Quality.Medium;

    /// <inheritdoc />
    public override ITypeInfo TypeInfo { get; } = new TypeInfo("Magic binary file for file(1) cmd", "MGC");
}