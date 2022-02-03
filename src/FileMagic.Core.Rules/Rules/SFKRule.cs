using ldy985.FileMagic.Abstracts;
using Microsoft.Extensions.Logging;

namespace ldy985.FileMagic.Core.Rules.Rules
{
    public class SFKRule : BaseRule
    {
        /// <inheritdoc />
        public SFKRule(ILogger<SFKRule> logger) : base(logger) { }

        /// <inheritdoc />
        public override IMagic Magic { get; } = new Magic("5346504B0100000040000000");
        /// <inheritdoc />
        public override Quality Quality => Quality.High;
        public override ITypeInfo TypeInfo { get; } = new TypeInfo("Waveform image of a WAV audio file", "SFK");
    }
}