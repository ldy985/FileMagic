using ldy985.FileMagic.Abstracts;
using Microsoft.Extensions.Logging;

namespace ldy985.FileMagic.Core.Rules.Rules
{
    /// <summary>
    /// https://www.fileformat.info/format/wmf/egff.htm
    /// </summary>
    public class WMFRule : BaseRule
    {
        /// <inheritdoc />
        public override IMagic Magic { get; } = new Magic("010009000003", 0);

        public override ITypeInfo TypeInfo { get; } = new TypeInfo("Windows Metafile", "WMF");

        /// <inheritdoc />
        public WMFRule(ILogger<WMFRule> logger) : base(logger) { }
    }

    /// <summary>
    /// https://docs.microsoft.com/en-us/windows/desktop/api/gdiplusmetaheader/ns-gdiplusmetaheader-wmfplaceablefileheader
    /// </summary>
    public class WMFwHeaderRule : BaseRule
    {
        /// <inheritdoc />
        public override IMagic Magic { get; } = new Magic("D7CDC69A0000????????????????????????????????01000900", 0);

        public override ITypeInfo TypeInfo { get; } = new TypeInfo("Windows Metafile", "WMF");

        /// <inheritdoc />
        public WMFwHeaderRule(ILogger<WMFwHeaderRule> logger) : base(logger) { }
    }
}