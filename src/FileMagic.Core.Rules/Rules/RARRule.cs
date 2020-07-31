using ldy985.FileMagic.Abstracts;
using Microsoft.Extensions.Logging;

namespace ldy985.FileMagic.Core.Rules.Rules
{
    public class RARRule : BaseRule
    {
        /// <inheritdoc />
        public override IMagic Magic { get; } = new Magic("526172211A0700", 0);

        public override ITypeInfo TypeInfo { get; } = new TypeInfo("RAR compressed file", "RAR");

        /// <inheritdoc />
        public RARRule(ILogger<RARRule> logger) : base(logger) { }
    }
}