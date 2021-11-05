using ldy985.FileMagic.Abstracts;
using Microsoft.Extensions.Logging;

namespace ldy985.FileMagic.Core.Rules.Rules
{
    public class DEBRule : BaseRule
    {
        /// <inheritdoc />
        public DEBRule(ILogger<DEBRule> logger) : base(logger) { }

        /// <inheritdoc />
        public override IMagic Magic { get; } = new Magic("213C617263683E");

        public override ITypeInfo TypeInfo { get; } = new TypeInfo("Debian Software Package", "DEB");
    }
}