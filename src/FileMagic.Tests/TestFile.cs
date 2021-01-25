using System.IO;

namespace ldy985.FileMagic.Tests
{
    public class TestFile
    {
        private readonly string _file;

        public TestFile(string file)
        {
            _file = file;
        }

        public string GetPath()
        {
            return _file;
        }

        public string? GetExtension()
        {
            string? extension = Path.GetExtension(_file);

            if (extension?.Length == 0)
                return extension;

            if (extension == null)
                return null;

            return extension[0] == '.' ? extension.Substring(1).ToUpperInvariant() : extension.ToUpperInvariant();
        }

        public override string ToString()
        {
            return _file;
        }
    }
}