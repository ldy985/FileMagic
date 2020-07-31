using ldy985.FileMagic.Abstracts;
using Microsoft.Extensions.Logging;

namespace ldy985.FileMagic.Core.Rules.Rules
{
    /// <summary>
    /// http://metastatic.org/source/JKS.java
    /// </summary>
    public class JKSRule : BaseRule
    {
        /// <inheritdoc />
        public override IMagic Magic { get; } = new Magic("FEEDFEED", 0);

        /// <inheritdoc />
        public override ITypeInfo TypeInfo { get; } = new TypeInfo("Java Key Store", "JKS");

        /// <inheritdoc />
        public JKSRule(ILogger<JKSRule> logger) : base(logger) { }
    }
}