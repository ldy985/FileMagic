using System.IO;

namespace ldy985.FileMagic.Abstracts
{
    public interface ISingleRuleMatcher : IMatcher
    {
        /// <summary>
        /// TestRule
        /// </summary>
        /// <param name="reader"></param>
        /// <param name="rule"></param>
        /// <returns></returns>
        /// <exception cref="System.ObjectDisposedException"></exception>
        /// <exception cref="IOException"></exception>
        bool TestRule(BinaryReader reader, in IRule rule);
    }

    public interface IMatcher
    {
        public bool IsAuthoritative { get; set; }
    }
}