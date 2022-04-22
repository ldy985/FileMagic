using System;
using Microsoft.Extensions.Logging;

namespace ldy985.FileMagic.Core.Logging;

internal static partial class BaseRuleLogger
{
    [LoggerMessage(0, LogLevel.Error, "Error while TryingStructure with: {RuleName}")]
    internal static partial void LogStructureError(ILogger logger, Exception ex, string RuleName);

    [LoggerMessage(1, LogLevel.Error, "Error while parsing with: {RuleName}")]
    internal static partial void LogParsingError(ILogger logger, Exception ex, string RuleName);
}