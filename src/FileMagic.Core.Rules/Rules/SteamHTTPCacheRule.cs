using ldy985.FileMagic.Abstracts;
using Microsoft.Extensions.Logging;

namespace ldy985.FileMagic.Core.Rules.Rules
{
    public class SteamHTTPCacheRule : BaseRule
    {
        /// <inheritdoc />
        public SteamHTTPCacheRule(ILogger<SteamHTTPCacheRule> logger) : base(logger) { }

        /// <inheritdoc />
        public override IMagic Magic { get; } = new Magic("02000000??????1200000000??29B50C60");

        public override ITypeInfo TypeInfo { get; } = new TypeInfo("Steam http cache file");
    }
}