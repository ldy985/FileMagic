using ldy985.FileMagic.Abstracts;
using Microsoft.Extensions.Logging;

namespace ldy985.FileMagic.Core.Rules.Rules
{
    /// <summary>
    /// https://docs.microsoft.com/en-us/previous-versions/bb417345(v=msdn.10)
    /// Windows Update Binary Delta Compression
    /// </summary>
    public class MSPatchRule : BaseRule
    {
        /// <inheritdoc />
        public override IMagic Magic { get; } = new Magic("50413330", 4);

        public override ITypeInfo TypeInfo { get; } = new TypeInfo("Microsoft delta patch data");

        /// <inheritdoc />
        public MSPatchRule(ILogger<MSPatchRule> logger) : base(logger) { }
    }
}