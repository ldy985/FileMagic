using ldy985.BinaryReaderExtensions;
using ldy985.FileMagic.Abstracts;
using Microsoft.Extensions.Logging;

namespace ldy985.FileMagic.Core.Rules.Rules.Containers.Archive
{
    public class TARRule : BaseRule
    {
        /// <inheritdoc />
        public TARRule(ILogger<TARRule> logger) : base(logger) { }

        /// <inheritdoc />
        public override IMagic? Magic { get; } //= new Magic("757374617200", 257); //Too much overhead

        public override ITypeInfo TypeInfo { get; } = new TypeInfo("TAR file format", "TAR");

        /// <inheritdoc />
        public override Quality Quality => Quality.Low;

        protected override bool TryStructureInternal(BinaryReader reader, IResult result)
        {
            reader.SkipForwards(257);
            long readInt64 = reader.ReadInt64();
            return (readInt64 & 0x0000FFFFFFFFFFFF) == 0x7261747375;
        }
    }
}