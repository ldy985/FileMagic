using System.Diagnostics.CodeAnalysis;
using System.IO;

namespace ldy985.FileMagic.Abstracts
{
    /// <summary>
    ///     A Rule is a structure that contains everything needed to detect a given type of data.
    /// </summary>
    public interface IRule
    {
        /// <summary>
        ///     Name of the rule.
        /// </summary>
        string Name { get; }

        /// <summary>
        ///     The magic signature. Null if none.
        /// </summary>
        IMagic? Magic { get; }

        /// <summary>
        ///     Contains basic information about the structure identified in the stream.
        /// </summary>
        ITypeInfo TypeInfo { get; }

        /// <summary>
        ///     True if the <see cref="IRule" /> has a known magic signature.
        /// </summary>
        bool HasMagic { get; }

        /// <summary>
        ///     True if the <see cref="IRule" /> has an implementation of parsing.
        /// </summary>
        bool HasParser { get; }

        /// <summary>
        ///     True if the <see cref="IRule" /> has an implementation of any structural checks.
        /// </summary>
        bool HasStructure { get; }

        /// <summary>
        ///     Tries to use the <see cref="IRule" /> to parse the data in the steam.
        /// </summary>
        /// <param name="reader">The reader with the data.</param>
        /// <param name="result">Any results from the parsing that may help determine which specific type we are looking at.</param>
        /// <param name="parsed">Any parsed data from the stream.</param>
        /// <returns></returns>
        bool TryParse(BinaryReader reader, ref IResult result, [NotNullWhen(true)] out IParsed? parsed);

        /// <summary>
        ///     Tries to run basic structure checks from the <see cref="IRule" /> with the data in the steam.
        /// </summary>
        /// <param name="reader">The reader with the data.</param>
        /// <param name="result">Any results from the structure that may help determine which specific type we are looking at.</param>
        /// <returns></returns>
        bool TryStructure(BinaryReader reader, ref IResult result);

        /// <summary>
        ///     Tries to match the magic in the <see cref="IRule" /> with the data in the steam.
        /// </summary>
        /// <param name="stream"></param>
        /// <returns>True if the stream matches the expected magic.</returns>
        bool TryMagic(Stream stream);
    }
}