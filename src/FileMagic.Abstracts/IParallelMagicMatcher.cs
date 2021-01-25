using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.IO;

namespace ldy985.FileMagic.Abstracts
{
    public interface IParallelMagicMatcher
    {
        bool TryFind([NotNull]BinaryReader br, in IMetaData metaData, [NotNullWhen(true)]out IEnumerable<IRule>? matchedRules);
        bool TryFind([NotNull]BinaryReader br, [NotNullWhen(true)]out IEnumerable<IRule>? matchedRules);
    }
}