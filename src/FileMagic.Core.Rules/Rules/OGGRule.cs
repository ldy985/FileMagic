using ldy985.FileMagic.Abstracts;
using Microsoft.Extensions.Logging;

namespace ldy985.FileMagic.Core.Rules.Rules
{
    /// <summary>
    /// https://www.xiph.org/ogg/doc/framing.html
    /// </summary>
    public class OGGRule : BaseRule
    {
        /// <inheritdoc />
        public override IMagic Magic { get; } = new Magic("4F676753", 0);

        public override ITypeInfo TypeInfo { get; } = new TypeInfo("Ogg Vorbis Audio File", "OGG", "OGX");

        /// <inheritdoc />
        public OGGRule(ILogger<OGGRule> logger) : base(logger) { }
    }
}