using ldy985.FileMagic.Abstracts;
using Microsoft.Extensions.Logging;

namespace ldy985.FileMagic.Core.Rules.Rules.Containers
{
    public class BZIPRule : BaseRule
    {
        /// <inheritdoc />
        public override IMagic Magic { get; } = new Magic("425A30", 0);

        public override ITypeInfo TypeInfo { get; } = new TypeInfo("bzip compressed file", "BZ");

        /// <inheritdoc />
        public BZIPRule(ILogger<BZIPRule> logger) : base(logger) { }
    }

    public class BZIP2Rule : BaseRule
    {
        /// <inheritdoc />
        public override IMagic Magic { get; } = new Magic("425A68", 0);

        public override ITypeInfo TypeInfo { get; } = new TypeInfo("bzip2 compressed file", "BZ2");

        /// <inheritdoc />
        public BZIP2Rule(ILogger<BZIP2Rule> logger) : base(logger) { }
    }
}