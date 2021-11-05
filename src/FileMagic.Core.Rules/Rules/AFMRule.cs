using ldy985.FileMagic.Abstracts;
using Microsoft.Extensions.Logging;

namespace ldy985.FileMagic.Core.Rules.Rules
{
    /// <summary>
    ///     https://www.adobe.com/content/dam/acom/en/devnet/font/pdfs/5004.AFM_Spec.pdf
    /// </summary>
    public class AFMRule : BaseRule
    {
        /// <inheritdoc />
        public AFMRule(ILogger<AFMRule> logger) : base(logger) { }

        /// <inheritdoc />
        public override IMagic Magic { get; } = new Magic("5374617274466F6E744D657472696373");

        /// <inheritdoc />
        public override ITypeInfo TypeInfo { get; } = new TypeInfo("Adobe font metrics file", "AFM");
    }

    /// <summary>
    ///     https://www.adobe.com/content/dam/acom/en/devnet/font/pdfs/5004.AFM_Spec.pdf
    /// </summary>
    public class ACFMRule : BaseRule
    {
        /// <inheritdoc />
        public ACFMRule(ILogger<ACFMRule> logger) : base(logger) { }

        /// <inheritdoc />
        public override IMagic Magic { get; } = new Magic("5374617274436f6d70466f6e744d657472696373");

        /// <inheritdoc />
        public override ITypeInfo TypeInfo { get; } = new TypeInfo("Adobe font metrics file", "ACFM");
    }

    /// <summary>
    ///     https://www.adobe.com/content/dam/acom/en/devnet/font/pdfs/5004.AFM_Spec.pdf
    /// </summary>
    public class AMFMRule : BaseRule
    {
        /// <inheritdoc />
        public AMFMRule(ILogger<AMFMRule> logger) : base(logger) { }

        /// <inheritdoc />
        public override IMagic Magic { get; } = new Magic("53746172744d6173746572466f6e744d657472696373");

        /// <inheritdoc />
        public override ITypeInfo TypeInfo { get; } = new TypeInfo("Adobe font metrics file", "AMFM");
    }
}