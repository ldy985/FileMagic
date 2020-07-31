using ldy985.FileMagic.Abstracts;
using Microsoft.Extensions.Logging;

namespace ldy985.FileMagic.Core.Rules.Rules
{
    public class RAR5Rule : BaseRule
    {
        /// <inheritdoc />
        public override IMagic Magic { get; } = new Magic("526172211A070100", 0);

        public override ITypeInfo TypeInfo { get; } = new TypeInfo("RAR5 compressed file", "RAR");

        /// <inheritdoc />
        public RAR5Rule(ILogger<RAR5Rule> logger) : base(logger) { }
    }
}