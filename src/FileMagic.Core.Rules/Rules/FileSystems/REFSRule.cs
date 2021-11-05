using ldy985.FileMagic.Abstracts;
using Microsoft.Extensions.Logging;

namespace ldy985.FileMagic.Core.Rules.Rules.FileSystems
{
    /// <summary>
    ///     https://github.com/libyal/libfsrefs/blob/master/documentation/Resilient%20File%20System%20(ReFS).pdf
    /// </summary>
    public class REFSRule : BaseRule
    {
        public REFSRule(ILogger<REFSRule> logger) : base(logger) { }
        public override IMagic Magic { get; } = new Magic("5265465300000000");
        public override ITypeInfo TypeInfo { get; } = new TypeInfo("Resilient File System (ReFS)");
    }
}