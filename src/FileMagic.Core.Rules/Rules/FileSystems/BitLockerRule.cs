using ldy985.FileMagic.Abstracts;
using Microsoft.Extensions.Logging;

namespace ldy985.FileMagic.Core.Rules.Rules.FileSystems;

/// <summary>
///     https://github.com/libyal/libbde/blob/master/documentation/BitLocker%20Drive%20Encryption%20(BDE)%20format.asciidoc
/// </summary>
public class BitLockerRule : BaseRule
{
    public BitLockerRule(ILogger<BitLockerRule> logger) : base(logger) { }
    public override IMagic Magic { get; } = new Magic("2d4656452d46532d");

    /// <inheritdoc />
    public override Quality Quality => Quality.High;

    public override ITypeInfo TypeInfo { get; } = new TypeInfo("BitLocker encrypted volume");
}