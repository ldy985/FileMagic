using ldy985.FileMagic.Abstracts;
using Microsoft.Extensions.Logging;

namespace ldy985.FileMagic.Core.Rules.Rules
{
    /// <summary>
    ///     http://fileformats.archiveteam.org/wiki/Extensible_Storage_Engine
    ///     https://github.com/libyal/libesedb/blob/master/documentation/Extensible%20Storage%20Engine%20(ESE)%20Database%20File%20(EDB)%20format.asciidoc
    /// </summary>
    public class ESEDBRule : BaseRule
    {
        /// <inheritdoc />
        public ESEDBRule(ILogger<ESEDBRule> logger) : base(logger) { }

        /// <inheritdoc />
        public override IMagic Magic { get; } = new Magic("efcdab89", 4);

        public override ITypeInfo TypeInfo { get; } = new TypeInfo("Extensible Storage Engine (ESE) Database File (EDB)", "EDB", "SDB", "DIT");
    }
}