using System.IO;
using ldy985.BinaryReaderExtensions;
using ldy985.FileMagic.Abstracts;
using Microsoft.Extensions.Logging;

namespace ldy985.FileMagic.Core.Rules.Rules
{
    public class LDBRule : BaseRule
    {
        /// <inheritdoc />
        public LDBRule(ILogger<LDBRule> logger) : base(logger) { }

        public override IMagic? Magic { get; }

        public override ITypeInfo TypeInfo { get; } = new TypeInfo("LevelDB data file", "LDB");

        protected override bool TryStructureInternal(BinaryReader reader, IResult result)
        {
            long length = reader.GetLength();
            if (length < 8)
                return false;

            reader.SetPosition(length - 8);
            return reader.ReadUInt64() == 0xdb4775248b80fb57;
        }
    }
}