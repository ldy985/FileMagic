using ldy985.FileMagic.Abstracts;
using Microsoft.Extensions.Logging;

namespace ldy985.FileMagic.Core.Rules.Rules
{
    public class VDIRule : BaseRule
    {
        /// <inheritdoc />
        public VDIRule(ILogger<VDIRule> logger) : base(logger) { }

        /// <inheritdoc />
        public override IMagic Magic { get; } =
            new Magic("3C3C3C204F7261636C6520564D205669727475616C426F78204469736B20496D616765203E3E3E0A000000000000000000000000000000000000000000000000");
        /// <inheritdoc />
        public override Quality Quality => Quality.High;
        public override ITypeInfo TypeInfo { get; } = new TypeInfo("VirtualBox Virtual Disk Image", "VDI");
    }
}