using ldy985.FileMagic.Abstracts;
using Microsoft.Extensions.Logging;

namespace ldy985.FileMagic.Core.Rules.Rules
{
    public class CABRule : BaseRule
    {
        /// <inheritdoc />
        public override IMagic Magic { get; } = new Magic("4D534346", 0);

        public override ITypeInfo TypeInfo { get; } = new TypeInfo("Microsoft Cabinet file", "CAB");

        /// <inheritdoc />
        public CABRule(ILogger<CABRule> logger) : base(logger) { }
    }
}