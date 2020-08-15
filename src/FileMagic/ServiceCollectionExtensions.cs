using ldy985.FileMagic.Abstracts;
using ldy985.FileMagic.Core;
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
                      .AddDefaultParallelRuleMatchers()
                      .AddDefaultConfig();
            return collection;
        }

        public static IFileMagicBuilder AddDefaultParallelRuleMatchers(this IFileMagicBuilder fileMagicBuilder)
        {
            fileMagicBuilder.Services.AddSingleton<IParallelMagicMatcher, TrieSignatureMatcher>();
            return fileMagicBuilder;
        }

        public static IFileMagicBuilder AddDefaultConfig(this IFileMagicBuilder fileMagicBuilder)
        {
            fileMagicBuilder.Services.AddOptions<Options>()
                            .Configure(options =>
                            {
                                options.StructureCheck = true;
                                options.ParserCheck = true;
                                options.PatternCheck = true;
                                options.ParserHandle = false;
                            });

            return fileMagicBuilder;
        }

        public static IParsedHandlerProvider AddParsedHandler(this IFileMagicBuilder fileMagicBuilder)
        {
            ParsedHandlerProvider parsedHandlerProvider = new ParsedHandlerProvider();

            fileMagicBuilder.Services.AddSingleton<IParsedHandlerProvider>(parsedHandlerProvider);

            return parsedHandlerProvider;
        }

        //public static IFileMagicBuilder AddDefaultSingleRuleMatchers(this IFileMagicBuilder fileMagicBuilder)
        //{
        //    fileMagicBuilder.Services.AddSingleton<ISingleRuleMatcher, SimpleSignatureMatcher>();
        //    return fileMagicBuilder;
        //}
    }
}