using ldy985.FileMagic.Abstracts;
using Microsoft.Extensions.Logging;

namespace ldy985.FileMagic.Core.Rules.Rules.Media
{
    public class FLACRule : BaseRule
    {
        /// <inheritdoc />
        public FLACRule(ILogger<FLACRule> logger) : base(logger) { }

        /// <inheritdoc />
        public override IMagic Magic { get; } = new Magic("664C614300000022");

        public override ITypeInfo TypeInfo { get; } = new TypeInfo("Free Lossless Audio Codec file", "FLAC");
    }
}