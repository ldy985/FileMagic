using ldy985.FileMagic.Abstracts;
using Microsoft.Extensions.Logging;

namespace ldy985.FileMagic.Core.Rules.Rules
{
    public class EVTXRule : BaseRule
    {
        /// <inheritdoc />
        public override IMagic Magic { get; } = new Magic("456c6646696c6500", 0);

        public override ITypeInfo TypeInfo { get; } = new TypeInfo("Vista Event Log", "EVTX");

        /// <inheritdoc />
        public EVTXRule(ILogger<EVTXRule> logger) : base(logger) { }
    }
}