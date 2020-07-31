using ldy985.FileMagic.Abstracts;
using Microsoft.Extensions.Logging;

namespace ldy985.FileMagic.Core.Rules.Rules
{
    /// <summary>
    /// https://github.com/StatsHelix/demoinfo
    /// </summary>
    public class DEMRule : BaseRule
    {
        /// <inheritdoc />
        public override IMagic Magic { get; } = new Magic("484C3244454D4F", 0);

        public override ITypeInfo TypeInfo { get; } = new TypeInfo("CS:GO-Demo", "DEM");

        /// <inheritdoc />
        public DEMRule(ILogger<DEMRule> logger) : base(logger) { }

    }
}