using ldy985.FileMagic.Abstracts;
using Microsoft.Extensions.Logging;

namespace ldy985.FileMagic.Core.Rules.Rules
{
    /// <summary>
    /// Windows Precompiled INF File
    /// </summary>
    public class PNFRule : BaseRule
    {
        /// <inheritdoc />
        public override IMagic Magic { get; } = new Magic("01030200??0000", 0);

        /// <inheritdoc />
        public override ITypeInfo TypeInfo { get; } = new TypeInfo("Windows Precompiled INF File", "PNF");

        /// <inheritdoc />
        public PNFRule(ILogger<PNFRule> logger) : base(logger)
        {
        }
    }
}