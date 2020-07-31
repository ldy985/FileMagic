using ldy985.FileMagic.Abstracts;
using Microsoft.Extensions.Logging;

namespace ldy985.FileMagic.Core.Rules.Rules
{
    public class ELFRule : BaseRule
    {
        /// <inheritdoc />
        public override IMagic Magic { get; } = new Magic("7F454C46", 0);

        /// <inheritdoc />
        public override ITypeInfo TypeInfo { get; } = new TypeInfo("ELF Executable code", "SO", "");

        /// <inheritdoc />
        public ELFRule(ILogger<ELFRule> logger) : base(logger) { }
    }
}