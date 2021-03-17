using System.Diagnostics.CodeAnalysis;
using System.IO;
using ldy985.BinaryReaderExtensions;
using ldy985.FileMagic.Abstracts;
using Microsoft.Extensions.Logging;

namespace ldy985.FileMagic.Core.Rules.Rules
{
    public class EmptyRule : BaseRule
    {
        /// <inheritdoc />
        protected override bool TryStructureInternal([NotNull] BinaryReader reader, IResult result)
        {
            return reader.GetLength() == 0;
        }

        public override IMagic? Magic { get; } = new Magic("", 0);

        public override ITypeInfo TypeInfo { get; } = new TypeInfo("Empty file");

        /// <inheritdoc />
        public EmptyRule(ILogger<EmptyRule> logger) : base(logger)
        {
        }
    }
}