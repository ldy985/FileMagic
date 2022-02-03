using ldy985.FileMagic.Abstracts;
using Microsoft.Extensions.Logging;

namespace ldy985.FileMagic.Core.Rules.Rules
{
    public class RTFRule : BaseRule
    {
        /// <inheritdoc />
        public RTFRule(ILogger<RTFRule> logger) : base(logger) { }

        /// <inheritdoc />
        public override IMagic Magic { get; } = new Magic("7B5C72746631");

        /// <inheritdoc />
        public override Quality Quality => Quality.Medium;
        public override ITypeInfo TypeInfo { get; } = new TypeInfo("Rich Text Format File", "RTF", "DOC");
    }
}