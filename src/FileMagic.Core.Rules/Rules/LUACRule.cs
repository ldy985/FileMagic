using ldy985.FileMagic.Abstracts;
using Microsoft.Extensions.Logging;

namespace ldy985.FileMagic.Core.Rules.Rules
{
    public class LUACRule : BaseRule
    {
        /// <inheritdoc />
        public override IMagic Magic { get; } = new Magic("1b4c7561", 0);

        public override ITypeInfo TypeInfo { get; } = new TypeInfo("Lua bytecode", "LUAC");

        /// <inheritdoc />
        public LUACRule(ILogger<LUACRule> logger) : base(logger) { }
    }
}