using System.IO;
using System.Runtime.InteropServices;
using ldy985.BinaryReaderExtensions;
using ldy985.FileMagic.Abstracts;
using ldy985.NumberExtensions;
using Microsoft.Extensions.Logging;

namespace ldy985.FileMagic.Core.Rules.Rules
{
    /// <summary>
    ///     https://docs.microsoft.com/da-dk/typography/opentype/spec/otff
    ///     https://fontforge.org/docs/techref/TrueOpenTables.html
    /// </summary>
    public class TTFRule : BaseRule
    {
        /// <inheritdoc />
        public TTFRule(ILogger<TTFRule> logger) : base(logger) { }

        /// <inheritdoc />
        public override IMagic? Magic { get; }

        /// <inheritdoc />
        public override ITypeInfo TypeInfo { get; } = new TypeInfo("True type font", "OTF", "TTF", "OTC", "TTC");

        protected override bool TryStructureInternal(BinaryReader reader, IResult result)
        {
            unsafe
            {
                long length = reader.GetLength();
                if (length < sizeof(OffsetTable))
                    return false;

                OffsetTable offsetTable = reader.ReadStruct<OffsetTable>();

                ushort offsetTableNumTables = offsetTable.numTables.Reverse();

                if (length < offsetTableNumTables * sizeof(DirTableEntry))
                    return false;

                for (int i = 0; i < offsetTableNumTables; i++)
                {
                    DirTableEntry dirTableEntry = reader.ReadStruct<DirTableEntry>();

                    switch (dirTableEntry.tag)
                    {
                        case 0x45534142: //BASE
                        case 0x54444245: //EBDT
                        case 0x434C4245: //EBLC
                        case 0x43534245: //EBSC
                        case 0x4D544646: //FFTM
                        case 0x46454447: //GDEF
                        case 0x534F5047: //GPOS
                        case 0x42555347: //GSUB
                        case 0x4854414D: //MATH
                        case 0x322F534F: //OS/2
                        case 0x64456650: //PfEd
                        case 0x6670615A: //Zapf
                        case 0x746E6361: //acnt
                        case 0x726B6E61: //ankr
                        case 0x72617661: //avar
                        case 0x74616462: //bdat
                        case 0x64656862: //bhed
                        case 0x636F6C62: //bloc
                        case 0x6E6C7362: //bsln
                        case 0x70616D63: //cmap
                        case 0x72617663: //cvar
                        case 0x63736466: //fdsc
                        case 0x74616566: //feat
                        case 0x78746D66: //fmtx
                        case 0x646E6F66: //fond
                        case 0x6D677066: //fpgm
                        case 0x72617666: //fvar
                        case 0x70736167: //gasp
                        case 0x66796C67: //glyf
                        case 0x72617667: //gvar
                        case 0x786D6468: //hdmx
                        case 0x64616568: //head
                        case 0x61656868: //hhea
                        case 0x78746D68: //hmtx
                        case 0x7473756A: //just
                        case 0x6E72656B: //kern
                        case 0x7872656B: //kerx
                        case 0x7261636C: //lcar
                        case 0x61636F6C: //loca
                        case 0x6761746C: //ltag
                        case 0x7078616D: //maxp
                        case 0x6174656D: //meta
                        case 0x74726F6D: //mort
                        case 0x78726F6D: //morx
                        case 0x656D616E: //name
                        case 0x6462706F: //opbd
                        case 0x74736F70: //post
                        case 0x70657270: //prep
                        case 0x706F7270: //prop
                        case 0x78696273: //sbix
                        case 0x6B617274: //trak
                        case 0x61656876: //vhea
                        case 0x78746D76: //vmtx
                        case 0x66657278: //xref
                            return true;
                    }
                }

                return false;
            }
        }

        [StructLayout(LayoutKind.Sequential)]
        private readonly struct OffsetTable
        {
            internal readonly uint sfntVersion;
            internal readonly ushort numTables;
            internal readonly ushort searchRange;
            internal readonly ushort entrySelector;
            internal readonly ushort rangeShift;
        }

        [StructLayout(LayoutKind.Sequential)]
        private readonly struct DirTableEntry
        {
            internal readonly uint tag;
            internal readonly uint checksum;
            internal readonly uint offset;
            internal readonly uint length;
        }
    }
}