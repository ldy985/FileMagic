using ldy985.FileMagic.Abstracts;
using Microsoft.Extensions.Logging;

namespace ldy985.FileMagic.Core.Rules.Rules
{
    public class MOBIRule : BaseRule
    {
        /// <inheritdoc />
        public override IMagic Magic { get; } = new Magic("424F4F4B4D4F42490000", 60);

        public override ITypeInfo TypeInfo { get; } = new TypeInfo("E-Book format", "MOBI");

        /// <inheritdoc />
        public MOBIRule(ILogger<MOBIRule> logger) : base(logger) { }
    }
}