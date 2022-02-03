using ldy985.FileMagic.Abstracts;
using Microsoft.Extensions.Logging;

namespace ldy985.FileMagic.Core.Rules.Rules.Fonts
{
    /// <summary>
    ///     https://www.w3.org/TR/WOFF/#WOFFHeader
    /// </summary>
    public class WOFFRule : BaseRule
    {
        public WOFFRule(ILogger<WOFFRule> logger) : base(logger) { }

        public override IMagic Magic { get; } = new Magic("774F4646");

        /// <inheritdoc />
        public override Quality Quality => Quality.Medium;

        public override ITypeInfo TypeInfo { get; } = new TypeInfo("WOFF font packaging", "WOFF");
    }
}