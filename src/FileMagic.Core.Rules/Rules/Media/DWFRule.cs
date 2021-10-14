using ldy985.FileMagic.Abstracts;
using Microsoft.Extensions.Logging;

namespace ldy985.FileMagic.Core.Rules.Rules.Media
{
    public class DWFRule : BaseRule
    {
        /// <inheritdoc />
        public override IMagic Magic { get; } = new Magic("28445746205630??2e????29", 0);

        /// <inheritdoc />
        public override ITypeInfo TypeInfo { get; } = new TypeInfo("Autodesk Design Web Format", "DWF", "DWFX");

        /// <inheritdoc />
        public DWFRule(ILogger<DWFRule> logger) : base(logger)
        {
        }
    }
}