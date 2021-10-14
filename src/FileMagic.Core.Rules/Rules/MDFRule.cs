using System.IO;
using ldy985.BinaryReaderExtensions;
using ldy985.FileMagic.Abstracts;
using ldy985.NumberExtensions;
using Microsoft.Extensions.Logging;

namespace ldy985.FileMagic.Core.Rules.Rules
{
    public class MDFRule : BaseRule
    {
        /// <inheritdoc />
        public override IMagic Magic { get; } = new Magic("010F0000", 0); 

        public override ITypeInfo TypeInfo { get; } = new TypeInfo("MSSQL database", "MDF");

        /// <inheritdoc />
        public MDFRule(ILogger<MDFRule> logger) : base(logger) { }
    }
}