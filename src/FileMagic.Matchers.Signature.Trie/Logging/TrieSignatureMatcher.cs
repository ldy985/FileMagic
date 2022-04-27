using Microsoft.Extensions.Logging;

namespace ldy985.FileMagic.Matchers.Signature.Trie.Logging;

internal static partial class TrieSignatureMatcherLogger
{
    [LoggerMessage(3, LogLevel.Trace, "Registered: {RuleName}")]
    internal static partial void LogRuleRegistration(ILogger logger, string RuleName);

    [LoggerMessage(4, LogLevel.Trace, "Trie found no leafs")]
    internal static partial void LogNoLeafs(ILogger logger);

    [LoggerMessage(5, LogLevel.Trace, "Trie found at least 1 leaf")]
    internal static partial void LogFoundLeaf(ILogger logger);
}