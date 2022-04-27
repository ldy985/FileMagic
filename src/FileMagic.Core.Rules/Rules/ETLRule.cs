using ldy985.FileMagic.Abstracts;
using Microsoft.Extensions.Logging;

namespace ldy985.FileMagic.Core.Rules.Rules;

public class ETLRule : BaseRule
{
    /// <inheritdoc />
    public ETLRule(ILogger<ETLRule> logger) : base(logger) { }

    /// <inheritdoc />
    public override IMagic Magic { get; } = new Magic("C4FFFFFF", 176);

    /// <inheritdoc />
    public override Quality Quality => Quality.Medium;

    /// <inheritdoc />
    public override ITypeInfo TypeInfo { get; } = new TypeInfo("Microsoft binary tracelog", "ETL");
}