using ldy985.FileMagic.Abstracts;
using Microsoft.Extensions.Logging;

namespace ldy985.FileMagic.Core.Rules.Rules
{
    /// <summary>
    /// http://metastatic.org/source/JKS.java
    /// https://stackoverflow.com/questions/10839303/specification-of-jks-key-store-format
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