using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using JetBrains.Annotations;
using ldy985.FileMagic.Abstracts;
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
        public static IEnumerable<T> CreateRules<T>(ILoggerFactory loggerFactory, [CanBeNull] Assembly? lookInAssembly = null)
        {
            foreach (Type type in TypeHelper.GetInstanceTypesInheritedFrom<T>(lookInAssembly))
            {
                if (type.ContainsGenericParameters)
                    continue;

                ILogger logger = loggerFactory.CreateLogger(type);

                ConstructorInfo constructorInfo = type.GetConstructors().First();

                yield return (T) constructorInfo.Invoke(new[] {((object) null!)!});
            }
        }
    }
}