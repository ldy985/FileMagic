using System.IO;

namespace ldy985.FileMagic.Abstracts
{
    public interface IFileMagic
    {
        bool IdentifyStream(Stream stream, out IResult result, ref IMetaData metaData);
        bool IdentifyStream(Stream stream, out IResult result);
        bool StreamMatches<T>(Stream stream, out IResult result) where T : IRule;
    }
}