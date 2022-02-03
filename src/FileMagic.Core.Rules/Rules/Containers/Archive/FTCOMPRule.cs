using ldy985.FileMagic.Abstracts;
using Microsoft.Extensions.Logging;

namespace ldy985.FileMagic.Core.Rules.Rules.Containers.Archive;

/// <summary>
///     https://mailman.astron.com/pipermail/file/2021-April/000497.html
/// </summary>
public class FTCOMPRule : BaseRule
{
    /// <inheritdoc />
    public FTCOMPRule(ILogger<FTCOMPRule> logger) : base(logger) { }

    /// <inheritdoc />
    public override IMagic Magic { get; } = new Magic("A596FDFF");

    /// <inheritdoc />
    public override Quality Quality => Quality.Low;

    public override ITypeInfo TypeInfo { get; } = new TypeInfo("FTCOMP Compressed Archive");
}