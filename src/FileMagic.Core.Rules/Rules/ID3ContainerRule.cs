using ldy985.FileMagic.Abstracts;
using Microsoft.Extensions.Logging;

namespace ldy985.FileMagic.Core.Rules.Rules
{
    /// <summary>
    ///     https://mutagen-specs.readthedocs.io/en/latest/id3/id3v2.4.0-structure.html
    /// </summary>
    public class ID3ContainerRule : BaseRule
    {
        /// <inheritdoc />
        public ID3ContainerRule(ILogger<ID3ContainerRule> logger) : base(logger) { }

        /// <inheritdoc />
        public override IMagic Magic { get; } = new Magic("494433");

        public override ITypeInfo TypeInfo { get; } = new TypeInfo("ID3 container", "MP3", "AAC", "MP2");
    }
}