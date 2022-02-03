using ldy985.FileMagic.Abstracts;
using Microsoft.Extensions.Logging;

namespace ldy985.FileMagic.Core.Rules.Rules.Media
{
    /// <summary>
    ///     https://www.loc.gov/preservation/digital/formats/fdd/fdd000188.shtml
    /// </summary>
    public class DNGRule : BaseRule
    {
        /// <inheritdoc />
        public DNGRule(ILogger<DNGRule> logger) : base(logger) { }

        /// <inheritdoc />
        public override IMagic Magic { get; } = new Magic("49492A00080000");

        /// <inheritdoc />
        public override Quality Quality => Quality.High;

        public override ITypeInfo TypeInfo { get; } = new TypeInfo("Digital negative image, subtype of TIFF", "DNG");
    }
}