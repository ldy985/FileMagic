using ldy985.FileMagic.Abstracts;
using Microsoft.Extensions.Logging;

namespace ldy985.FileMagic.Core.Rules.Rules;

/// <summary>
///     http://www.jerrysguide.com/tips/demystify-tvs-file-format.html
/// </summary>
public class TVSRule : BaseRule
{
    public TVSRule(ILogger<TVSRule> logger) : base(logger) { }

    public override IMagic Magic { get; } = new Magic("5456530d0a56657273696f6e09");

    /// <inheritdoc />
    public override Quality Quality => Quality.High;

    public override ITypeInfo TypeInfo { get; } = new TypeInfo("TeamViewer Session", "TVS");
}