namespace ldy985.FileMagic.Abstracts.Enums
{
    public enum Quality
    {
        /// <summary>
        /// No idea about the overall quality of the rule.
        /// </summary>
        None,

        /// <summary>
        /// Basic rule implementation unknown amount of false positives.
        /// </summary>
        Low,

        /// <summary>
        /// Rule implementation with some false positives. Based on random sources.
        /// </summary>
        Medium,

        /// <summary>
        /// Rule implementation with few false positives. Based on statistical analysis or partial specification.
        /// </summary>
        High,

        /// <summary>
        /// Rule implementation with no false positives. Based on specification.
        /// </summary>
        VeryHigh,

        /// <summary>
        /// Full rule implementation with no false positives. Based on full specification with structure checks.
        /// </summary>
        Best,
    }
}