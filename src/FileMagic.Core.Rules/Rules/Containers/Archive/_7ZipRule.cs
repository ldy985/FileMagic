using ldy985.BinaryReaderExtensions;
using ldy985.FileMagic.Abstracts;
using Microsoft.Extensions.Logging;

namespace ldy985.FileMagic.Core.Rules.Rules.Containers.Archive
{
    public class _7ZipRule : BaseRule
    {
        /// <inheritdoc />
        public _7ZipRule(ILogger<_7ZipRule> logger) : base(logger) { }

        /// <inheritdoc />
        public override IMagic Magic { get; } = new Magic("377ABCAF271C");

        /// <inheritdoc />
        public override Quality Quality => Quality.High;

        public override ITypeInfo TypeInfo { get; } = new TypeInfo("7-zip compressed file", "7Z");

        protected override bool TryStructureInternal(BinaryReader reader, IResult result)
        {
            return reader.GetLength() >= 64;
        }
    }
}