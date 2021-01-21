using System;
using JetBrains.Annotations;
using ldy985.FileMagic.Abstracts;
using ldy985.FileMagic.Core.Misc;
using ldy985.FileMagic.Matchers.Signature.Trie;
using Microsoft.Extensions.DependencyInjection;

namespace ldy985.FileMagic.Core.Extensions
{
    [PublicAPI]
    public static class ServiceCollectionExtensions
    {
        public static IFileMagicBuilder AddFileMagicCore([NotNull] this IServiceCollection collection,
            Action<FileMagicConfig> configureOptions)
        {
            collection.Configure(configureOptions);
            return AddFileMagicCore(collection);
        }

        public static IFileMagicBuilder AddFileMagicCore([NotNull] this IServiceCollection collection)
        {
            collection.AddLogging();
            collection.AddOptions();
            
            collection.AddSingleton<IParallelMagicMatcher, TrieSignatureMatcher>();
            collection.AddSingleton<IRuleProvider, RuleProvider>();

            return new FileMagicBuilder(collection);
        }
    }
}