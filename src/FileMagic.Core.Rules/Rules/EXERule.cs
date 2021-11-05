using ldy985.FileMagic.Abstracts;
using Microsoft.Extensions.Logging;

namespace ldy985.FileMagic.Core.Rules.Rules
{
    public class EXERule : BaseRule
    {
        /// <inheritdoc />
        public EXERule(ILogger<EXERule> logger) : base(logger) { }

        /// <inheritdoc />
        public override IMagic Magic { get; } = new Magic("4D5A");

        /// <inheritdoc />
        public override ITypeInfo TypeInfo { get; } = new TypeInfo("MZ Executable code", "EXE", "DLL", "SYS", "WINMD", "XLL");
    }
}