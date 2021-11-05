using ldy985.FileMagic.Abstracts;
using Microsoft.Extensions.Logging;

namespace ldy985.FileMagic.Core.Rules.Rules.Fonts
{
    /// <summary>
    ///     https://www.w3.org/TR/WOFF2/#woff20Header
    /// </summary>
    public class WOFF2Rule : BaseRule
    {
        public WOFF2Rule(ILogger<WOFF2Rule> logger) : base(logger) { }

        public override IMagic Magic { get; } = new Magic("774F4632");

        public override ITypeInfo TypeInfo { get; } = new TypeInfo("WOFF2 font packaging", "WOFF2");
    }
}