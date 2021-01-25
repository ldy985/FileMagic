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
        protected override bool TryParseInternal(BinaryReader reader, IResult result, [NotNullWhen(true)]out IParsed? parsed)
        {
            Bmp bmp = new Bmp();
            BmpType type = reader.ReadEnum<BmpType>();
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
                    Bitmapcoreheader readStruct = reader.ReadStruct<Bitmapcoreheader>();
                    bmp.Width = readStruct.Width;
                    bmp.Height = readStruct.Height;
                    break;
                }

                case 40:
                {
                    Bitmapinfoheader readStruct = reader.ReadStruct<Bitmapinfoheader>();
                    bmp.Width = (uint)readStruct.Width;
                    bmp.Height = (uint)readStruct.Height;
                    break;
                }

                case 108:
                {
                    Bitmapv4Header readStruct = reader.ReadStruct<Bitmapv4Header>();
                    bmp.Width = (uint)readStruct.Width;
                    bmp.Height = (uint)readStruct.Height;
                    break;
                }

                case 124:
                {
                    Bitmapv5Header readStruct = reader.ReadStruct<Bitmapv5Header>();
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

        protected override bool TryStructureInternal([NotNull] BinaryReader reader, IResult result)
        {
            reader.SkipForwards(2);
            uint size = reader.ReadUInt32();
            return size == reader.GetLength();
        }

        /// <summary>
        /// https://docs.microsoft.com/en-us/windows/desktop/api/wingdi/ns-wingdi-tagbitmapcoreheader
        /// </summary>
        private struct Bitmapcoreheader
        {
            internal uint Size;
            internal ushort Width;
            internal ushort Height;
        }

        /// <summary>
        /// https://docs.microsoft.com/en-us/previous-versions/dd183376(v%3Dvs.85)
        /// </summary>
        private struct Bitmapinfoheader
        {
            internal uint Size;
            internal int Width;
            internal int Height;
        }

        /// <summary>
        /// https://docs.microsoft.com/en-us/windows/desktop/api/wingdi/ns-wingdi-bitmapv4header
        /// </summary>
        private struct Bitmapv4Header
        {
            internal uint Size;
            internal int Width;
            internal int Height;
        }

        /// <summary>
        /// https://docs.microsoft.com/en-us/windows/desktop/api/wingdi/ns-wingdi-bitmapv5header
        /// </summary>
        private struct Bitmapv5Header
        {
            internal uint Size;
            internal int Width;
            internal int Height;
        }

        public class Bmp : IParsed
        {
            public uint Size { get; set; }
            public BmpType Type { get; set; }
            public uint Height { get; set; }
            public uint Width { get; set; }
        }

        public enum BmpType : short
        {
            Bm = 0x4d42,
            Ba = 0x4142,
            Ci = 0x4943,
            Cp = 0x5043,
            Ic = 0x4349,
            Pt = 0x5450
        }

        /// <inheritdoc />
        public BitmapRule(ILogger<BitmapRule> logger) : base(logger) { }
    }
}