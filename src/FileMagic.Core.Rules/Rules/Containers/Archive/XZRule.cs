using ldy985.FileMagic.Abstracts;
using Microsoft.Extensions.Logging;

namespace ldy985.FileMagic.Core.Rules.Rules.Containers.Archive
{
    public class XZRule : BaseRule
    {
        /// <inheritdoc />
        public XZRule(ILogger<XZRule> logger) : base(logger) { }

        /// <inheritdoc />
        public override IMagic Magic { get; } = new Magic("FD377A585A00");

        /// <inheritdoc />
        public override Quality Quality => Quality.High;

        public override ITypeInfo TypeInfo { get; } = new TypeInfo("XZ Compressed Archive", "XZ");
    }
}