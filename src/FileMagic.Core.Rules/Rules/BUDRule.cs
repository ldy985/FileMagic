using ldy985.FileMagic.Abstracts;
using Microsoft.Extensions.Logging;

namespace ldy985.FileMagic.Core.Rules.Rules
{
    /// <summary>
    ///     https://docs.microsoft.com/en-us/troubleshoot/windows-server/printing/cannot-print-install-service-pack-update-rollup
    ///     https://fileinfo.com/extension/bud
    /// </summary>
    public class BUDRule : BaseRule
    {
        /// <inheritdoc />
        public BUDRule(ILogger<BUDRule> logger) : base(logger) { }

        /// <inheritdoc />
        public override IMagic Magic { get; } = new Magic("00002044504773001900", 2);

        /// <inheritdoc />
        public override ITypeInfo TypeInfo { get; } = new TypeInfo("Binary printer description file", "BUD");
    }
}