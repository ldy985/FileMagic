using ldy985.FileMagic.Abstracts;
using Microsoft.Extensions.Logging;

namespace ldy985.FileMagic.Core.Rules.Rules
{
    /// <summary>
    /// https://www.loc.gov/preservation/digital/formats/fdd/fdd000188.shtml
    /// </summary>
    public class DNGRule : BaseRule
    {
        /// <inheritdoc />
        public override IMagic Magic { get; } = new Magic("49492A00080000", 0);

        public override ITypeInfo TypeInfo { get; } = new TypeInfo("Digital negative image, subtype of TIFF", "DNG");

        /// <inheritdoc />
        public DNGRule(ILogger<DNGRule> logger) : base(logger) { }
    }
}