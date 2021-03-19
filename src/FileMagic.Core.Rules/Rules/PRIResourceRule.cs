using ldy985.FileMagic.Abstracts;
using Microsoft.Extensions.Logging;

namespace ldy985.FileMagic.Core.Rules.Rules
{
    /// <summary>
    /// https://docs.microsoft.com/en-us/windows/uwp/app-resources/compile-resources-manually-with-makepri
    /// </summary>
    public class PRIResourceRule : BaseRule
    {
        /// <inheritdoc />
        public override IMagic Magic { get; } = new Magic("6D726D5F70726932", 0);

        public override ITypeInfo TypeInfo { get; } = new TypeInfo("Windows UWP App resource file", "PRI");

        /// <inheritdoc />
        public PRIResourceRule(ILogger<PRIResourceRule> logger) : base(logger)
        {
        }
    }
}