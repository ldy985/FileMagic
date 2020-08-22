using ldy985.FileMagic.Abstracts;
using Microsoft.Extensions.Logging;

namespace ldy985.FileMagic.Core.Rules.Rules
{
    /// <summary>
    /// https://www.iana.org/assignments/media-types/application/pdf
    /// </summary>
    public class PDFRule : BaseRule
    {
        /// <inheritdoc />
        public override IMagic Magic { get; } = new Magic("255044462d", 0);

        public override ITypeInfo TypeInfo { get; } = new TypeInfo("Adobe Portable Document Format, Forms Document Format, and Illustrator graphics files", "PDF", "FDF", "AI");

        /// <inheritdoc />
        public PDFRule(ILogger<PDFRule> logger) : base(logger) { }
    }
}