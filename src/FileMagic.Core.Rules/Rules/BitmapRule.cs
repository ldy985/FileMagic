using System.Diagnostics.CodeAnalysis;
using System.IO;
using ldy985.BinaryReaderExtensions;
using ldy985.FileMagic.Abstracts;
using Microsoft.Extensions.Logging;

namespace ldy985.FileMagic.Core.Rules.Rules
{
    /// <summary>
    /// https://en.wikipedia.org/wiki/BMP_file_format
    /// </summary>
    public class BitmapRule : BaseRule
    {
        /// <inheritdoc />
        public override IMagic Magic { get; } = new Magic("424D", 0);

        /// <inheritdoc />
        public override ITypeInfo TypeInfo { get; } = new TypeInfo("Bitmap", "BMP");

        /// <inheritdoc />
#if NETSTANDARD2_1
        protected override bool TryParseInternal(BinaryReader reader, IResult result, [NotNullWhen(true)]out IParsed? parsed)
#else
        protected override bool TryParseInternal(BinaryReader reader, IResult result, out IParsed? parsed)
#endif
        {
            BMP bmp = new BMP();
            BMPType type = reader.ReadEnum<BMPType>();
            uint size = reader.ReadUInt32();
            reader.SkipForwards(4);
            uint dataAddress = reader.ReadUInt32();
            uint dibSize = reader.Peek(br => br.ReadUInt32());

            bmp.Type = type;
            bmp.Size = size;

            switch (dibSize)
            {
                case 12:
                {
                    BITMAPCOREHEADER readStruct = reader.ReadStruct<BITMAPCOREHEADER>();
                    bmp.Width = readStruct.Width;
                    bmp.Height = readStruct.Height;
                    break;
                }

                case 40:
                {
                    BITMAPINFOHEADER readStruct = reader.ReadStruct<BITMAPINFOHEADER>();
                    bmp.Width = (uint)readStruct.Width;
                    bmp.Height = (uint)readStruct.Height;
                    break;
                }

                case 108:
                {
                    BITMAPV4HEADER readStruct = reader.ReadStruct<BITMAPV4HEADER>();
                    bmp.Width = (uint)readStruct.Width;
                    bmp.Height = (uint)readStruct.Height;
                    break;
                }

                case 124:
                {
                    BITMAPV5HEADER readStruct = reader.ReadStruct<BITMAPV5HEADER>();
                    bmp.Width = (uint)readStruct.Width;
                    bmp.Height = (uint)readStruct.Height;
                    break;
                }

                default:
                {
                    parsed = null!;
                    return false;
                }
            }

            parsed = bmp;
            return true;
        }

        protected override bool TryStructureInternal(BinaryReader reader, IResult result)
        {
            reader.SkipForwards(2);
            uint size = reader.ReadUInt32();
            return size == reader.GetLength();
        }

        /// <summary>
        /// https://docs.microsoft.com/en-us/windows/desktop/api/wingdi/ns-wingdi-tagbitmapcoreheader
        /// </summary>
        private struct BITMAPCOREHEADER
        {
            internal uint Size;
            internal ushort Width;
            internal ushort Height;
        }

        /// <summary>
        /// https://docs.microsoft.com/en-us/previous-versions/dd183376(v%3Dvs.85)
        /// </summary>
        private struct BITMAPINFOHEADER
        {
            internal uint Size;
            internal int Width;
            internal int Height;
        }

        /// <summary>
        /// https://docs.microsoft.com/en-us/windows/desktop/api/wingdi/ns-wingdi-bitmapv4header
        /// </summary>
        private struct BITMAPV4HEADER
        {
            internal uint Size;
            internal int Width;
            internal int Height;
        }

        /// <summary>
        /// https://docs.microsoft.com/en-us/windows/desktop/api/wingdi/ns-wingdi-bitmapv5header
        /// </summary>
        private struct BITMAPV5HEADER
        {
            internal uint Size;
            internal int Width;
            internal int Height;
        }

        public class BMP : IParsed
        {
            public uint Size { get; set; }
            public BMPType Type { get; set; }
            public uint Height { get; set; }
            public uint Width { get; set; }
        }

        public enum BMPType : short
        {
            BM = 0x4d42,
            BA = 0x4142,
            CI = 0x4943,
            CP = 0x5043,
            IC = 0x4349,
            PT = 0x5450
        }

        /// <inheritdoc />
        public BitmapRule(ILogger<BitmapRule> logger) : base(logger) { }
    }
}