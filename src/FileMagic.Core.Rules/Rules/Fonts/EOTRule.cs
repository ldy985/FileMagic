using ldy985.FileMagic.Abstracts;
using Microsoft.Extensions.Logging;

namespace ldy985.FileMagic.Core.Rules.Rules.Fonts
{
    /// <summary>
    /// https://www.w3.org/Submission/EOT/
    /// </summary>
    public class EOTRule : BaseRule
    {
        public EOTRule(ILogger<EOTRule> logger) : base(logger)
        {
        }

        public override IMagic Magic { get; } = new Magic("4c50", 34);

        public override ITypeInfo TypeInfo { get; } = new TypeInfo("Embedded OpenType File Format", "EOT");
    }
}