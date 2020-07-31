using ldy985.FileMagic.Abstracts;
using Microsoft.Extensions.Logging;

namespace ldy985.FileMagic.Core.Rules.Rules
{
    public class VHDRule : BaseRule
    {
        /// <inheritdoc />
        public override IMagic Magic { get; } = new Magic("636F6E6563746978", 0);

        public override ITypeInfo TypeInfo { get; } = new TypeInfo("Virtual PC Virtual HD image", "VHD");

        /// <inheritdoc />
        public VHDRule(ILogger<VHDRule> logger) : base(logger) { }
    }
}