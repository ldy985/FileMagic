using System.Collections.Generic;

namespace ldy985.FileMagic.Abstracts
{
    public interface IRuleProvider
    {
        T Rent<T>() where T : IRule;
        void Return<T>(T rule) where T : IRule;
        IRule[] PatternRules { get; }
        IRule[] ParserRules { get; }
        IRule[] StructureRules { get; }
        IRule[] Rules { get; }
        IRule[] ComplexRulesOnly { get; }
    }
}