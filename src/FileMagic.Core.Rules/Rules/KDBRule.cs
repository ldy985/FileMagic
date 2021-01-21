using ldy985.FileMagic.Abstracts;
using Microsoft.Extensions.Logging;

namespace ldy985.FileMagic.Core.Rules.Rules
{
    /// <summary>
    /// https://keepass.info/help/base/repair.html
    /// </summary>
    public class KDBRule : BaseRule
    {
        /// <inheritdoc />
        public override IMagic Magic { get; } = new Magic("03D9A29A65FB4BB5", 0);

        public override ITypeInfo TypeInfo { get; } = new TypeInfo("KeePass 1.x database", "KDB");

        /// <inheritdoc />
        public KDBRule(ILogger<KDBRule> logger) : base(logger) { }
    }
}