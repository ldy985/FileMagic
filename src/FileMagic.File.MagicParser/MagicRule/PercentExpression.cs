/**
 * Representation of our percent expression used by the {@link MagicFormatter} class.
 *

 */

using System;
using System.Text;
using System.Text.RegularExpressions;
using Chronos.Libraries.FileClassifier.entries;
using Chronos.Libraries.FileClassifier.Enums;
using Chronos.Libraries.FileClassifier.Values;

namespace Chronos.Libraries.FileClassifier.MagicRule
{
    
    public class PercentExpression
    {
        private static readonly Regex FORMAT_PATTERN = new Regex("%([0#+ -]*)([0-9]*)(\\.([0-9]+))?([" + MagicFormatter.PATTERN_MODIFIERS + "]*)([" + MagicFormatter._PATTERN_CHARS + "])", RegexOptions.Compiled);

        private static readonly string SPACE_CHARS = "                                                                      ";

        private static readonly string ZERO_CHARS = "00000000000000000000000000000000000000000000000000000000000000000000000";

        /** if we need to choose the shorter of two formats */

        private readonly bool alternativeForm;

        private readonly string expression;

        private readonly bool justValue;

        private readonly bool leftAdjust;

        private readonly char patternChar;

        private readonly bool plusPrefix;

        private readonly bool spacePrefix;

        private readonly int totalWidth;

        private readonly int truncateWidth;

        private readonly bool zeroPrefix;

        private string decimalFormat;

        internal PercentExpression(string expression)
        {
            this.expression = expression;
            var matcher = FORMAT_PATTERN.Match(expression);

            // Matcher matcher = FORMAT_PATTERN.matcher(expression);
            if (!matcher.Success || matcher.Groups[6].Value == null || matcher.Groups[6].Value.Length != 1)
            {
                // may never get here but let's be careful
                justValue = true;
                alternativeForm = false;
                patternChar = '\0';
                zeroPrefix = false;
                plusPrefix = false;
                spacePrefix = false;
                leftAdjust = false;
                totalWidth = -1;
                truncateWidth = -1;
                return;
            }

            justValue = false;

            var flags = matcher.Groups[1].Value;
            alternativeForm = readFlag(flags, '#');
            zeroPrefix = readFlag(flags, '0');
            plusPrefix = readFlag(flags, '+');
            if (plusPrefix)
            {
                spacePrefix = false;
            }
            else
            {
                spacePrefix = readFlag(flags, ' ');
            }

            leftAdjust = readFlag(flags, '-');
            totalWidth = readPrecision(matcher.Groups[2].Value, -1);
            var dotPrecision = readPrecision(matcher.Groups[4].Value, -1);

            // 5 is ignored
            patternChar = matcher.Groups[6].Value[0];
            switch (patternChar)
            {
                case 'e':
                case 'E':
                {
                    this.decimalFormat = ScientificFormat(dotPrecision);
                    break;
                }

                case 'f':
                case 'F':
                {
                    this.decimalFormat = DecimalFormat(dotPrecision);

                    break;
                }

                case 'g':
                case 'G':
                {
                    // will take the shorter of the two
                    this.decimalFormat = DecimalFormat(dotPrecision);
                    break;
                }

                default:

                    // this.decimalFormat = null;
                    // this.altDecimalFormat = null;
                    break;
            }

            if (patternChar == 's' || patternChar == 'b')
            {
                truncateWidth = dotPrecision;
            }
            else
            {
                truncateWidth = -1;
            }
        }

        public override string ToString()
        {
            return expression;
        }

        private static bool readFlag(string flags, char flagChar)
        {
            if (flags != null && flags.IndexOf(flagChar) >= 0)
            {
                return true;
            }

            return false;
        }

        private static int readPrecision(string s, int defaultVal)
        {
            if (s == null || s.Length == 0)
            {
                return defaultVal;
            }

            try
            {
                return int.Parse(s);
            }
            catch (Exception e)
            {
                // ignored
                return defaultVal;
            }
        }

        private void AppendChars(StringBuilder sb, string indentChars, int diff)
        {
            while (true)
            {
                if (diff > indentChars.Length)
                {
                    sb.Append(indentChars);
                    diff -= indentChars.Length;
                }
                else
                {
                    sb.Append(indentChars, 0, diff);
                    break;
                }
            }
        }

        private void AppendHex(StringBuilder sb, bool upper, Number extractedValue)
        {
            var value = extractedValue.GetValue(ValueTypes.QUAD, false);
            string sign = null;
            if (value < 0)
            {
                sign = "-";
                value = -value;
            }

            string prefix = null;
            if (alternativeForm)
            {
                if (upper)
                {
                    prefix = "0X";
                }
                else
                {
                    prefix = "0x";
                }
            }

            var strValue = string.Format("{0:X}", value);

            // var strValue = value.ToString(this.expression);
            if (upper)
            {
                strValue = strValue.ToUpper();
            }

            this.AppendValue(sb, sign, prefix, strValue, true);
        }

        private void AppendValue(StringBuilder sb, string sign, string prefix, string value, bool isNumber)
        {
            var len = 0;
            if (sign != null)
            {
                len += sign.Length;
            }

            if (prefix != null)
            {
                len += prefix.Length;
            }

            len += value.Length;
            var diff = totalWidth - len;
            if (diff < 0)
            {
                diff = 0;
            }

            if (!leftAdjust)
            {
                if (isNumber && zeroPrefix)
                {
                    if (sign != null)
                    {
                        sb.Append(sign);
                        sign = null;
                    }

                    if (prefix != null)
                    {
                        // may never get here
                        sb.Append(prefix);
                        prefix = null;
                    }

                    AppendChars(sb, ZERO_CHARS, diff);
                }
                else
                {
                    AppendChars(sb, SPACE_CHARS, diff);
                }
            }

            if (sign != null)
            {
                sb.Append(sign);
            }

            if (prefix != null)
            {
                sb.Append(prefix);
            }

            sb.Append(value);
            if (leftAdjust)
            {
                AppendChars(sb, SPACE_CHARS, diff);
            }
        }

        /**
	 * -d.ddd+-dd style, if no precision then 6 digits, 'inf', nan', if 0 precision then ""
	 */
        private string DecimalFormat(int fractionPrecision)
        {
            string format;
            if (fractionPrecision == 0)
            {
                format = ("###0");
            }
            else if (fractionPrecision > 0)
            {
                StringBuilder formatSb = new StringBuilder();
                formatSb.Append("###0.");
                AppendChars(formatSb, ZERO_CHARS, fractionPrecision);
                format = (formatSb.ToString());
            }
            else
            {
                format = ("###0.###");
            }

            return format;
        }

        private string ScientificFormat(int fractionPrecision)
        {
            string format;
            if (fractionPrecision == 0)
            {
                format = "0E0";
            }
            else if (fractionPrecision > 0)
            {
                StringBuilder formatSb = new StringBuilder();
                formatSb.Append("0.");
                AppendChars(formatSb, ZERO_CHARS, fractionPrecision);
                formatSb.Append("E0");
                format = (formatSb.ToString());
            }
            else
            {
                format = ("0.###E0");
            }

            return format;
        }
    }
}