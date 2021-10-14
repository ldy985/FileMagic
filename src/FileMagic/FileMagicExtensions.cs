using System.IO;
using System.Text;
using ldy985.FileMagic.Abstracts;
using ldy985.FileMagic.Core;

namespace ldy985.FileMagic
{
    public static class FileMagicExtensions
    {
        /// <summary>
        /// IdentifyFile
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
        public static bool IdentifyFile(this IFileMagic fileMagic, string filePath, out IResult result)
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

        public static bool IdentifyStream(this IFileMagic fileMagic, Stream stream, out IResult result, in IMetaData metaData)
        {
            result = new Result();

            using BinaryReader binaryReader = new BinaryReader(stream, Encoding.UTF8, true);
            return fileMagic.IdentifyStream(binaryReader, out result, metaData);
        }

        public static bool IdentifyStream(this IFileMagic fileMagic, Stream stream, out IResult result)
        {
            result = new Result();

            using BinaryReader binaryReader = new BinaryReader(stream, Encoding.UTF8, true);
            return fileMagic.IdentifyStream(binaryReader, out result);
        }
    }
}