using ldy985.FileMagic.Abstracts;
using Microsoft.Extensions.Logging;

namespace ldy985.FileMagic.Core.Rules.Rules
{
    public class RDPCacheRule : BaseRule
    {
        /// <inheritdoc />
        public override IMagic Magic { get; } = new Magic("52445038626D7000", 0);

        public override ITypeInfo TypeInfo { get; } = new TypeInfo("Windows Remote desktop bitmap cache", "BIN");

        /// <inheritdoc />
        public RDPCacheRule(ILogger<RDPCacheRule> logger) : base(logger) { }
    }
}