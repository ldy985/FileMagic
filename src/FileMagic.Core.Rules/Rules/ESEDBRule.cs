using ldy985.FileMagic.Abstracts;
using Microsoft.Extensions.Logging;

namespace ldy985.FileMagic.Core.Rules.Rules
{
    /// <summary>
    /// http://fileformats.archiveteam.org/wiki/Extensible_Storage_Engine
    /// https://github.com/libyal/libesedb
    /// </summary>
    public class ESEDBRule : BaseRule
    {
        /// <inheritdoc />
        public override IMagic Magic { get; } = new Magic("efcdab89", 4);

        public override ITypeInfo TypeInfo { get; } = new TypeInfo("Extensible Storage Engine (ESE) Database File (EDB)", "EDB", "SDB");

        /// <inheritdoc />
        public ESEDBRule(ILogger<ESEDBRule> logger) : base(logger) { }
    }
}