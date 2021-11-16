using System.IO;

namespace ldy985.FileMagic.Tests
{
    public class TestFile
    {
        public TestFile(string file)
        {
            Path = file;
        }

        public string Path { get; }

        public string GetExtension()
        {
            string extension = System.IO.Path.GetExtension(Path);

            if (extension.Length == 0)
                return extension;

            return extension[0] == '.' ? extension.Substring(1).ToUpperInvariant() : extension.ToUpperInvariant();
        }

        public override string ToString()
        {
            return Path;
        }
    }
}