using ldy985.FileMagic.Abstracts;
using Microsoft.Extensions.Logging;
namespace ldy985.FileMagic.Core.Rules.Rules
{
    /// <summary>
    /// https://github.com/google/iconvg
    /// </summary>
    public class IVGRule : BaseRule
    {
        public IVGRule(ILogger<IVGRule> logger) : base(logger) { }

        public override IMagic Magic { get; } = new Magic("89495647");

        public override ITypeInfo TypeInfo { get; } = new TypeInfo("IconVG, Binary SVG", "IVG");
    }
}