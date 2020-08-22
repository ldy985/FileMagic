namespace ldy985.FileMagic.Abstracts
{
    public interface IRuleProvider
    {
        T Get<T>() where T : IRule;
        IRule[] PatternRules { get; }
        IRule[] ParserRules { get; }
        IRule[] StructureRules { get; }
        IRule[] Rules { get; }
        IRule[] ComplexRulesOnly { get; }
    }
}