using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using JetBrains.Annotations;
using ldy985.FileMagic.Abstracts;
using Microsoft.Extensions.DependencyInjection;

namespace ldy985.FileMagic.Core.Rules
{
    public static class FileMagicBuilderExtensions
    {
        public static IFileMagicBuilder AddDefaultFileMagicRules(this IFileMagicBuilder fileMagicBuilder)
        {
            IEnumerable<Type> instanceOfAll = TypeHelper.GetInstanceTypesInheritedFrom<IRule>(typeof(FileMagicBuilderExtensions).Assembly);
            foreach (Type type in instanceOfAll)
            {
                fileMagicBuilder.Services.AddSingleton(typeof(IRule), type);
            }

            return fileMagicBuilder;
        }
    }

    public static class FileMagicRuleHelpers
    {
        public static IEnumerable<IRule> GetDefaultFileMagicRules()
        {
            return CreateRules<IRule>(typeof(FileMagicBuilderExtensions).Assembly);
        }

        [NotNull]
        [ItemNotNull]
        [Pure]
        public static IEnumerable<T> CreateRules<T>([CanBeNull]Assembly? lookInAssembly = null)
        {
            foreach (Type type in TypeHelper.GetInstanceTypesInheritedFrom<T>(lookInAssembly))
            {
                if (type.ContainsGenericParameters)
                    continue;

                ConstructorInfo constructorInfo = type.GetConstructors().First();

                yield return (T)constructorInfo.Invoke(new[] { ((object)null!)! });
            }
        }
    }
}