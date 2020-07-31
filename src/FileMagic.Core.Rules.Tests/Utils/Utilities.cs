using System.IO;
using ldy985.FileMagic.Abstracts;

namespace ldy985.FileMagic.Core.Rules.Tests.Utils
{
    public static class Utilities
    {
        public static string BasePath(uint id) => $"../../../../../resources/test{id}.";

        public static bool MatchMagic(this IRule rule, string filePath)
        {
            byte?[] magic = rule.Magic.MagicBytes.Value;

            int length = magic.Length;
            byte[] bytes = new byte[length];

            using (Stream bs = GetFileSteam(filePath))
            {
                if (bs.Read(bytes, 0, length) != length)
                    return false;
            }

            for (int i = 0; i < magic.Length; i++)
            {
                if (magic[i].HasValue && magic[i] != bytes[i])
                    return false;
            }

            return true;
        }

        public static Stream GetFileSteam(string filePath)
        {
            return new BufferedStream(new FileStream(filePath, FileMode.Open, FileAccess.Read, FileShare.Read, 4096, FileOptions.SequentialScan), 8096);
        }

        public static BinaryReader GetReader(string filePath)
        {
            return new BinaryReader(GetFileSteam(filePath));
        }
    }
}