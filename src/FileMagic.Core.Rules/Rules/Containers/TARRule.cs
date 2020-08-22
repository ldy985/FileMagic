using ldy985.FileMagic.Abstracts;
using Microsoft.Extensions.Logging;

namespace ldy985.FileMagic.Core.Rules.Rules.Containers
{
    public class TARRule : BaseRule
    {
        /// <inheritdoc />
        public override IMagic Magic { get; } = new Magic("7573746172", 257);

        public override ITypeInfo TypeInfo { get; } = new TypeInfo("TAR file format", "TAR");

        /// <inheritdoc />
        public TARRule(ILogger<TARRule> logger) : base(logger) { }
    }
}