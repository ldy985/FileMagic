using ldy985.FileMagic.Abstracts;
using Microsoft.Extensions.Logging;

namespace ldy985.FileMagic.Core.Rules.Rules
{
    public class LNKRule : BaseRule
    {
        /// <inheritdoc />
        public override IMagic Magic { get; } = new Magic("4c0000000114020000000000c000000000000046", 0);

        public override ITypeInfo TypeInfo { get; } = new TypeInfo("Windows shortcut", "LNK");

        /// <inheritdoc />
        public LNKRule(ILogger<LNKRule> logger) : base(logger) { }
    }
}