using ldy985.FileMagic.Abstracts;
using Microsoft.Extensions.Logging;

namespace ldy985.FileMagic.Core.Rules.Rules;

/// <summary>
///     https://github.com/file/file/blob/ec05878d72180287d84397819c5e3e127551ce46/magic/Magdir/vmware
/// </summary>
public class NVRAMRule : BaseRule
{
    /// <inheritdoc />
    public NVRAMRule(ILogger<NVRAMRule> logger) : base(logger) { }

    /// <inheritdoc />
    public override IMagic Magic { get; } = new Magic("4D52564E");

    /// <inheritdoc />
    public override Quality Quality => Quality.High;

    public override ITypeInfo TypeInfo { get; } = new TypeInfo("VMware nvram file", "NVRAM");
}