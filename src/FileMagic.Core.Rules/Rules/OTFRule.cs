using ldy985.FileMagic.Abstracts;
using Microsoft.Extensions.Logging;

namespace ldy985.FileMagic.Core.Rules.Rules
{
    /// <summary>
    /// https://docs.microsoft.com/en-us/typography/opentype/spec/otff
    /// </summary>
    public class OTFRule : BaseRule
    {
        /// <inheritdoc />
        public override IMagic Magic { get; } = new Magic("4F54544F00", 0);

        public override ITypeInfo TypeInfo { get; } = new TypeInfo("OpenType Font", "OTF");

        /// <inheritdoc />
        public OTFRule(ILogger<OTFRule> logger) : base(logger) { }
    }
}