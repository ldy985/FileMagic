using ldy985.FileMagic.Abstracts;
using Microsoft.Extensions.Logging;

namespace ldy985.FileMagic.Core.Rules.Rules
{
    /// <summary>
    ///     https://stackoverflow.com/questions/33239875/jks-bks-and-pkcs12-file-formats
    /// </summary>
    public class JCEKSRule : BaseRule
    {
        /// <inheritdoc />
        public JCEKSRule(ILogger<JCEKSRule> logger) : base(logger) { }

        /// <inheritdoc />
        public override IMagic Magic { get; } = new Magic("CECECECE");

        /// <inheritdoc />
        public override ITypeInfo TypeInfo { get; } = new TypeInfo("Java Key Store", "JCEKS", "JKS");
    }
}