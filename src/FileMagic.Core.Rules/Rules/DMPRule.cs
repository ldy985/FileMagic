using System.IO;
using ldy985.BinaryReaderExtensions;
using ldy985.FileMagic.Abstracts;
using Microsoft.Extensions.Logging;

namespace ldy985.FileMagic.Core.Rules.Rules
{
    public class DMPRule : BaseRule
    {
        /// <inheritdoc />
        public override IMagic Magic { get; } = new Magic("504147454455", 0);

        public override ITypeInfo TypeInfo { get; } = new TypeInfo("Windows Minidump", "DMP");

        /// <inheritdoc />
        protected override bool TryStructureInternal(BinaryReader reader, IResult result)
        {
            reader.SkipForwards(6);
            ushort readUInt16 = reader.ReadUInt16();
            switch (readUInt16)
            {
                case 0x3634: //x64
                case 0x4D32: //x86
                    return true;
                default:
                    return false;
            }
        }

        /// <inheritdoc />
        public DMPRule(ILogger<DMPRule> logger) : base(logger) { }
    }

    public class MDMPRule : BaseRule
    {
        /// <inheritdoc />
        public override IMagic Magic { get; } = new Magic("4D444D5093A7", 0);

        public override ITypeInfo TypeInfo { get; } = new TypeInfo("Windows compressed Minidump", "DMP", "MDMP");

        /// <inheritdoc />
        public MDMPRule(ILogger<MDMPRule> logger) : base(logger) { }
    }
}