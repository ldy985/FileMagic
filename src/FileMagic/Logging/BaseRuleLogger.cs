using Microsoft.Extensions.Logging;

namespace ldy985.FileMagic.Logging;

internal static partial class FileMagicLogger
{
    [LoggerMessage(100, LogLevel.Trace, "Trying {Matcher} matcher")]
    internal static partial void LogMatchTry(ILogger logger, string Matcher);

    [LoggerMessage(101, LogLevel.Debug, "{Matcher} matched")]
    internal static partial void LogMatch(ILogger logger, string Matcher);

    [LoggerMessage(102, LogLevel.Debug, "Rule: {RuleName} matched")]
    internal static partial void LogRuleMatch(ILogger logger, string RuleName);

    [LoggerMessage(103, LogLevel.Trace, "Testing {RuleName} pattern")]
    internal static partial void LogTestPattern(ILogger logger, string RuleName);

    [LoggerMessage(104, LogLevel.Debug, "Matched {RuleName} pattern")]
    internal static partial void LogPatternMatch(ILogger logger, string RuleName);

    [LoggerMessage(105, LogLevel.Trace, "Testing {RuleName} structure")]
    internal static partial void LogTestStructure(ILogger logger, string RuleName);

    [LoggerMessage(106, LogLevel.Debug, "Matched {RuleName} structure")]
    internal static partial void LogStructureMatch(ILogger logger, string RuleName);

    [LoggerMessage(107, LogLevel.Trace, "Testing {RuleName} parser")]
    internal static partial void LogTestParser(ILogger logger, string RuleName);

    [LoggerMessage(108, LogLevel.Debug, "Matched {RuleName} parser")]
    internal static partial void LogParserMatch(ILogger logger, string RuleName);
}