using ldy985.FileMagic.Abstracts;
using Microsoft.Extensions.Logging;

namespace ldy985.FileMagic.Core.Rules.Rules
{
    public class A3BISIGNRule : BaseRule
    {
        /// <inheritdoc />
        public override IMagic Magic { get; } = new Magic("613300940000000602000000240000525341310004000001", 0);

        public override ITypeInfo TypeInfo { get; } = new TypeInfo("Bohemia Interactive ArmA 3 resource file signature", "BISIGN");

        /// <inheritdoc />
        public A3BISIGNRule(ILogger<A3BISIGNRule> logger) : base(logger)
        {
        }
    }

    public class DayZBISIGNRule : BaseRule
    {
        /// <inheritdoc />
        public override IMagic Magic { get; } = new Magic("6461797A00940000000602000000240000525341310004000001", 0);

        public override ITypeInfo TypeInfo { get; } = new TypeInfo("Bohemia Interactive DayZ resource file signature", "BISIGN");

        /// <inheritdoc />
        public DayZBISIGNRule(ILogger<DayZBISIGNRule> logger) : base(logger)
        {
        }
    }
}