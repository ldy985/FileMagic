using ldy985.FileMagic.Abstracts;
using Microsoft.Extensions.Logging;

namespace ldy985.FileMagic.Core.Rules.Rules.Containers.Archive;

public class RARRule : BaseRule
{
    /// <inheritdoc />
    public RARRule(ILogger<RARRule> logger) : base(logger) { }

    /// <inheritdoc />
    public override IMagic Magic { get; } = new Magic("526172211A0700");

    /// <inheritdoc />
    public override Quality Quality => Quality.High;

    public override ITypeInfo TypeInfo { get; } = new TypeInfo("RAR compressed file", "RAR");
}