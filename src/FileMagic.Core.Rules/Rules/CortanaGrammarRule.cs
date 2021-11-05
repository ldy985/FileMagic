using ldy985.FileMagic.Abstracts;
using Microsoft.Extensions.Logging;

namespace ldy985.FileMagic.Core.Rules.Rules
{
    public class CortanaGrammarRule : BaseRule
    {
        /// <inheritdoc />
        public CortanaGrammarRule(ILogger<CortanaGrammarRule> logger) : base(logger) { }

        /// <inheritdoc />
        public override IMagic Magic { get; } = new Magic("424C41481F8B0800000000000400");

        public override ITypeInfo TypeInfo { get; } = new TypeInfo("Cortana undocumented compressed grammar", "GZ");
    }
}