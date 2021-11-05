using System.Diagnostics.CodeAnalysis;
using System.IO;

namespace ldy985.FileMagic.Core.Rules.Tests.Utils
{
    public static class Utilities
    {
        public static string BasePath(uint id)
        {
            return $"../../../../../resources/test{id}.";
        }


        [SuppressMessage("IDisposableAnalyzers.Correctness", "IDISP001:Dispose created.", Justification = "<Pending>")]
        public static Stream GetFileSteam(string filePath)
        {
            FileStream fileStream = new FileStream(filePath, FileMode.Open, FileAccess.Read, FileShare.Read, 4096,
                FileOptions.SequentialScan);

            return new BufferedStream(fileStream, 8096);
        }

        public static BinaryReader GetReader(string filePath)
        {
            return new BinaryReader(GetFileSteam(filePath));
        }
    }
}