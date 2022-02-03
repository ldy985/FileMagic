using ldy985.FileMagic.Abstracts;
using Microsoft.Extensions.Logging;

namespace ldy985.FileMagic.Core.Rules.Rules;

public class PDBRule : BaseRule
{
    /// <inheritdoc />
    public PDBRule(ILogger<PDBRule> logger) : base(logger) { }

    /// <inheritdoc />
    public override IMagic Magic { get; } = new Magic("4D6963726F736F667420432F432B2B20");

    /// <inheritdoc />
    public override Quality Quality => Quality.VeryHigh;

    public override ITypeInfo TypeInfo { get; } = new TypeInfo("Microsoft C/C++ debugging symbols file", "PDB");
}

public class PDBNetRule : BaseRule
{
    /// <inheritdoc />
    public PDBNetRule(ILogger<PDBNetRule> logger) : base(logger) { }

    /// <inheritdoc />
    public override IMagic Magic { get; } = new Magic("42534A4201000100000000000C0000005044422076312E30");

    /// <inheritdoc />
    public override Quality Quality => Quality.VeryHigh;

    public override ITypeInfo TypeInfo { get; } = new TypeInfo("Microsoft .Net debugging symbols file", "PDB");
}