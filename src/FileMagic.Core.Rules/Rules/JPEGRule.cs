using ldy985.FileMagic.Abstracts;
using Microsoft.Extensions.Logging;

namespace ldy985.FileMagic.Core.Rules.Rules
{
    /// <summary>
    /// http://www.ecma-international.org/publications/files/ECMA-TR/ECMA%20TR-098.pdf
    /// https://www.fileformat.info/format/jpeg/egff.htm
    /// </summary>
    public class JPEGRule : BaseRule
    {
        /// <inheritdoc />
        public override IMagic Magic { get; } = new Magic("FFD8", 0);

        public override ITypeInfo TypeInfo { get; } = new TypeInfo("Image format", "JPE", "JPEG", "JPG", "JPS");

        /// <inheritdoc />
        public JPEGRule(ILogger<JPEGRule> logger) : base(logger) { }
    }
}