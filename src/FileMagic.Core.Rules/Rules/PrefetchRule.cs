using ldy985.FileMagic.Abstracts;
using Microsoft.Extensions.Logging;

namespace ldy985.FileMagic.Core.Rules.Rules
{
    /// <summary>
    ///     https://github.com/libyal/libscca/blob/master/documentation/Windows%20Prefetch%20File%20(PF)%20format.asciidoc
    /// </summary>
    public class MAMPrefetchRule : BaseRule
    {
        /// <inheritdoc />
        public MAMPrefetchRule(ILogger<MAMPrefetchRule> logger) : base(logger) { }

        /// <inheritdoc />
        public override IMagic Magic { get; } = new Magic("4d414d04");
        /// <inheritdoc />
        public override Quality Quality => Quality.Medium;
        public override ITypeInfo TypeInfo { get; } = new TypeInfo("Compressed Prefetch file", "PF");
    }

    /// <summary>
    ///     https://github.com/libyal/libscca/blob/master/documentation/Windows%20Prefetch%20File%20(PF)%20format.asciidoc
    /// </summary>
    public class SCCAPrefetchRule : BaseRule
    {
        /// <inheritdoc />
        public SCCAPrefetchRule(ILogger<SCCAPrefetchRule> logger) : base(logger) { }

        /// <inheritdoc />
        public override IMagic Magic { get; } = new Magic("53434341", 4);
        /// <inheritdoc />
        public override Quality Quality => Quality.Medium;
        public override ITypeInfo TypeInfo { get; } = new TypeInfo("Uncompressed  Prefetch file", "PF");
    }
}