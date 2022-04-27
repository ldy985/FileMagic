using ldy985.FileMagic.Abstracts;
using Microsoft.Extensions.Logging;

namespace ldy985.FileMagic.Core.Rules.Rules;

public class PCKGDEPRule : BaseRule
{
    /// <inheritdoc />
    public PCKGDEPRule(ILogger<PCKGDEPRule> logger) : base(logger) { }

    /// <inheritdoc />
    public override IMagic Magic { get; } = new Magic("4152493800000000");

    /// <inheritdoc />
    public override Quality Quality => Quality.High;

    public override ITypeInfo TypeInfo { get; } = new TypeInfo("Microsoft Windows Package Deployment Format", "PCKGDEP");
}