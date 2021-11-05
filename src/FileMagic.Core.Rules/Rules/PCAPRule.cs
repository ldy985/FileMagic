using ldy985.FileMagic.Abstracts;
using Microsoft.Extensions.Logging;

namespace ldy985.FileMagic.Core.Rules.Rules
{
    /// <summary>
    ///     https://wiki.wireshark.org/Development/LibpcapFileFormat
    /// </summary>
    public class PCAPRule : BaseRule
    {
        /// <inheritdoc />
        public PCAPRule(ILogger<PCAPRule> logger) : base(logger) { }

        /// <inheritdoc />
        public override IMagic Magic { get; } = new Magic("D4C3B2A1");

        /// <inheritdoc />
        public override ITypeInfo TypeInfo { get; } = new TypeInfo("libpcap file format", "PCAP");
    }
}