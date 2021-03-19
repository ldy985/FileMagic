using ldy985.FileMagic.Abstracts;
using Microsoft.Extensions.Logging;

namespace ldy985.FileMagic.Core.Rules.Rules
{
    /// <summary>
    /// https://fileinfo.com/extension/p7x
    /// https://stackoverflow.com/questions/38634052/what-is-the-structure-of-appxsignature-p7x
    /// </summary>
    public class P7XRule : BaseRule
    {
        /// <inheritdoc />
        public override IMagic Magic { get; } = new Magic("504B4358308221", 0);

        /// <inheritdoc />
        public override ITypeInfo TypeInfo { get; } = new TypeInfo("APPX Packed Digital Signature File", "P7X");

        /// <inheritdoc />
        public P7XRule(ILogger<P7XRule> logger) : base(logger)
        {
        }
    }
}