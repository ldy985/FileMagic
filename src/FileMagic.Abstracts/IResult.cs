using ldy985.FileMagic.Abstracts.Enums;

namespace ldy985.FileMagic.Abstracts
{
    /// <summary>
    /// Describes the results from a <see cref="IRule"/>.
    /// </summary>
    public interface IResult
    {
        /// <summary>
        /// Any extensions that is normal for the type of data.
        /// </summary>
        string[] Extensions { get; set; }

        /// <summary>
        /// A description of the data.
        /// </summary>
        string Description { get; set; }

        /// <summary>
        /// The name of the rule that matched the data.
        /// </summary>
        IRule MatchedRule { get; set; }

        /// <summary>
        /// The types of checks that matched.
        /// </summary>
        MatchTypes MatchedRuleTypes { get; set; }
    }
}