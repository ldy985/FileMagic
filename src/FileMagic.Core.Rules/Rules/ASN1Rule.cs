using ldy985.FileMagic.Abstracts;
using Microsoft.Extensions.Logging;

namespace ldy985.FileMagic.Core.Rules.Rules
{
    /// <summary>
    /// http://lapo.it/asn1js/
    /// https://stackoverflow.com/questions/1598236/microsoft-security-catalog-format-documentation-and-api-samples
    /// </summary>
    public class CATRule : BaseRule
    {
        /// <inheritdoc />
        public override IMagic Magic { get; } = new Magic("3082????06092A864886F70D010702A082", 0);

        public override ITypeInfo TypeInfo { get; } = new TypeInfo("ASN1 encoded file", "CAT");

        /// <inheritdoc />
        public CATRule(ILogger<CATRule> logger) : base(logger)
        {
        }
    }

    /// <summary>
    /// http://lapo.it/asn1js/
    /// https://stackoverflow.com/questions/1598236/microsoft-security-catalog-format-documentation-and-api-samples
    /// </summary>
    public class CAT1Rule : BaseRule
    {
        /// <inheritdoc />
        public override IMagic Magic { get; } = new Magic("3083??????06092A864886F70D010702A083", 0);

        public override ITypeInfo TypeInfo { get; } = new TypeInfo("ASN1 encoded file", "CAT");

        /// <inheritdoc />
        public CAT1Rule(ILogger<CAT1Rule> logger) : base(logger)
        {
        }
    }
}