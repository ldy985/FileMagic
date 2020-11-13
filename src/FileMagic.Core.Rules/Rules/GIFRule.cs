#if NETSTANDARD2_1
using System.Diagnostics.CodeAnalysis;
#endif
using System.IO;
using ldy985.BinaryReaderExtensions;
using ldy985.FileMagic.Abstracts;
using Microsoft.Extensions.Logging;

namespace ldy985.FileMagic.Core.Rules.Rules
{
    public class GIFRule : BaseRule
    {
        /// <inheritdoc />
        public override IMagic Magic { get; } = new Magic("474946", 0);

        /// <inheritdoc />
        public override ITypeInfo TypeInfo { get; } = new TypeInfo("Gif image", "GIF");

        /// <inheritdoc />
#if NETSTANDARD2_1
        protected override bool TryParseInternal(BinaryReader reader, IResult result, [NotNullWhen(true)] out IParsed? parsed)
#else
        protected override bool TryParseInternal(BinaryReader reader, IResult result, out IParsed? parsed)
#endif
        {
            reader.SkipForwards(6);
            GIF gif = new GIF();
            gif.Width = reader.ReadUInt16();
            gif.Height = reader.ReadUInt16();

            parsed = gif;
            return true;
        }

        public class GIF : IParsed
        {
            public ushort Height { get; set; }
            public ushort Width { get; set; }
        }

        protected override bool TryStructureInternal(BinaryReader reader, IResult result)
        {
            reader.SkipForwards(3);
            ushort type = reader.ReadUInt16();
            byte a = reader.ReadByte();

            switch (type)
            {
                case 14136 when a == 97: //87a
                case 14648 when a == 97: //89a
                    return true;
                default:
                    return false;
            }
        }

        /// <inheritdoc />
        public GIFRule(ILogger<GIFRule> logger) : base(logger) { }
    }
}