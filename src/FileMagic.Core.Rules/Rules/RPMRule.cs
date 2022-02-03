using ldy985.FileMagic.Abstracts;
using Microsoft.Extensions.Logging;

namespace ldy985.FileMagic.Core.Rules.Rules
{
    /// <summary>
    ///     http://ftp.rpm.org/max-rpm/s1-rpm-file-format-rpm-file-format.html
    /// </summary>
    public class RPMRule : BaseRule
    {
        /// <inheritdoc />
        public RPMRule(ILogger<RPMRule> logger) : base(logger) { }

        /// <inheritdoc />
        public override IMagic Magic { get; } = new Magic("EDABEEDB");

        /// <inheritdoc />
        public override Quality Quality => Quality.Medium;

        public override ITypeInfo TypeInfo { get; } = new TypeInfo("Red Hat Package Manager file", "RPM");
    }
}