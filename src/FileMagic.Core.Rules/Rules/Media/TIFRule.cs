using ldy985.FileMagic.Abstracts;
using Microsoft.Extensions.Logging;

namespace ldy985.FileMagic.Core.Rules.Rules.Media
{
    public class TIF_LERule : BaseRule
    {
        /// <inheritdoc />
        public override IMagic Magic { get; } = new Magic("49492A00", 0);

        public override ITypeInfo TypeInfo { get; } = new TypeInfo("Tif image format", "TIF");

        /// <inheritdoc />
        public TIF_LERule(ILogger<TIF_LERule> logger) : base(logger) { }
    }

    public class TIF_BERule : BaseRule
    {
        /// <inheritdoc />
        public override IMagic Magic { get; } = new Magic("4D4D002A", 0);

        public override ITypeInfo TypeInfo { get; } = new TypeInfo("Tif image format", "TIF");

        /// <inheritdoc />
        public TIF_BERule(ILogger<TIF_BERule> logger) : base(logger) { }
    }
}