using ldy985.FileMagic.Abstracts;
using Microsoft.Extensions.Logging;

namespace ldy985.FileMagic.Core.Rules.Rules.Containers.Archive
{
    /// <summary>
    /// https://github.com/horsicq/Detect-It-Easy/blob/master/db/Binary/archives.1.sg#L13
    /// </summary>
    public class IzPackRule : BaseRule
    {
        /// <inheritdoc />
        public IzPackRule(ILogger<IzPackRule> logger) : base(logger) { }

        /// <inheritdoc />
        public override IMagic Magic { get; } = new Magic("ACED00057704");

        /// <inheritdoc />
        public override Quality Quality => Quality.Low;

        public override ITypeInfo TypeInfo { get; } = new TypeInfo("IzPack");
    }
}