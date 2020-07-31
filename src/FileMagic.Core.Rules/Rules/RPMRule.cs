using ldy985.FileMagic.Abstracts;
using Microsoft.Extensions.Logging;

namespace ldy985.FileMagic.Core.Rules.Rules
{
    /// <summary>
    /// http://ftp.rpm.org/max-rpm/s1-rpm-file-format-rpm-file-format.html
    /// </summary>
    public class RPMRule : BaseRule
    {
        /// <inheritdoc />
        public override IMagic Magic { get; } = new Magic("EDABEEDB", 0);

        public override ITypeInfo TypeInfo { get; } = new TypeInfo("Red Hat Package Manager file", "RPM");

        /// <inheritdoc />
        public RPMRule(ILogger<RPMRule> logger) : base(logger) { }
    }
}