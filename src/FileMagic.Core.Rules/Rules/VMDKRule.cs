using ldy985.FileMagic.Abstracts;
using Microsoft.Extensions.Logging;

namespace ldy985.FileMagic.Core.Rules.Rules
{
    public class VMDKRule : BaseRule
    {
        /// <inheritdoc />
        public override IMagic Magic { get; } = new Magic("4B444D", 0);

        public override ITypeInfo TypeInfo { get; } = new TypeInfo("Virtual Disk Format", "VMDK");

        /// <inheritdoc />
        public VMDKRule(ILogger<VMDKRule> logger) : base(logger) { }
    }
}