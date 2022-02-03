using ldy985.BinaryReaderExtensions;
using ldy985.FileMagic.Abstracts;
using Microsoft.Extensions.Logging;

namespace ldy985.FileMagic.Core.Rules.Rules
{
    public class EmptyRule : BaseRule
    {
        /// <inheritdoc />
        public EmptyRule(ILogger<EmptyRule> logger) : base(logger) { }

        public override IMagic? Magic { get; } = new Magic("");

        /// <inheritdoc />
        public override Quality Quality => Quality.Best;

        public override ITypeInfo TypeInfo { get; } = new TypeInfo("Empty file");

        /// <inheritdoc />
        protected override bool TryStructureInternal(BinaryReader reader, IResult result)
        {
            return reader.GetLength() == 0;
        }
    }
}