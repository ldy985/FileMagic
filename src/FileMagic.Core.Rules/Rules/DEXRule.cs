using ldy985.FileMagic.Abstracts;
using Microsoft.Extensions.Logging;

namespace ldy985.FileMagic.Core.Rules.Rules
{
    public class DEXRule : BaseRule
    {
        /// <inheritdoc />
        public override IMagic Magic { get; } = new Magic("6465780A30333500", 0);

        public override ITypeInfo TypeInfo { get; } = new TypeInfo("Dalvik Executable Format", "DEX");

        /// <inheritdoc />
        public DEXRule(ILogger<DEXRule> logger) : base(logger) { }
    }
}