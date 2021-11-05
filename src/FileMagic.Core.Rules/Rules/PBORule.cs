using ldy985.FileMagic.Abstracts;
using Microsoft.Extensions.Logging;

namespace ldy985.FileMagic.Core.Rules.Rules
{
    public class PBORule : BaseRule
    {
        /// <inheritdoc />
        public PBORule(ILogger<PBORule> logger) : base(logger) { }

        /// <inheritdoc />
        public override IMagic Magic { get; } = new Magic("0073726556000000");

        public override ITypeInfo TypeInfo { get; } = new TypeInfo("Bohemia Interactive resource file", "PBO", "EBO");
    }
}