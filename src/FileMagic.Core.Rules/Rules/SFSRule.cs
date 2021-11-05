using ldy985.FileMagic.Abstracts;
using Microsoft.Extensions.Logging;

namespace ldy985.FileMagic.Core.Rules.Rules
{
    /// <summary>
    ///     https://dr-emann.github.io/squashfs/
    /// </summary>
    public class SFSRule : BaseRule
    {
        /// <inheritdoc />
        public SFSRule(ILogger<SFSRule> logger) : base(logger) { }

        /// <inheritdoc />
        public override IMagic Magic { get; } = new Magic("68737173");

        public override ITypeInfo TypeInfo { get; } = new TypeInfo("Squashfs Binary Format", "SFS");
    }
}