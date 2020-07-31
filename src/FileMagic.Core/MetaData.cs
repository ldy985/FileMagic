using ldy985.FileMagic.Abstracts;

namespace ldy985.FileMagic.Core
{
    public readonly struct MetaData : IMetaData
    {
        public MetaData(string extension)
        {
            Extension = extension[0] == '.' ? extension.Substring(1).ToUpperInvariant() : extension.ToUpperInvariant();
        }

        public string Extension { get; }
    }
}