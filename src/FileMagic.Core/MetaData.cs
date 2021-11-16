using System;
using ldy985.FileMagic.Abstracts;

namespace ldy985.FileMagic.Core
{
    /// <inheritdoc cref="ldy985.FileMagic.Abstracts.IMetaData" />
    public readonly struct MetaData : IMetaData, IEquatable<MetaData>
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

        public static bool operator ==(MetaData left, MetaData right)
        {
            return left.Equals(right);
        }

        public static bool operator !=(MetaData left, MetaData right)
        {
            return !(left == right);
        }

        public bool Equals(MetaData other)
        {
            return Extension == other.Extension;
        }

        public override bool Equals(object? obj)
        {
            return obj is MetaData other && Equals(other);
        }

        public override int GetHashCode()
        {
            return Extension.GetHashCode(StringComparison.Ordinal);
        }
    }
}