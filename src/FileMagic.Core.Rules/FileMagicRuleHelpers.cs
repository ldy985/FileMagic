using System.Reflection;
using JetBrains.Annotations;
using ldy985.FileMagic.Abstracts;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Logging.Abstractions;

namespace ldy985.FileMagic.Core.Rules
{
    public static class FileMagicRuleHelpers
    {
        public static IEnumerable<IRule> GetDefaultFileMagicRules(ILoggerFactory loggerFactory)
        {
            return CreateRules<IRule>(loggerFactory, typeof(FileMagicBuilderExtensions).Assembly);
        }

        [Pure]
        public static IEnumerable<TRule> CreateRules<TRule>(ILoggerFactory loggerFactory, Assembly? lookInAssembly = null) where TRule : IRule
        {
            foreach (Type type in TypeHelper.GetAllTypesThatImplementInterface<TRule>(lookInAssembly))
            {
                if (type.ContainsGenericParameters)
                    continue;

                Type log = typeof(Logger<>);
                Type genericLogger = log.MakeGenericType(type);
                object logger = Activator.CreateInstance(genericLogger, loggerFactory) ?? throw new MissingMemberException();
                yield return (TRule)(Activator.CreateInstance(type, logger) ?? throw new MissingMemberException());
            }
        }

        [Pure]
        public static TRule CreateRule<TRule>() where TRule : class, IRule
        {
            return CreateRule<TRule>(NullLoggerFactory.Instance);
        }

        [Pure]
        public static TRule CreateRule<TRule>(ILoggerFactory loggerFactory) where TRule : class, IRule
        {
            Type type = typeof(TRule);

            Type log = typeof(Logger<>);
            Type genericLogger = log.MakeGenericType(type);
            object logger = Activator.CreateInstance(genericLogger, loggerFactory) ??
                            throw new ArgumentException("Unable to create logger for rule " + type.Name);

            object rule = Activator.CreateInstance(type, logger) ?? throw new ArgumentException("Unable to create rule " + type.Name);
            return (TRule)rule;
        }
    }
}