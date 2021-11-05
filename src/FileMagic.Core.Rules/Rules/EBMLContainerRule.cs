using System.Collections.Generic;
using System.IO;
using System.Text;
using ldy985.BinaryReaderExtensions;
using ldy985.FileMagic.Abstracts;
using Microsoft.Extensions.Logging;

namespace ldy985.FileMagic.Core.Rules.Rules
{
    /// <summary>
    ///     https://en.wikipedia.org/wiki/Extensible_Binary_Meta_Language
    ///     https://github.com/Matroska-Org/ebml-specification/blob/master/specification.markdown#ebml-element
    ///     https://matroska-org.github.io/libebml/specs.html
    /// </summary>
    public class EBMLContainerRule : BaseRule
    {
        /// <inheritdoc />
        public EBMLContainerRule(ILogger<EBMLContainerRule> logger) : base(logger) { }

        /// <inheritdoc />
        public override IMagic Magic { get; } = new Magic("1A45DFA3");

        public override ITypeInfo TypeInfo { get; } = new TypeInfo("Extensible Binary Meta Language container", "MKV", "WEBM");

        /// <inheritdoc />
        protected override bool TryStructureInternal(BinaryReader reader, IResult result)
        {
            var bytes = new List<byte>();
            for (int i = 0; i < 64; i++)
                if (reader.ReadByte() == 0x42 && reader.ReadByte() == 0x82)
                    goto foundStart;

            return false;

            foundStart:
            reader.SkipForwards(1);

            for (int i = 0; i < 64; i++)
            {
                byte readByte = reader.ReadByte();

                if (readByte != 0x42)
                {
                    bytes.Add(readByte);
                    continue;
                }

                byte b = reader.ReadByte();

                if (b != 0x87)
                {
                    bytes.Add(b);
                    continue;
                }

                goto foundEnd;
            }

            return false;

            foundEnd:

            switch (Encoding.ASCII.GetString(bytes.ToArray()))
            {
                case "webm":
                    result.Extensions = new[] { "WEBM" };
                    break;
                case "matroska\0":
                case "matroska":
                    result.Extensions = new[] { "MKV" };
                    break;
            }

            return true;
        }
    }
}