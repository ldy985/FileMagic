using ldy985.FileMagic.Abstracts;
using Microsoft.Extensions.Logging;

namespace ldy985.FileMagic.Core.Rules.Rules
{
    public class FLVRule : BaseRule
    {
        /// <inheritdoc />
        public FLVRule(ILogger<FLVRule> logger) : base(logger) { }

        /// <inheritdoc />
        public override IMagic Magic { get; } = new Magic("464C5601??0000000900000000");

        public override ITypeInfo TypeInfo { get; } = new TypeInfo("Adobe flash video format", "FLV");
    }
}