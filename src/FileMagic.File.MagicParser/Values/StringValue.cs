using System.Text;
using Chronos.Libraries.FileClassifier.Enums;

namespace Chronos.Libraries.FileClassifier.Values
{
    public class StringValue : Value
    {
        public StringValue(string value)
        {
            Value = value;
        }

        /// <summary>
        ///     Gets the string.
        /// </summary>
        public string Value { get; }

        /// <inheritdoc />
        public override byte GetStartingByte()
        {
            return (byte) Value[0];
        }

        /// <summary>
        ///     Get the string.
        /// </summary>
        /// <param name="type">No effect</param>
        /// <param name="unsigned">No effect.</param>
        /// <returns>The string value.</returns>
        public override dynamic GetValue(ValueTypes type, bool unsigned)
        {
            return Value;
        }

        public override byte[] GetBytes()
        {
            return Encoding.ASCII.GetBytes(Value.ToCharArray());
        }

        /// <inheritdoc />
        public override string ToString()
        {
            return Value;
        }
    }
}