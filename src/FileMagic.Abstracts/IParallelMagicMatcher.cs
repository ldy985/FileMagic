using System.Collections.Generic;
using System.IO;

namespace ldy985.FileMagic.Abstracts
{
    public interface IParallelMagicMatcher
    {
        bool TryFind(BinaryReader br, in IMetaData metaData, out IEnumerable<IRule> matchedRules);
        bool TryFind(BinaryReader br, out IEnumerable<IRule> matchedRules);
    }
}