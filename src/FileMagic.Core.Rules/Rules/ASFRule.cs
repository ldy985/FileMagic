using ldy985.FileMagic.Abstracts;
using Microsoft.Extensions.Logging;

namespace ldy985.FileMagic.Core.Rules.Rules
{
    public class ASFRule : BaseRule
    {
        /// <inheritdoc />
        public ASFRule(ILogger<ASFRule> logger) : base(logger) { }

        /// <inheritdoc />
        public override IMagic Magic { get; } = new Magic("3026B2758E66CF11A6D900AA0062CE6C");

        /// <inheritdoc />
        public override Quality Quality => Quality.High;

        /// <inheritdoc />
        public override ITypeInfo TypeInfo { get; } = new TypeInfo("Advanced Systems Format", "ASF");
    }
}