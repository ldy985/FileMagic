using System;
using System.Globalization;
using ldy985.FileMagic.Abstracts;

namespace ldy985.FileMagic.Core
{
    /// <inheritdoc />
    public class Magic : IMagic
    {
        /// <inheritdoc />
        public string Pattern { get; }

        private byte?[]? _magicBytes;

        /// <inheritdoc />
        public byte?[] MagicBytes => _magicBytes ??= GetMagicBytes();

        /// <inheritdoc />
        public ulong Offset { get; }

        /// <summary>
        /// <inheritdoc cref="Magic"/>
        /// </summary>
        /// <param name="pattern"><inheritdoc cref="Pattern"/></param>
        /// <param name="offset"><inheritdoc cref="Offset"/></param>
        /// <exception cref="ArgumentException">Throws if <see cref="Pattern is not multiples of two."/></exception>
        public Magic(string pattern, ulong offset = 0)
        {
            if (pattern.Length % 2 != 0)
                throw new ArgumentException("Invalid magic length");

            Pattern = pattern;
            Offset = offset;
        }

        private byte?[] GetMagicBytes()
        {
            string pattern = Pattern;//TODO use ReadOnlySpan when byte.Parse implements it.
            int length = pattern.Length;
            byte?[] data = new byte?[length / 2];
            for (int i = 0; i < length; i += 2)
            {
                string substring = pattern.Substring(i, 2);
                if (substring == "??")
                    data[i >> 1] = null;
                else
                    data[i >> 1] = byte.Parse(substring, NumberStyles.HexNumber, CultureInfo.InvariantCulture);
            }

            return data;
        }
    }
}