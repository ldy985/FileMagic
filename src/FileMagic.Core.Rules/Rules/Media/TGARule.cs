using System.IO;
using System.Text;
using ldy985.BinaryReaderExtensions;
using ldy985.FileMagic.Abstracts;
using Microsoft.Extensions.Logging;

namespace ldy985.FileMagic.Core.Rules.Rules
{
    /// <summary>
    /// http://www.paulbourke.net/dataformats/tga/
    /// https://www.fileformat.info/format/tga/egff.htm
    /// http://www.opennet.ru/docs/formats/targa.pdf
    /// https://www.dca.fee.unicamp.br/~martino/disciplinas/ea978/tgaffs.pdf
    /// </summary>
    public class TGARule : BaseRule
    {
        public override IMagic? Magic { get; }

        /// <inheritdoc />
        public override ITypeInfo TypeInfo { get; } = new TypeInfo("TGA image file", "TGA");

        /// <inheritdoc />
        public TGARule(ILogger<TGARule> logger) : base(logger)
        {
        }

        protected override bool TryStructureInternal(BinaryReader reader, IResult result)
        {
            long position = reader.GetPosition();
            if (!reader.TrySetPosition(reader.GetLength() - 18))
                return false;

            //Check for v2 optional footer
            var readFixedString = reader.ReadFixedString(17, Encoding.ASCII);
            if (readFixedString == "TRUEVISION-XFILE.")
                return true;

            if (!reader.TrySetPosition(position + 2))
                return false;
            
            byte colorMap = reader.ReadByte();
            if (colorMap > 2)
                return false;

            byte dataType = reader.ReadByte();
            switch (dataType)
            {
                // case 0: //this is useless eg. empty image
                case 1:
                case 2:
                case 3:
                case 9:
                case 10:
                case 11:
                case 32:
                case 33:
                    return true;
                default:
                    return false;
            }
        }
    }
}