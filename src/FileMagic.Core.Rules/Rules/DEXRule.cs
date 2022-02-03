using ldy985.FileMagic.Abstracts;
using Microsoft.Extensions.Logging;

namespace ldy985.FileMagic.Core.Rules.Rules;

public class DEXRule : BaseRule
{
    /// <inheritdoc />
    public DEXRule(ILogger<DEXRule> logger) : base(logger) { }

    /// <inheritdoc />
    public override IMagic Magic { get; } = new Magic("6465780A30333500");

    /// <inheritdoc />
    public override Quality Quality => Quality.Medium;

    public override ITypeInfo TypeInfo { get; } = new TypeInfo("Dalvik Executable Format", "DEX");
}