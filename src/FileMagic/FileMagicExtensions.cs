using System.Diagnostics.CodeAnalysis;
using System.IO;
using System.Text;
using ldy985.FileMagic.Abstracts;
using ldy985.FileMagic.Core;

namespace ldy985.FileMagic
{
    public static class FileMagicExtensions
    {
        /// <summary>
        ///     IdentifyFile
        /// </summary>
        /// <param name="fileMagic"></param>
        /// <param name="filePath"></param>
        /// <param name="result"></param>
        /// <returns></returns>
        /// <exception cref="PathTooLongException"></exception>
        /// <exception cref="DirectoryNotFoundException"></exception>
        /// <exception cref="System.UnauthorizedAccessException"></exception>
        /// <exception cref="FileNotFoundException"></exception>
        /// <exception cref="IOException"></exception>
        /// <exception cref="T:System.ArgumentException">.NET Framework and .NET Core versions older than 2.1: <paramref name="path" /> is a zero-length string, contains only white space, or contains one or more invalid characters. You can query for invalid characters by using the <see cref="M:System.IO.Path.GetInvalidPathChars" /> method.</exception>
        /// <exception cref="T:System.NotSupportedException"><paramref name="path" /> is in an invalid format.</exception>
        /// <exception cref="T:System.ArgumentNullException"><paramref name="path" /> is <see langword="null" />.</exception>
        public static bool IdentifyFile(this IFileMagic fileMagic, string filePath, [NotNullWhen(true)] out IResult? result)
        {
            using (FileStream fileStream = File.OpenRead(filePath))
            {
                string extension = Path.GetExtension(filePath);
                if (string.IsNullOrEmpty(extension))
                    return fileMagic.IdentifyStream(fileStream, out result);

                IMetaData helpingData = new MetaData(extension);
                return fileMagic.IdentifyStream(fileStream, out result, in helpingData);
            }
        }

        public static bool IdentifyStream(this IFileMagic fileMagic, Stream stream, [NotNullWhen(true)] out IResult? result, in IMetaData metaData)
        {
            using BinaryReader binaryReader = new BinaryReader(stream, Encoding.UTF8, true);
            return fileMagic.IdentifyStream(binaryReader, out result, metaData);
        }

        public static bool IdentifyStream(this IFileMagic fileMagic, Stream stream, [NotNullWhen(true)] out IResult? result)
        {
            using BinaryReader binaryReader = new BinaryReader(stream, Encoding.UTF8, true);
            return fileMagic.IdentifyStream(binaryReader, out result);
        }

        public static bool StreamMatches<T>(this IFileMagic fileMagic, Stream stream, [NotNullWhen(true)] out IResult? result) where T : IRule
        {
            using BinaryReader binaryReader = new BinaryReader(stream, Encoding.UTF8, true);
            return fileMagic.StreamMatches<T>(binaryReader, out result);
        }
    }
}