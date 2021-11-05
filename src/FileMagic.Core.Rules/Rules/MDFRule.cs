using ldy985.FileMagic.Abstracts;
using Microsoft.Extensions.Logging;

namespace ldy985.FileMagic.Core.Rules.Rules
{
    public class MDFRule : BaseRule
    {
        /// <inheritdoc />
        public MDFRule(ILogger<MDFRule> logger) : base(logger) { }

        /// <inheritdoc />
        public override IMagic Magic { get; } = new Magic("010F0000");

        public override ITypeInfo TypeInfo { get; } = new TypeInfo("MSSQL database", "MDF");
    }
}