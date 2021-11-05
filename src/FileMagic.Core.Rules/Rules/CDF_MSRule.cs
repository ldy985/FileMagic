using ldy985.FileMagic.Abstracts;
using Microsoft.Extensions.Logging;

namespace ldy985.FileMagic.Core.Rules.Rules
{
    /// <summary>
    ///     https://fileinfo.com/extension/cdf-ms
    /// </summary>
    public class CDF_MSRule : BaseRule
    {
        /// <inheritdoc />
        public CDF_MSRule(ILogger<CDF_MSRule> logger) : base(logger) { }

        /// <inheritdoc />
        public override IMagic Magic { get; } = new Magic("50636D480100000000000000");

        /// <inheritdoc />
        public override ITypeInfo TypeInfo { get; } = new TypeInfo("Compiled manifest file", "CDF-MS");
    }
}