using ldy985.FileMagic.Abstracts;
using Microsoft.Extensions.Logging;

namespace ldy985.FileMagic.Core.Rules.Rules
{
    /// <summary>
    /// https://docs.microsoft.com/en-us/visualstudio/extensibility/internals/solution-dot-sln-file?view=vs-2019
    /// https://github.com/MicrosoftDocs/visualstudio-docs/issues/4078
    /// </summary>
    public class SLNRule : BaseRule
    {
        /// <inheritdoc />
        public override IMagic Magic { get; } = new Magic("4D6963726F736F66742056697375616C2053747564696F20536F6C7574696F6E2046696C65", 0);

        public override ITypeInfo TypeInfo { get; } = new TypeInfo("Visual studio solution file", "SLN");

        /// <inheritdoc />
        public SLNRule(ILogger<SLNRule> logger) : base(logger)
        {
        }
    }

    /// <summary>
    /// https://docs.microsoft.com/en-us/visualstudio/extensibility/internals/solution-dot-sln-file?view=vs-2019
    /// https://github.com/MicrosoftDocs/visualstudio-docs/issues/4078
    /// </summary>
    public class SLN_BOMRule : BaseRule
    {
        /// <inheritdoc />
        public override IMagic Magic { get; } = new Magic("EFBBBF0D0A4D6963726F736F66742056697375616C2053747564696F20536F6C7574696F6E2046696C65", 0);

        public override ITypeInfo TypeInfo { get; } = new TypeInfo("Visual studio solution file", "SLN");

        /// <inheritdoc />
        public SLN_BOMRule(ILogger<SLN_BOMRule> logger) : base(logger)
        {
        }
    }
}