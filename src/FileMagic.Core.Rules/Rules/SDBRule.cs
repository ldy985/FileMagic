using ldy985.FileMagic.Abstracts;
using Microsoft.Extensions.Logging;

namespace ldy985.FileMagic.Core.Rules.Rules;

/// <summary>
///     https://github.com/AdamGrossTX/FU.WhyAmIBlocked/blob/master/FU.WhyAmIBlocked/Public/Expand-SDB.ps1#L33
///     https://www.asquaredozen.com/2020/07/26/demystifying-windows-10-feature-update-blocks/
///     https://github.com/TheEragon/SdbUnpacker
/// </summary>
public class SDBRule : BaseRule
{
    /// <inheritdoc />
    public SDBRule(ILogger<SDBRule> logger) : base(logger) { }

    /// <inheritdoc />
    public override IMagic Magic { get; } = new Magic("7A646266", 8);

    /// <inheritdoc />
    public override Quality Quality => Quality.High;

    /// <inheritdoc />
    public override ITypeInfo TypeInfo { get; } = new TypeInfo("Windows Shim database format", "SDB");
}