using System;
using ldy985.FileMagic.Abstracts;

namespace ldy985.FileMagic.Core
{
    public class Magic : IMagic
    {
        public string Pattern { get; }

        public Lazy<byte?[]> MagicBytes { get; }

        public ulong Offset { get; }

        public Magic(string pattern, ulong offset = 0)
        {
            if (pattern.Length % 2 != 0)
                throw new ArgumentException("Invalid magic length");

            Pattern = pattern;
            Offset = offset;

            MagicBytes = new Lazy<byte?[]>(() =>
            {
                string bytes = Pattern;

                byte?[] data = new byte?[bytes.Length / 2];
                for (int i = 0; i < bytes.Length; i += 2)
                {
                    string substring = bytes.Substring(i, 2);
                    if (substring == "??")
                        data[i >> 1] = null;
                    else
                        data[i >> 1] = Convert.ToByte(substring, 16);
                }

                return data;
            });
        }
    }
}