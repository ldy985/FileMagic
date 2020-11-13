/**
 * Formatter that handles the C %0.2f type formats appropriately. I would have used the {@link java.util.Formatter} but
 * you can't pre-parse those for some stupid reason. Also, I needed this to be compatible with the printf(3) C formats.
 *

 */

using System.Text;
using System.Text.RegularExpressions;

namespace ldy985.FileMagic.File.MagicParser.MagicRule
{
    public class MagicFormatter
    {
        public static string _PATTERN_CHARS = "%bcdeEfFgGiosuxX";

        public static string PATTERN_MODIFIERS = "lqh";

        // NOTE: the backspace is taken care of by checking the format string prefix above
        private static readonly Regex FORMAT_PATTERN = new Regex("([^%]*)(%[-+0-9# ." + PATTERN_MODIFIERS + "]*[" + _PATTERN_CHARS + "])?(.*)", RegexOptions.Compiled);

        private readonly PercentExpression percentExpression;

        private readonly string prefix;

        private readonly string suffix;

        /**
	 * This takes a format string, breaks it up into prefix, %-thang, and suffix.
	 */
        public MagicFormatter(string formatstring)
        {
            var matcher = FORMAT_PATTERN.Match(formatstring);
            if (!matcher.Success)
            {
                // may never get here
                prefix = formatstring;
                percentExpression = null;
                suffix = null;
                return;
            }

            var prefixMatch = matcher.Groups[1].Value;
            var percentMatch = matcher.Groups[2].Value;
            var suffixMatch = matcher.Groups[3].Value;

            if (percentMatch != null && percentMatch.Equals("%%"))
            {
                // we go recursive trying to find the first true % pattern
                var formatter = new MagicFormatter(suffixMatch);
                var sb = new StringBuilder();
                if (prefixMatch != null)
                {
                    sb.Append(prefixMatch);
                }

                sb.Append('%');
                if (formatter.prefix != null)
                {
                    sb.Append(formatter.prefix);
                }

                prefix = sb.ToString();
                percentExpression = formatter.percentExpression;
                suffix = formatter.suffix;
                return;
            }

            if (prefixMatch == null || prefixMatch.Length == 0)
            {
                prefix = null;
            }
            else
            {
                prefix = prefixMatch;
            }

            if (percentMatch == null || percentMatch.Length == 0)
            {
                percentExpression = null;
            }
            else
            {
                percentExpression = new PercentExpression(percentMatch);
            }

            if (suffixMatch == null || suffixMatch.Length == 0)
            {
                suffix = null;
            }
            else
            {
                suffix = suffixMatch.Replace("%%", "%");
            }
        }

        public override string ToString()
        {
            var sb = new StringBuilder();
            if (prefix != null)
            {
                sb.Append(prefix);
            }

            if (percentExpression != null)
            {
                sb.Append(percentExpression);
            }

            if (suffix != null)
            {
                sb.Append(suffix);
            }

            return sb.ToString();
        }
    }
}