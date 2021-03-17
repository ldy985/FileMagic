using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using JetBrains.Annotations;
using ldy985.FileMagic.Abstracts;
using ldy985.FileMagic.Core.Rules.Rules;
using Microsoft.Extensions.Logging;

namespace ldy985.FileMagic.Core.Rules
{
    public static class FileMagicRuleHelpers
    {
        public static IEnumerable<IRule> GetDefaultFileMagicRules(ILoggerFactory loggerFactory)
        {
            return CreateRules<IRule>(loggerFactory, typeof(FileMagicBuilderExtensions).Assembly);
        }

        [NotNull]
        [ItemNotNull]
        [Pure]
        public static IEnumerable<TRule> CreateRules<TRule>(ILoggerFactory loggerFactory, [CanBeNull] Assembly? lookInAssembly = null) where TRule : IRule
        {
            foreach (Type type in TypeHelper.GetInstanceTypesInheritedFrom<TRule>(lookInAssembly))
            {
                if (type.ContainsGenericParameters)
                    continue;

                Type log = typeof(Logger<>);
                Type? genericLogger = log.MakeGenericType(type);
                var logger = Activator.CreateInstance(genericLogger, loggerFactory);
                yield return (TRule) Activator.CreateInstance(type, logger);
            }
        }
    }
}