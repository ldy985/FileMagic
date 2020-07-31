using ldy985.FileMagic.Abstracts;
using Microsoft.Extensions.Logging;

namespace ldy985.FileMagic.Core.Rules.Rules
{
    public class CHMRule : BaseRule
    {
        /// <inheritdoc />
        public override IMagic Magic { get; } = new Magic("49545346????????????????01000000??????????????????????7CAA7BD0119E0C00A0C922E6EC11FD017CAA7BD0119E0C00A0C922E6EC", 0);

        public override ITypeInfo TypeInfo { get; } = new TypeInfo("Microsoft Compiled HTML", "CHM");

        /// <inheritdoc />
        public CHMRule(ILogger<CHMRule> logger) : base(logger) { }
    }
}