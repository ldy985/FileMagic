using ldy985.FileMagic.Abstracts;
using Microsoft.Extensions.Logging;

namespace ldy985.FileMagic.Core.Rules.Rules.FileSystems
{
    /// <summary>
    /// https://en.wikipedia.org/wiki/NTFS
    /// </summary>
    public class NTFSRule : BaseRule
    {
        public override IMagic Magic { get; } = new Magic("4E544653E28082E28082E28082E28082", 3);

        public override ITypeInfo TypeInfo { get; } = new TypeInfo("NTFS (NT File System)");

        public NTFSRule(ILogger<NTFSRule> logger) : base(logger) { }
    }
}