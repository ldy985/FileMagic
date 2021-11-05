using ldy985.FileMagic.Abstracts;
using Microsoft.Extensions.Logging;

namespace ldy985.FileMagic.Core.Rules.Rules
{
    /// <summary>
    ///     Chrome webview cache
    ///     https://arxiv.org/ftp/arxiv/papers/1707/1707.08696.pdf
    /// </summary>
    public class ChromeIndexRule : BaseRule
    {
        public ChromeIndexRule(ILogger<ChromeIndexRule> logger) : base(logger) { }

        public override IMagic Magic { get; } = new Magic("305C72A71B6DFBFC");

        public override ITypeInfo TypeInfo { get; } = new TypeInfo("Chrome webview cache");
    }
}