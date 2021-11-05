using ldy985.FileMagic.Abstracts;

namespace ldy985.FileMagic.Core
{
    /// <inheritdoc />
    public readonly struct MetaData : IMetaData
    {
        /// <summary>
        ///     The extension, with or without the dot.
        /// </summary>
        /// <param name="extension"></param>
        public MetaData(string extension)
        {
            Extension = extension[0] == '.' ? extension.Substring(1).ToUpperInvariant() : extension.ToUpperInvariant();
        }

        /// <inheritdoc />
        public string Extension { get; }
    }
}