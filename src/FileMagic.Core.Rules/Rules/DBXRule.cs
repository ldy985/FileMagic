using ldy985.FileMagic.Abstracts;
using Microsoft.Extensions.Logging;

namespace ldy985.FileMagic.Core.Rules.Rules
{
    public class DBXRule : BaseRule
    {
        /// <inheritdoc />
        public DBXRule(ILogger<DBXRule> logger) : base(logger) { }

        /// <inheritdoc />
        public override IMagic Magic { get; } = new Magic("CFAD12FE");

        public override Quality Quality => Quality.Medium;

        public override ITypeInfo TypeInfo { get; } = new TypeInfo("MS Outlook Express DBX file", "DBX");
    }
}