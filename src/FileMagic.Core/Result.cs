using ldy985.FileMagic.Abstracts;
using ldy985.FileMagic.Abstracts.Enums;

namespace ldy985.FileMagic.Core
{
    /// <inheritdoc />
    public class Result : IResult
    {
        /// <inheritdoc />
        public string[] Extensions { get; set; }

        /// <inheritdoc />
        public string Description { get; set; }

        /// <inheritdoc />
        public IRule MatchedRule { get; set; }

        /// <inheritdoc />
        public MatchTypes MatchedRuleTypes { get; set; }
    }
}