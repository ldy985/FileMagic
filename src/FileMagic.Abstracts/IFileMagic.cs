using System.IO;

namespace ldy985.FileMagic.Abstracts
{
    public interface IFileMagic
    {
        /// <summary>
        /// Tries to identify the data in a steam.
        /// </summary>
        /// <param name="stream">The data stream.</param>
        /// <param name="result">The result</param>
        /// <param name="metaData">Any metadata to help the identification.</param>
        /// <returns>True if we found a match.</returns>
        bool IdentifyStream(Stream stream, out IResult result, ref IMetaData metaData);

        /// <summary>
        /// Tries to identify the data in a steam.
        /// </summary>
        /// <param name="stream">The data stream.</param>
        /// <param name="result">The result</param>
        /// <returns>True if we found a match.</returns>
        bool IdentifyStream(Stream stream, out IResult result);

        /// <summary>
        /// Checks if the stream matches with a given rule.
        /// </summary>
        /// <param name="stream">The stream to check.</param>
        /// <param name="result">The result of the check.</param>
        /// <typeparam name="T">The type of rule to check against.</typeparam>
        /// <returns>True if the rule matched the stream</returns>
        bool StreamMatches<T>(Stream stream, out IResult result) where T : IRule;
    }
}