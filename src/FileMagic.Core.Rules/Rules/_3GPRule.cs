using ldy985.FileMagic.Abstracts;
using Microsoft.Extensions.Logging;

namespace ldy985.FileMagic.Core.Rules.Rules
{
    /// <summary>
    /// https://www.etsi.org/deliver/etsi_ts/126200_126299/126244/07.03.00_60/ts_126244v070300p.pdf
    /// </summary>
    public class _3GPRule : BaseRule
    {
        /// <inheritdoc />
        public override IMagic Magic { get; } = new Magic("336770", 0);

        /// <inheritdoc />
        public override ITypeInfo TypeInfo { get; } = new TypeInfo("3GP media container", "AMR", "3GP");

        /// <inheritdoc />
        public _3GPRule(ILogger<_3GPRule> logger) : base(logger) { }
    }
}