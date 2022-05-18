using System;
using System.IO;
using ldy985.FileMagic.Abstracts;

namespace ldy985.FileMagic.Core.Extensions
{
    public static class RuleExtensions
    {
        /// <exception cref="T:System.ArgumentException">Rule must have a magic</exception>
        /// <exception cref="T:System.IO.DirectoryNotFoundException">The specified path is invalid, (for example, it is on an unmapped drive).</exception>
        /// <exception cref="T:System.UnauthorizedAccessException"><paramref name="path" /> specified a directory.
        ///  -or-
        ///  The caller does not have the required permission.</exception>
        /// <exception cref="T:System.IO.FileNotFoundException">The file specified in <paramref name="path" /> was not found.</exception>
        /// <exception cref="T:System.IO.IOException">An I/O error occurred while opening the file.</exception>
        /// <exception cref="T:System.ArgumentNullException"><paramref name="path" /> is <see langword="null" />.</exception>
        /// <exception cref="T:System.NotSupportedException"><paramref name="path" /> is in an invalid format.</exception>
        /// <exception cref="T:System.IO.PathTooLongException">The specified path, file name, or both exceed the system-defined maximum length.</exception>
        public static bool TryMagic(this IRule rule, string filePath)
        {
            if (!rule.HasMagic)
                throw new ArgumentException("Rule must have a magic");

            using (FileStream stream = File.OpenRead(filePath))
            {
                return rule.TryMagic(stream);
            }
        }
    }
}