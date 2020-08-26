using System;
using Chronos.Libraries.FileClassifier.Enums;

namespace Chronos.Libraries.FileClassifier.Helpers
{
    public static class IntHelper
    {
        /// <summary>
        /// Decodes a string containing either a hex, octal or decimal value.
        /// </summary>
        /// <param name="input">The string to convert.</param>
        /// <returns>The parsed value <see cref="long"/>.</returns>
        /// <exception cref="Exception">Zero length input.</exception>
        public static long Decode(string input)
        {
            var radix = 10;
            var index = 0;
            var negative = false;
            long result;

            if (input.Length == 0)
            {
                throw new Exception("Zero length string");
            }

            var firstChar = input[0];

            // Handle sign, if present
            if (firstChar == '-')
            {
                negative = true;
                index++;
            }
            else if (firstChar == '+')
            {
                index++;
            }

            // Handle radix specifier, if present
            if (input.Substring(index).StartsWith("0x", StringComparison.OrdinalIgnoreCase))
            {
                index += 2;
                radix = 16;
            }
            else if (input.Substring(index).StartsWith("#", StringComparison.Ordinal))
            {
                index++;
                radix = 16;
            }
            else if (input.Substring(index).StartsWith("0", StringComparison.Ordinal) && input.Length > 1 + index)
            {
                index++;
                radix = 8;
            }

            if (input.Substring(index).StartsWith("-", StringComparison.Ordinal) || input.Substring(index).StartsWith("+", StringComparison.Ordinal))
            {
                throw new Exception("Sign character in wrong position");
            }

            try
            {
                result = Convert.ToInt64(input.Substring(index), radix);
                result = negative ? Convert.ToInt64(-result) : result;
            }
            catch (Exception)
            {
                // If number is Integer.MIN_VALUE, we'll end up here. The next line handles this
                // case, and causes any genuine format error to be rethrown.
                var constant = negative ? "-" + input.Substring(index) : input.Substring(index);
                result = Convert.ToInt64(constant, radix);
            }

            return result;
        }

        public static dynamic DoOperation(Operators op, dynamic val1, dynamic val2)
        {
            dynamic val1Value = null;
            if (op == Operators.And)
            {
                val1Value = val1 & val2;
            }

            if (op == Operators.Add)
            {
                val1Value = val1 + val2;
            }

            if (op == Operators.Subtract)
            {
                val1Value = val1 - val2;
            }

            if (op == Operators.Multiply)
            {
                val1Value = val1 * val2;
            }

            if (op == Operators.Divide)
            {
                val1Value = val1 / val2;
            }

            if (op == Operators.Modolu)
            {
                val1Value = val1 % val2;
            }

            if (op == Operators.Or)
            {
                val1Value = val1 | val2;
            }

            if (op == Operators.XOR)
            {
                val1Value = val1 ^ val2;
            }

            if (op == Operators.Complement)
            {
                val1Value = ~val1;
            }

            return val1Value;
        }
    }
}