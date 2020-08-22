using System;
using System.Collections.Generic;
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
            return TypeHelper.CreateInstanceOfAll<IRule>(typeof(FileMagicBuilderExtensions).Assembly);
        }
    }
}