using System.IO;
using ldy985.FileMagic.Abstracts;
using Microsoft.Extensions.Logging;

namespace ldy985.FileMagic.Core.Rules.Rules.Media
{
    /// <summary>
    /// http://www.multiweb.cz/twoinches/mp3inside.htm#FrameHeaderJ
    /// </summary>
    public class MP3Rule : BaseRule
    {
        /// <inheritdoc />
        public override IMagic Magic { get; } = new Magic("FF", 0);

        public override ITypeInfo TypeInfo { get; } = new TypeInfo("MP3 Audio File", "MP3");

        /// <inheritdoc />
        protected override bool TryStructureInternal(BinaryReader reader, IResult result)
        {
            uint frameHeader = reader.ReadUInt32();

            uint verionId = frameHeader & 0b00000000_00011000_00000000_00000000;
            if (verionId != 0b00000000_00010000_00000000_00000000 && verionId != 0b00000000_00011000_00000000_00000000)
                return false;

            uint layer = frameHeader & 0b00000000_00000110_00000000_00000000;
            if (layer != 0b00000000_00000110_00000000_00000000)
                return false;

            return true;
        }

        /// <inheritdoc />
        public MP3Rule(ILogger<MP3Rule> logger) : base(logger) { }
    }
}