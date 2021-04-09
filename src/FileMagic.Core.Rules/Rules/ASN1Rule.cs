using ldy985.FileMagic.Abstracts;
using Microsoft.Extensions.Logging;

namespace ldy985.FileMagic.Core.Rules.Rules
{
    /// <summary>
    /// http://lapo.it/asn1js/
    /// https://stackoverflow.com/questions/1598236/microsoft-security-catalog-format-documentation-and-api-samples
    /// </summary>
    public class ASN1DERRule : BaseRule
    {
        /// <inheritdoc />
        public override IMagic Magic { get; } = new Magic("3082????06092A864886F70D010702A082", 0);

        public override ITypeInfo TypeInfo { get; } = new TypeInfo("ASN1 DER encoded file", "CAT", "P7B");

        /// <inheritdoc />
        public ASN1DERRule(ILogger<ASN1DERRule> logger) : base(logger)
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

        public override ITypeInfo TypeInfo { get; } = new TypeInfo("ASN1 encoded file", "CAT", "P7B");

        /// <inheritdoc />
        public CAT1Rule(ILogger<CAT1Rule> logger) : base(logger)
        {
        }
    }

    /// <summary>
    /// http://lapo.it/asn1js/
    /// https://stackoverflow.com/questions/1598236/microsoft-security-catalog-format-documentation-and-api-samples
    /// </summary>
    public class P7SRule : BaseRule
    {
        /// <inheritdoc />
        public override IMagic Magic { get; } = new Magic("308006092A864886", 0);

        public override ITypeInfo TypeInfo { get; } = new TypeInfo("ASN1 encoded file", "P7S");

        /// <inheritdoc />
        public P7SRule(ILogger<P7SRule> logger) : base(logger)
        {
        }
    }

    /// <summary>
    /// https://www.ssl.com/guide/pem-der-crt-and-cer-x-509-encodings-and-conversions/
    /// </summary>
    public class P7SPEMRule : BaseRule
    {
        /// <inheritdoc />
        public override IMagic Magic { get; } = new Magic("2D2D2D2D2D424547494E20504B4353372D2D2D2D2D", 0);

        public override ITypeInfo TypeInfo { get; } = new TypeInfo("ASN1 encoded file", "P7B");

        /// <inheritdoc />
        public P7SPEMRule(ILogger<P7SRule> logger) : base(logger)
        {
        }
    }
}