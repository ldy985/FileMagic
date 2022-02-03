using ldy985.FileMagic.Abstracts;
using Microsoft.Extensions.Logging;

namespace ldy985.FileMagic.Core.Rules.Rules.Containers.Archive;

public class RAR5Rule : BaseRule
{
    /// <inheritdoc />
    public RAR5Rule(ILogger<RAR5Rule> logger) : base(logger) { }

    /// <inheritdoc />
    public override IMagic Magic { get; } = new Magic("526172211A070100");

    /// <inheritdoc />
    public override Quality Quality => Quality.High;

    public override ITypeInfo TypeInfo { get; } = new TypeInfo("RAR5 compressed file", "RAR");
}