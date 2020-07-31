using ldy985.FileMagic.Abstracts;
using ldy985.FileMagic.Core.Extensions;
using ldy985.FileMagic.Core.Rules;
using ldy985.FileMagic.Matchers.Signature.Trie;
using Microsoft.Extensions.DependencyInjection;

namespace ldy985.FileMagic
{
    public static class ServiceCollectionExtensions
    {
        public static IServiceCollection AddFileMagic(this IServiceCollection collection)
        {
            collection.AddFileMagicCore()
                      .UseFileMagic()
                      .AddDefaultFileMagicRules()
                      .AddDefaultParallelRuleMatchers();
            return collection;
        }

        public static IFileMagicBuilder AddDefaultParallelRuleMatchers(this IFileMagicBuilder fileMagicBuilder)
        {
            fileMagicBuilder.Services.AddSingleton<IParallelMagicMatcher, TrieSignatureMatcher>();
            return fileMagicBuilder;
        }

        //public static IFileMagicBuilder AddDefaultSingleRuleMatchers(this IFileMagicBuilder fileMagicBuilder)
        //{
        //    fileMagicBuilder.Services.AddSingleton<ISingleRuleMatcher, SimpleSignatureMatcher>();
        //    return fileMagicBuilder;
        //}
    }
}