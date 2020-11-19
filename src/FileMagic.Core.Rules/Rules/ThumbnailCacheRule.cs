using ldy985.FileMagic.Abstracts;
using Microsoft.Extensions.Logging;

namespace ldy985.FileMagic.Core.Rules.Rules
{
    /// <summary>
    /// https://github.com/libyal/libwtcdb/blob/master/documentation/Windows%20Explorer%20Thumbnail%20Cache%20database%20format.asciidoc
    /// </summary>
    public class ThumbnailCacheRule : BaseRule
    {
        /// <inheritdoc />
        public override IMagic Magic { get; } = new Magic("434d4d4d", 0);

        public override ITypeInfo TypeInfo { get; } = new TypeInfo("Explorer Thumbnail Cache database", "DB");

        /// <inheritdoc />
        public ThumbnailCacheRule(ILogger<ThumbnailCacheRule> logger) : base(logger) { }
    }
}