using ldy985.FileMagic.Abstracts;
using Microsoft.Extensions.Logging;

namespace ldy985.FileMagic.Core.Rules.Rules;

/// <summary>
///     https://fileinfo.com/extension/cov
/// </summary>
public class COVRule : BaseRule
{
    /// <inheritdoc />
    public COVRule(ILogger<COVRule> logger) : base(logger) { }

    /// <inheritdoc />
    public override IMagic Magic { get; } = new Magic("464158434F5645522D56455230");

    /// <inheritdoc />
    public override Quality Quality => Quality.High;

    /// <inheritdoc />
    public override ITypeInfo TypeInfo { get; } = new TypeInfo("Fax Cover Page File", "COV");
}