using ldy985.FileMagic.Abstracts;
using Microsoft.Extensions.Logging;

namespace ldy985.FileMagic.Core.Rules.Rules;

/// <summary>
///     https://www.iana.org/assignments/media-types/application/vnd.fuzzysheet
/// </summary>
public class FZSRule : BaseRule
{
    /// <inheritdoc />
    public FZSRule(ILogger<FZSRule> logger) : base(logger) { }

    /// <inheritdoc />
    public override IMagic Magic { get; } = new Magic("467A5300");

    /// <inheritdoc />
    public override Quality Quality => Quality.Medium;

    public override ITypeInfo TypeInfo { get; } = new TypeInfo("Common Interchange of non-precise information", "FZS");
}