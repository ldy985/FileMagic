using ldy985.FileMagic.Abstracts;
using Microsoft.Extensions.Logging;

namespace ldy985.FileMagic.Core.Rules.Rules.Containers
{
    /// <summary>
    /// https://www.loc.gov/preservation/digital/formats/fdd/fdd000408.shtml
    /// https://github.com/libyal/libewf/blob/master/documentation/Expert%20Witness%20Compression%20Format%20%28EWF%29.asciidoc
    /// </summary>
    public class EWFE01Rule : BaseRule
    {
        /// <inheritdoc />
        public override IMagic Magic { get; } = new Magic("455646090D0AFF00", 0);

        public override ITypeInfo TypeInfo { get; } = new TypeInfo("Expert Witness Compression Format", "E01");

        /// <inheritdoc />
        public EWFE01Rule(ILogger<EWFE01Rule> logger) : base(logger) { }
    }

    /// <summary>
    /// https://www.loc.gov/preservation/digital/formats/fdd/fdd000408.shtml
    /// https://github.com/libyal/libewf/blob/master/documentation/Expert%20Witness%20Compression%20Format%20%28EWF%29.asciidoc
    /// </summary>
    public class EWFL01Rule : BaseRule
    {
        /// <inheritdoc />
        public override IMagic Magic { get; } = new Magic("4C5646090D0AFF00", 0);

        public override ITypeInfo TypeInfo { get; } = new TypeInfo("Expert Witness Compression Format", "E01");

        /// <inheritdoc />
        public EWFL01Rule(ILogger<EWFL01Rule> logger) : base(logger) { }
    }

    /// <summary>
    /// https://www.loc.gov/preservation/digital/formats/fdd/fdd000408.shtml
    /// https://github.com/libyal/libewf/blob/master/documentation/Expert%20Witness%20Compression%20Format%20%28EWF%29.asciidoc
    /// </summary>
    public class EWFEx01Rule : BaseRule
    {
        /// <inheritdoc />
        public override IMagic Magic { get; } = new Magic("455646320D0A81", 0);

        public override ITypeInfo TypeInfo { get; } = new TypeInfo("EnCase® Evidence File Format Version 2", "Ex01");

        /// <inheritdoc />
        public EWFEx01Rule(ILogger<EWFEx01Rule> logger) : base(logger) { }
    }
}