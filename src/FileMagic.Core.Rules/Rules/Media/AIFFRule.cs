using ldy985.FileMagic.Abstracts;
using Microsoft.Extensions.Logging;

namespace ldy985.FileMagic.Core.Rules.Rules.Media
{
    /// <summary>
    ///     https://www.loc.gov/preservation/digital/formats/fdd/fdd000005.shtml
    ///     https://en.wikipedia.org/wiki/Audio_Interchange_File_Format
    /// </summary>
    public class AIFFRule : BaseRule
    {
        /// <inheritdoc />
        public AIFFRule(ILogger<AIFFRule> logger) : base(logger) { }

        /// <inheritdoc />
        public override IMagic Magic { get; } = new Magic("336770");

        /// <inheritdoc />
        public override ITypeInfo TypeInfo { get; } = new TypeInfo("Audio Interchange File Format", "AIFF", "AIF", "AIFC");
    }
}