using System;
using System.IO;
using ldy985.FileMagic.Abstracts;

namespace ldy985.FileMagic.Core.Extensions
{
    public static class RuleExtensions
    {
        public static bool TryMagic(this IRule rule, Stream stream)
        {
            long streamPosition = stream.Position;
            long magicOffset = (long) rule.Magic.Offset;
            ReadOnlySpan<byte?> magic = rule.Magic.MagicBytes.Value;

            if (streamPosition + magicOffset + (rule.Magic.Pattern.Length >> 1) > stream.Length)
                return false;


            stream.Seek(magicOffset, SeekOrigin.Current);

            int length = magic.Length;
#if NETSTANDARD2_1
            Span<byte> span = stackalloc byte[magic.Length];
            if (stream.Read(span) != length)
                return false;
#else
            byte[] span = new byte[magic.Length];
            if (stream.Read(span, 0, length) != length)
                return false;
#endif


            for (int i = 0; i < length; i++)
            {
                if (magic[i].HasValue && magic[i] != span[i])
                    return false;
            }

            stream.Seek(streamPosition, SeekOrigin.Begin);

            return true;
        }

        public static bool TryMagic(this IRule rule, string filePath)
        {
            using (FileStream? stream = File.OpenRead(filePath))
            {
                long streamPosition = stream.Position;
                long magicOffset = (long) rule.Magic.Offset;
                ReadOnlySpan<byte?> magic = rule.Magic.MagicBytes.Value;

                if (streamPosition + magicOffset + (rule.Magic.Pattern.Length >> 1) > stream.Length)
                    return false;


                stream.Seek(magicOffset, SeekOrigin.Current);

                int length = magic.Length;
#if NETSTANDARD2_1
            Span<byte> span = stackalloc byte[magic.Length];
            if (stream.Read(span) != length)
                return false;
#else
                byte[] span = new byte[magic.Length];
                if (stream.Read(span, 0, length) != length)
                    return false;
#endif

                for (int i = 0; i < length; i++)
                {
                    if (magic[i].HasValue && magic[i] != span[i])
                        return false;
                }

                stream.Seek(streamPosition, SeekOrigin.Begin);

                return true;
            }
        }
    }
}