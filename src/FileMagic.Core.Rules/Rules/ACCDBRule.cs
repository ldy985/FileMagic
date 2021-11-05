using ldy985.FileMagic.Abstracts;
using Microsoft.Extensions.Logging;

namespace ldy985.FileMagic.Core.Rules.Rules
{
    /// <summary>
    ///     http://jabakobob.net/mdb/first-page.html
    ///     http://jabakobob.net/mdb/
    /// </summary>
    public class ACCDBRule : BaseRule
    {
        /// <inheritdoc />
        public ACCDBRule(ILogger<ACCDBRule> logger) : base(logger) { }

        /// <inheritdoc />
        public override IMagic Magic { get; } = new Magic("000100005374616E646172642041434520444200");

        public override ITypeInfo TypeInfo { get; } = new TypeInfo("Microsoft Access database", "ACCDB");
    }

    /// <summary>
    ///     http://jabakobob.net/mdb/first-page.html
    ///     http://jabakobob.net/mdb/
    /// </summary>
    public class MDBRule : BaseRule
    {
        /// <inheritdoc />
        public MDBRule(ILogger<MDBRule> logger) : base(logger) { }

        /// <inheritdoc />
        public override IMagic Magic { get; } = new Magic("000100005374616E64617264204A657420444200");

        public override ITypeInfo TypeInfo { get; } = new TypeInfo("Microsoft Access database", "MDB");
    }
}