using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.IO;

namespace ldy985.FileMagic.Abstracts
{
    /// <summary>
    ///     A pattern matcher able to match multiple rules on a stream.
    /// </summary>
    public interface IParallelMagicMatcher
    {
        /// <summary>
        ///     Tries to find any <see cref="IRule" /> with a pattern that matches the stream.
        /// </summary>
        /// <param name="br">The data.</param>
        /// <param name="metaData">Any metadata that can speed up the matching process.</param>
        /// <param name="matchedRules">The rules that matched.</param>
        /// <returns></returns>
        bool TryFind(BinaryReader br, in IMetaData metaData, [NotNullWhen(true)] out IEnumerable<IRule>? matchedRules);

        /// <summary>
        ///     Tries to find any <see cref="IRule" /> with a pattern that matches the stream.
        /// </summary>
        /// <param name="br">The data.</param>
        /// <param name="matchedRules">The rules that matched.</param>
        /// <returns></returns>
        bool TryFind(BinaryReader br, [NotNullWhen(true)] out IEnumerable<IRule>? matchedRules);
    }
}