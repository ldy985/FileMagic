using ldy985.FileMagic.Abstracts;
using Microsoft.Extensions.Logging;

namespace ldy985.FileMagic.Core.Rules.Rules
{
    public class PDBRule : BaseRule
    {
        /// <inheritdoc />
        public override IMagic Magic { get; } = new Magic("4D6963726F736F667420432F432B2B20", 0);

        public override ITypeInfo TypeInfo { get; } = new TypeInfo("Microsoft C/C++ debugging symbols file", "PDB");

        /// <inheritdoc />
        public PDBRule(ILogger<PDBRule> logger) : base(logger) { }
    }

    public class PDBNetRule : BaseRule
    {
        /// <inheritdoc />
        public override IMagic Magic { get; } = new Magic("42534A4201000100000000000C0000005044422076312E30", 0);

        public override ITypeInfo TypeInfo { get; } = new TypeInfo("Microsoft .Net debugging symbols file", "PDB");

        /// <inheritdoc />
        public PDBNetRule(ILogger<PDBNetRule> logger) : base(logger) { }
    }
}