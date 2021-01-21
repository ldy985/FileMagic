using ldy985.FileMagic.Abstracts;
using Microsoft.Extensions.Logging;

namespace ldy985.FileMagic.Core.Rules.Rules
{
    /// <summary>
    /// https://keepass.info/help/base/repair.html
    /// </summary>
    public class KDBXRule : BaseRule
    {
        /// <inheritdoc />
        public override IMagic Magic { get; } = new Magic("03D9A29A67FB4BB5", 0);

        public override ITypeInfo TypeInfo { get; } = new TypeInfo("KeePass 2.x database", "KDBX");

        /// <inheritdoc />
        public KDBXRule(ILogger<KDBXRule> logger) : base(logger) { }
    }
}