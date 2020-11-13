using ldy985.FileMagic.Abstracts;
using Microsoft.Extensions.Logging;

namespace ldy985.FileMagic.Core.Rules.Rules
{
    public class TypeLibRule : BaseRule
    {
        /// <inheritdoc />
        public override IMagic Magic { get; } = new Magic("4D53465402000100", 0);

        public override ITypeInfo TypeInfo { get; } = new TypeInfo("Visual C++ type library file", "TLB", "OLE", "SPSS");

        /// <inheritdoc />
        public TypeLibRule(ILogger<TypeLibRule> logger) : base(logger) { }
    }
}