using ldy985.FileMagic.Abstracts;
using Microsoft.Extensions.Logging;

namespace ldy985.FileMagic.Core.Rules.Rules;

/// <summary>
///     https://keepass.info/help/base/repair.html
/// </summary>
public class KDBXRule : BaseRule
{
    /// <inheritdoc />
    public KDBXRule(ILogger<KDBXRule> logger) : base(logger) { }

    /// <inheritdoc />
    public override IMagic Magic { get; } = new Magic("03D9A29A67FB4BB5");

    /// <inheritdoc />
    public override Quality Quality => Quality.High;

    public override ITypeInfo TypeInfo { get; } = new TypeInfo("KeePass 2.x database", "KDBX");
}