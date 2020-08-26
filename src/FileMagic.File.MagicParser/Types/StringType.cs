using System.Text;

namespace Chronos.Libraries.FileClassifier.Types
{
    public class StringType
    {
        public static int Digit(char value, int radix)
        {
            if (radix <= 0 || radix > 36)
            {
                return -1; // Or throw exception
            }

            if (radix <= 10)
            {
                if (value >= '0' && value < '0' + radix)
                {
                    return value - '0';
                }

                return -1;
            }

            if (value >= '0' && value <= '9')
            {
                return value - '0';
            }

            if (value >= 'a' && value < 'a' + radix - 10)
            {
                return value - 'a' + 10;
            }

            if (value >= 'A' && value < 'A' + radix - 10)
            {
                return value - 'A' + 10;
            }

            return -1;
        }

        /**
	 * Pre-processes the pattern by handling \007 type of escapes and others.
	 */
        public static string preProcessPattern(string pattern)
        {
            var index = pattern.IndexOf('\\');
            if (index < 0)
            {
                return pattern;
            }

            var sb = new StringBuilder();
            for (var pos = 0; pos < pattern.Length; pos++)
            {
                var ch = pattern[pos];
                if (ch != '\\')
                {
                    sb.Append(ch);
                    continue;
                }

                if (pos + 1 >= pattern.Length)
                {
                    // we'll end the pattern with a '\\' char
                    sb.Append(ch);
                    break;
                }

                ch = pattern[++pos];
                switch (ch)
                {
                    case 'b':
                        sb.Append('\b');
                        break;

                    case 'f':
                        sb.Append('\f');
                        break;

                    case 'n':
                        sb.Append('\n');
                        break;

                    case '0':
                    case '1':
                    case '2':
                    case '3':
                    case '4':
                    case '5':
                    case '6':
                    case '7':
                    {
                        int octal = Digit(ch, 8);
                        for (int i = 1; i <= 2 && pos + 1 < pattern.Length; i++)
                        {
                            ch = pattern[pos + 1];
                            int digit = Digit(ch, 8);
                            if (digit >= 0)
                            {
                                octal = octal * 8 + digit;
                                pos++;
                            }
                            else
                            {
                                break;
                            }
                        }

                        sb.Append((char)(octal & 0xff));
                        break;
                    }

                    case 'r':
                        sb.Append('\r');
                        break;

                    case 't':
                        sb.Append('\t');
                        break;

                    case 'a':
                        // sb.Append('\007');
                        break;

                    case 'v':
                        // sb.Append('\013');
                        break;

                    case 'x':
                    {
                        int hex = Digit(pattern[pos + 1], 16);
                        if (hex >= 0)
                        {
                            pos++;
                            if (pos + 1 < pattern.Length)
                            {
                                int digit = Digit(pattern[pos + 1], 16);
                                if (digit >= 0)
                                {
                                    hex = hex * 16 + digit;
                                    pos++;
                                }
                            }

                            sb.Append((char)(hex & 0xff));
                        }
                        else
                        {
                            // if there is no valid hex digit treat \x as x
                            sb.Append(ch);
                        }

                        break;
                    }

                    case ' ':
                    case '\\':
                    default:
                        sb.Append(ch);
                        break;
                }
            }

            var processPattern = sb.ToString();

            processPattern = processPattern
                             .Replace("[:space:]", @" \t\r\n\v\f")
                             .Replace("[:alpha:]", "a-zA-Z")
                             .Replace("[:alnum:]", "a-zA-Z0-9")
                             .Replace("[:digit:]", "0-9")
                             .Replace("[:graph:]", @"\x21-\x7E")
                             .Replace("[:lower:]", "a-z")
                             .Replace("[:punct:]", "!\"#$%&'()*+,-.\\/:;<=>?@[\\]^_`{|}~")
                             .Replace("[:upper:]", "A-Z")
                             .Replace("[:word:]", "A-Za-z0-9_")
                             .Replace("[:print:]", @"\x20-\x7E")
                             .Replace("[:xdigit:]", "A-Fa-f0-9");

            return processPattern;
        }
    }
}