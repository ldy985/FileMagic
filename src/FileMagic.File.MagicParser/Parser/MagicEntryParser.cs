using System;
using System.Globalization;
using System.Text.RegularExpressions;
using Chronos.Libraries.FileClassifier.entries;
using Chronos.Libraries.FileClassifier.Enums;
using Chronos.Libraries.FileClassifier.Helpers;
using Chronos.Libraries.FileClassifier.MagicRule;
using Chronos.Libraries.FileClassifier.Types;
using Chronos.Libraries.FileClassifier.Values;
using Microsoft.Extensions.Logging;

namespace Chronos.Libraries.FileClassifier.Parser
{
    /// <summary>
    /// Class which parses a line from the magic (5) format and produces a <see cref="MagicEntry"/>.
    /// </summary>
    public class MagicEntryParser
    {
        // special lines, others are put into the extensionMap
        private static readonly string MIME_TYPE_LINE = "!:mime";

        /// <summary>
        /// The offset pattern.
        /// </summary>

        // todo compact pattern.
        private static readonly Regex OffsetPattern = new Regex(
                                                                @"^(>+)?(&)?(?:\()?(\&)?(-?[0-9a-fA-Fx]+)(?:([.,])([BbCcSsHhLlm]))?([0-9a-fA-Fx]+)?([+\-*\/%&|^])?(?:\()?([+\-0-9a-fA-Fx]+)?(\))?(?:[)])?(?:[(])?([0-9a-fA-Fx]+)?([)]+)?",
                                                                RegexOptions.Compiled | RegexOptions.IgnoreCase);

        private static readonly string OPTIONAL_LINE = "!:optional";

        /// <summary>
        /// The strength pattern.
        /// </summary>
        private static readonly Regex StrengthPattern = new Regex(@"!:strength(?: |\t)*([\+\-\*\/])(?: |\t)*(\d+)", RegexOptions.Compiled);

        /// <summary>
        /// The type pattern.
        /// </summary>

        // todo compact pattern.
        private static readonly Regex TypePattern = new Regex(
                                                              @"^(?:(u)?(bedate|bedouble|befloat|beid3|beldate|belong|beqdate|beqldate|bequad|beqwdate|beshort|bestring16|byte|clear|date|default|der|double|float|indirect|ldate|ledate|ledouble|lefloat|leid3|leldate|lelong|leqdate|leqldate|lequad|leqwdate|leshort|lestring16|long|medate|meldate|melong|name|pstring|qdate|qldate|quad|qwdate|regex(?:\/(?:([csl)]+)?([0-9a-fA-Fx]+)?))?|short|(?:string|search)(?:\/(?:([WwcCtbT]+)?([0-9a-fA-Fx]+)?))?|use))([+\-*\/%&|^])?([0-9a-fA-Fx]+)?",
                                                              RegexOptions.Compiled | RegexOptions.IgnoreCase);

        private static readonly string UNKNOWN_NAME = "unknown";

        private readonly ILogger<MagicEntryParser> _logger;

        public MagicEntryParser(ILogger<MagicEntryParser> logger)
        {
            _logger = logger;
        }

        /// <summary>
        /// Parses one line in a magic rule file.
        /// </summary>
        /// <param name="previous">The previous magic.</param>
        /// <param name="line">The line to parse.</param>
        /// <returns>The parsed <see cref="MagicEntry"/>.</returns>
        public MagicEntry ParseLine(MagicEntry previous, string line)
        {
            _logger.LogTrace("Line to parse: {Line}", line);
            if (line.StartsWith("!:", StringComparison.Ordinal))
            {
                if (previous != null)
                {
                    HandleSpecial(previous, line);
                }

                return null;
            }

            var parts = SplitLine(line);
            if (parts == null)
            {
                _logger.LogError("Invalid Line.");
                return null;
            }

            var offsetMatch = OffsetPattern.Match(parts[0]);
            var typeMatch = TypePattern.Match(parts[1]);

            if (!offsetMatch.Success)
            {
                _logger.LogWarning("Offset matching failed: {data}", parts[0]);
                return null;
            }

            if (!typeMatch.Success)
            {
                _logger.LogWarning("Type matching failed: {data}", parts[1]);
                return null;
            }

            var magicEntry = new MagicEntry();

            if (offsetMatch.Groups[10].Success)
                magicEntry.Offset.Indirection = new Indirection();

            magicEntry.Level = offsetMatch.Groups[1].Length;

            magicEntry.Offset.Value = IntHelper.Decode(offsetMatch.Groups[4].Value);

            if (typeMatch.Groups[2].Value.Contains("/"))
            {
                magicEntry.DataType.Type = TypeHelper.TypeToEnum(typeMatch.Groups[2].Value.Substring(0, typeMatch.Groups[2].Value.IndexOf("/", StringComparison.Ordinal)));
            }
            else
            {
                magicEntry.DataType.Type = TypeHelper.TypeToEnum(typeMatch.Groups[2].Value);
            }

            if (offsetMatch.Groups[2].Success)
            {
                _logger.LogTrace("Offset: Relative");
                magicEntry.Offset.Realative = true;
            }

            if (offsetMatch.Groups[3].Success)
            {
                _logger.LogTrace("Offset: Indirection: Relative");
                magicEntry.Offset.Indirection.Relative = true;
            }

            if (offsetMatch.Groups[5].Success)
            {
                var indirectionUnsigned = offsetMatch.Groups[5].Value[0] == '.';
                _logger.LogTrace("Offset: Indirection: Unsigned: {type}", indirectionUnsigned);
                magicEntry.Offset.Indirection.Unsigned = indirectionUnsigned;
            }

            if (offsetMatch.Groups[6].Success)
            {
                var indirectionType = offsetMatch.Groups[6].Value[0];
                _logger.LogTrace("Offset: Indirection: Type: {type}", indirectionType);
                magicEntry.Offset.Indirection.Type = indirectionType;
            }

            if (offsetMatch.Groups[8].Success && offsetMatch.Groups[9].Success)
            {
                var indirectionOp = (Operators)offsetMatch.Groups[8].Value[0];
                _logger.LogTrace("Offset: Indirection: Op: {operator}", indirectionOp);
                magicEntry.Offset.Indirection.Op = indirectionOp;

                var value = offsetMatch.Groups[9].Value;
                _logger.LogTrace("Offset: Indirection: Value: {value}", value);
                magicEntry.Offset.Indirection.ModifierValue = IntHelper.Decode(value);
            }
            else if (offsetMatch.Groups[8].Success ^ offsetMatch.Groups[9].Success)
            {
                _logger.LogWarning("Offset: Indirection: missing op or value");
            }

            if (!TypeHelper.IS_STRING_OR_SPECIAl(magicEntry.DataType.Type))
            {
                var unsigned = typeMatch.Groups[1].Success;
                _logger.LogTrace("DataType: Unsigned: {value}", unsigned);
                magicEntry.DataType.Unsigned = unsigned;

                if (typeMatch.Groups[7].Success && typeMatch.Groups[8].Success)
                {
                    var dataTypeOp = (Operators)typeMatch.Groups[7].Value[0];
                    _logger.LogTrace("DataType: Op: {operator}", dataTypeOp);
                    magicEntry.DataType.Op = dataTypeOp;

                    var value = typeMatch.Groups[8].Value;
                    _logger.LogTrace("DataType: ModifierValue: {value}", value);
                    magicEntry.DataType.ModifierValue = IntHelper.Decode(value);
                }
                else if (typeMatch.Groups[7].Success ^ typeMatch.Groups[8].Success)
                {
                    _logger.LogWarning("DataType: missing op or value");
                }
            }
            else
            {
                if (parts[1].Length > typeMatch.Length)
                {
                    var flags = parts[1].Substring(parts[1].IndexOf("/", StringComparison.Ordinal) + 1);
                    _logger.LogTrace("DataType: Flags: {flags}", flags);

                    int pos = 0;

                    string num = string.Empty;
                    while (pos < flags.Length && !char.IsWhiteSpace(flags[pos]))
                    {
                        switch (flags[pos])
                        {
                            case 'B':
                            case 'H':
                            case 'h':
                            case 'L':
                            case 'l':
                                if ((magicEntry.DataType.Type == ValueTypes.PSTRING))
                                {
                                    //pMatcher1.Type = flags[pos];
                                }

                                else if (magicEntry.DataType.Type == ValueTypes.REGEX)
                                    magicEntry.PatternInfo.ByteOrLine = true;

                                break;
                            case 'J':
                                if (magicEntry.DataType.Type == ValueTypes.PSTRING)
                                {
                                    //pMatcher2.IncludeLengthInSize = true;
                                }

                                break;

                            case '0':
                            case '1':
                            case '2':
                            case '3':
                            case '4':
                            case '5':
                            case '6':
                            case '7':
                            case '8':
                            case '9':
                            case 'x':
                                num += flags[pos];
                                break;

                            case 'W':
                                magicEntry.StrFlags |= Flags.STRING_COMPACT_WHITESPACE;
                                break;

                            case 'w':
                                magicEntry.StrFlags |= Flags.STRING_COMPACT_OPTIONAL_WHITESPACE;
                                break;

                            case 'c':
                                magicEntry.StrFlags |= Flags.STRING_IGNORE_LOWERCASE;
                                break;

                            case 'C':
                                magicEntry.StrFlags |= Flags.STRING_IGNORE_UPPERCASE;
                                break;

                            case 's':
                                magicEntry.StrFlags |= Flags.REGEX_OFFSET_START;
                                break;

                            case 'b':
                                magicEntry.StrFlags |= Flags.STRING_BINTEST;
                                break;

                            case 't':
                                magicEntry.StrFlags |= Flags.STRING_TEXTTEST;
                                break;

                            case 'T':
                                magicEntry.StrFlags |= Flags.STRING_TRIM;
                                break;
                        }

                        pos++;
                    }

                    if (num.Length != 0)
                        magicEntry.PatternInfo.ScanSize = (int)IntHelper.Decode(num);
                }
            }

            // process the test-string
            var testStr = parts[2];

            HandleTest(testStr, magicEntry);

            if (parts.Length == 3)
            {
                magicEntry.Name = UNKNOWN_NAME;
            }
            else
            {
                HandleFormat(parts[3], magicEntry);
            }

            return magicEntry;
        }

        private static int FindNonWhitespace(string line, int startPos)
        {
            for (var pos = startPos; pos < line.Length; pos++)
            {
                if (!char.IsWhiteSpace(line[pos]))
                {
                    return pos;
                }
            }

            return -1;
        }

        private static int FindWhitespaceWithoutEscape(string line, int startPos)
        {
            var lastEscape = false;
            for (var pos = startPos; pos < line.Length; pos++)
            {
                var ch = line[pos];
                if (ch == ' ')
                {
                    if (!lastEscape)
                    {
                        return pos;
                    }

                    lastEscape = false;
                }
                else if (char.IsWhiteSpace(ch))
                {
                    return pos;
                }
                else if (ch == '\\')
                {
                    lastEscape = true;
                }
                else
                {
                    lastEscape = false;
                }
            }

            return -1;
        }

        private static void HandleFormat(string format, MagicEntry magicEntry)
        {
            // a starting \\b or ^H means don't prepend a space when chaining content details
            if (format.StartsWith("\\b"))
            {
                format = format.Substring(2);
                magicEntry.FormatSpacePrefix = false;
            }
            else if (format.StartsWith("\010"))
            {
                // NOTE: sometimes the \b is expressed as a ^H character (grumble)
                format = format.Substring(1);
                magicEntry.FormatSpacePrefix = false;
            }
            else if (format.StartsWith("\\r"))
            {
                format = format.Substring(2);
                magicEntry.ClearFormat = true;
            }

            magicEntry.Formatter = new MagicFormatter(format);

            var trimmedFormat = format.Trim();
            var spaceIndex = trimmedFormat.IndexOf(' ');
            if (spaceIndex < 0)
            {
                spaceIndex = trimmedFormat.IndexOf('\t');
            }

            if (spaceIndex > 0)
            {
                magicEntry.Name = trimmedFormat.Substring(0, spaceIndex);
            }
            else if (trimmedFormat.Length == 0)
            {
                magicEntry.Name = UNKNOWN_NAME;
            }
            else
            {
                magicEntry.Name = trimmedFormat;
            }
        }

        /// <summary>
        /// Handle a special line.
        /// </summary>
        /// <param name="previous">The previous magic.</param>
        /// <param name="line">The line to handle.</param>
        /// <exception cref="Exception"></exception>
        private static void HandleSpecial(MagicEntry previous, string line)
        {
            if (line.Equals(OPTIONAL_LINE))
            {
                previous.IsOptional = true;
                return;
            }

            var startPos = FindNonWhitespace(line, 0);
            var index = FindWhitespaceWithoutEscape(line, startPos);
            if (index < 0)
            {
                throw new Exception("invalid extension line has less than 2 whitespace separated fields", null);
            }

            var key = line.Substring(startPos, index - startPos);
            startPos = FindNonWhitespace(line, index);
            if (startPos < 0)
            {
                throw new Exception("invalid extension line has less than 2 whitespace separated fields", null);
            }

            // find whitespace after value, if any
            index = FindWhitespaceWithoutEscape(line, startPos);
            if (index < 0)
            {
                index = line.Length;
            }

            var value = line.Substring(startPos, index - startPos);

            if (key.Equals(MIME_TYPE_LINE))
            {
                previous.MimeType = value;
            }

            var result = StrengthPattern.Match(line);

            if (!result.Success) return;

            var operation = result.Groups[1].Value[0];
            var offset = (uint)IntHelper.Decode(result.Groups[2].Value);
            previous.Modifiers.StrengthMod = new Tuple<char, uint>(operation, offset);
        }

        private static void HandleTest(string testStr, MagicEntry magicEntry)
        {
            if (testStr.Equals("x", StringComparison.Ordinal))
            {
                magicEntry.Test.Op = Operators.MatchAll;
                magicEntry.Test.Value = null;
                return;
            }

            bool hasModifier = Regex.IsMatch(testStr.Substring(0, 1), "[><&^=!]", RegexOptions.Compiled);

            if (TypeHelper.IS_STRING_OR_SPECIAl(magicEntry.DataType.Type))
            {
                testStr = StringType.preProcessPattern(testStr);

                if (hasModifier)
                {
                    magicEntry.Test.Op = (Operators)testStr[0];
                    magicEntry.Test.Value = new StringValue(testStr.Substring(1));
                }
                else
                {
                    magicEntry.Test.Op = Operators.Equal;
                    magicEntry.Test.Value = new StringValue(testStr);
                }
            }
            else if (TypeHelper.IS_DOUBLE(magicEntry.DataType.Type))
            {
                if (hasModifier)
                {
                    magicEntry.Test.Op = (Operators)testStr[0];
                    magicEntry.Test.Value = new Number(double.Parse(testStr.Substring(1), CultureInfo.InvariantCulture));
                }
                else
                {
                    magicEntry.Test.Op = Operators.Equal;
                    magicEntry.Test.Value = new Number(double.Parse(testStr, CultureInfo.InvariantCulture));
                }
            }
            else
            {
                if (hasModifier)
                {
                    magicEntry.Test.Op = (Operators)testStr[0];
                    magicEntry.Test.Value = new Number(IntHelper.Decode(testStr.Substring(1)));
                }
                else
                {
                    magicEntry.Test.Op = Operators.Equal;
                    magicEntry.Test.Value = new Number(IntHelper.Decode(testStr));
                }
            }
        }

        /// <summary> Splits the line into level|offset, type, test and output text. </summary>
        /// <param name="line">The line.</param> <returns>level&offset, type, test and output
        /// text</returns> <exception cref="System.Exception"> invalid number of whitespace separated
        /// fields, must be >= 4 or invalid number of whitespace separated fields, must be >= 4 or
        /// invalid number of whitespace separated fields, must be >= 4 or invalid number of
        /// whitespace separated fields, must be >= 4 </exception>
        private static string[] SplitLine(string line)
        {
            // skip opening whitespace if any
            var startPos = FindNonWhitespace(line, 0);
            if (startPos < 0)
            {
                return null;
            }

            // find the level info
            var endPos = FindWhitespaceWithoutEscape(line, startPos);
            if (endPos < 0)
            {
                throw new Exception("invalid number of whitespace separated fields, must be >= 4", null);
            }

            var levelStr = line.Substring(startPos, endPos - startPos);

            // skip whitespace
            startPos = FindNonWhitespace(line, endPos + 1);
            if (startPos < 0)
            {
                throw new Exception("invalid number of whitespace separated fields, must be >= 4", null);
            }

            // find the type string
            endPos = FindWhitespaceWithoutEscape(line, startPos);
            if (endPos < 0)
            {
                throw new Exception("invalid number of whitespace separated fields, must be >= 4", null);
            }

            var typeStr = line.Substring(startPos, endPos - startPos);

            // skip whitespace
            startPos = FindNonWhitespace(line, endPos);
            if (startPos < 0)
            {
                throw new Exception("invalid number of whitespace separated fields, must be >= 4", null);
            }

            // find the test string
            endPos = FindWhitespaceWithoutEscape(line, startPos);
            if (endPos < 0)
            {
                endPos = line.Length;
            }

            var testStr = line.Substring(startPos, endPos - startPos);

            // skip any whitespace
            startPos = FindNonWhitespace(line, endPos + 1);

            // format is optional, this could return length
            if (startPos < 0)
            {
                return new[] { levelStr, typeStr, testStr };
            }

            return new[] { levelStr, typeStr, testStr, line.Substring(startPos) };
        }
    }
}