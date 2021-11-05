using System;
using System.Collections.Generic;
using System.Linq;
using ldy985.FileMagic.Abstracts;

namespace ldy985.FileMagic.Core
{
    public class RuleProvider : IRuleProvider
    {
        private readonly Dictionary<Type, IRule> _rules = new Dictionary<Type, IRule>();

        public RuleProvider(IEnumerable<IRule> rules)
        {
            foreach (IRule rule in rules)
                _rules.Add(rule.GetType(), rule);

            Rules = _rules.Values.ToArray();
            PatternRules = _rules.Values.Where(rule => rule.HasMagic).ToArray();
            ParserRules = _rules.Values.Where(rule => rule.HasParser).ToArray();
            StructureRules = _rules.Values.Where(rule => rule.HasStructure).ToArray();
            ComplexRulesOnly = _rules.Values.Where(rule => !rule.HasMagic && (rule.HasStructure || rule.HasParser)).ToArray();
        }

        /// <inheritdoc />
        public T Get<T>() where T : IRule
        {
            return (T)_rules[typeof(T)];
        }

        /// <inheritdoc />
        public IRule[] PatternRules { get; }

        /// <inheritdoc />
        public IRule[] ParserRules { get; }

        /// <inheritdoc />
        public IRule[] StructureRules { get; }

        /// <inheritdoc />
        public IRule[] Rules { get; }

        /// <inheritdoc />
        public IRule[] ComplexRulesOnly { get; }
    }
}