using ldy985.FileMagic.Abstracts;
using Microsoft.Extensions.Logging;

namespace ldy985.FileMagic.Core.Rules.Rules.Containers
{
    /// <summary>
    /// https://msdn.microsoft.com/en-us/library/mt740058.aspx
    /// https://winprotocoldoc.blob.core.windows.net/productionwindowsarchives/MS-VHDX/[MS-VHDX].pdf
    /// </summary>
    public class VHDXRule : BaseRule
    {
        /// <inheritdoc />
        public override IMagic Magic { get; } = new Magic("7668647866696c65", 0);

        public override ITypeInfo TypeInfo { get; } = new TypeInfo("Virtual PC Virtual HD image", "VHDX");

        /// <inheritdoc />
        public VHDXRule(ILogger<VHDXRule> logger) : base(logger) { }
    }
}