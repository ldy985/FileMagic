namespace ldy985.FileMagic.Abstracts
{
    public interface IRuleProvider
    {
        /// <summary>
        /// Get a specific rule.
        /// </summary>
        /// <typeparam name="T">The IRule to get.</typeparam>
        /// <returns>The rule.</returns>
        T Get<T>() where T : IRule;

        /// <summary>
        /// Gets all the rules that has magic.
        /// </summary>
        IRule[] PatternRules { get; }

        /// <summary>
        /// Gets all the rules that have a parser.
        /// </summary>
        IRule[] ParserRules { get; }

        /// <summary>
        /// Gets all rules that implement a structure.
        /// </summary>
        IRule[] StructureRules { get; }

        /// <summary>
        /// Gets all rules.
        /// </summary>
        IRule[] Rules { get; }

        /// <summary>
        /// Gets all the rules that do not have a magic.
        /// </summary>
        IRule[] ComplexRulesOnly { get; }
    }
}