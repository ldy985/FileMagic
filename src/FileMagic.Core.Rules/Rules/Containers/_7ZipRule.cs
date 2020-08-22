using ldy985.FileMagic.Abstracts;
using Microsoft.Extensions.Logging;

namespace ldy985.FileMagic.Core.Rules.Rules.Containers
{
    public class _7ZipRule : BaseRule
    {
        /// <inheritdoc />
        public _7ZipRule(ILogger<_7ZipRule> logger) : base(logger) { }

        /// <inheritdoc />
        public override IMagic Magic { get; } = new Magic("377ABCAF271C", 0);

        public override ITypeInfo TypeInfo { get; } = new TypeInfo("7-zip compressed file", "7Z");
    }
}