using System.Diagnostics.CodeAnalysis;
using System.IO;
using ldy985.BinaryReaderExtensions;
using ldy985.FileMagic.Abstracts;
using Microsoft.Extensions.Logging;

namespace ldy985.FileMagic.Core.Rules.Rules
{
    /// <summary>
    /// https://www.adobe.com/content/dam/acom/en/devnet/pdf/swf-file-format-spec.pdf
    /// </summary>
    public class SWFRule : BaseRule
    {
        /// <inheritdoc />
        public override IMagic Magic { get; } = new Magic("??5753", 0);

        public override ITypeInfo TypeInfo { get; } = new TypeInfo("Shockwave Flash Movie", "SWF");

        /// <inheritdoc />
        protected override bool TryStructureInternal([NotNull] BinaryReader reader, IResult result)
        {
            reader.SkipForwards(4);
            uint fileSize = reader.ReadUInt32();
            return fileSize == reader.GetLength();
        }

        /// <inheritdoc />
        public SWFRule(ILogger<SWFRule> logger) : base(logger) { }
    }
}