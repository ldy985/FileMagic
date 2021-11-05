using System.IO;
using ldy985.BinaryReaderExtensions;
using ldy985.FileMagic.Abstracts;
using Microsoft.Extensions.Logging;

namespace ldy985.FileMagic.Core.Rules.Rules.Containers
{
    public class PAK5Rule : BaseRule
    {
        /// <inheritdoc />
        public PAK5Rule(ILogger<PAK5Rule> logger) : base(logger) { }

        /// <inheritdoc />
        public override IMagic Magic { get; } = new Magic("05000000");

        public override ITypeInfo TypeInfo { get; } = new TypeInfo("Chromium pak files", "PAK", "DATA");

        protected override bool TryStructureInternal(BinaryReader reader, IResult result)
        {
            reader.SkipForwards(8);

            uint resource_count = reader.ReadUInt16();
            reader.SkipForwards(resource_count * 6 + 4);
            uint file_offset = reader.ReadUInt32();

            long length = reader.GetLength();
            if (length != file_offset)
                return false;

            return true;
        }
    }

    public class PAK4Rule : BaseRule
    {
        /// <inheritdoc />
        public PAK4Rule(ILogger<PAK4Rule> logger) : base(logger) { }

        /// <inheritdoc />
        public override IMagic Magic { get; } = new Magic("04000000");

        public override ITypeInfo TypeInfo { get; } = new TypeInfo("Chromium pak files", "PAK", "DATA");

        protected override bool TryStructureInternal(BinaryReader reader, IResult result)
        {
            reader.SkipForwards(4);

            uint num_entries = reader.ReadUInt32();
            reader.SkipForwards(num_entries * 5 + 1);
            uint file_offset = reader.ReadUInt32();

            long length = reader.GetLength();
            if (length != file_offset)
                return false;

            return true;
        }
    }
}