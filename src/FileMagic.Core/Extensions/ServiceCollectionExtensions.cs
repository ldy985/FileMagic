using System;
using JetBrains.Annotations;
using ldy985.FileMagic.Abstracts;
using ldy985.FileMagic.Core.Misc;
using Microsoft.Extensions.DependencyInjection;

namespace ldy985.FileMagic.Core.Extensions
{
    [PublicAPI]
    public static class ServiceCollectionExtensions
    {
        public static IFileMagicBuilder AddFileMagicCore(this IServiceCollection collection, Action<FileMagicConfig> configureOptions)
        {
            collection?.Configure(configureOptions);
            return AddFileMagicCore(collection);
        }

        public static IFileMagicBuilder AddFileMagicCore(this IServiceCollection collection)
        {
            collection.AddOptions<FileMagicConfig>()
                      .Configure(options =>
                      {
                          options.StructureCheck = true;
                          options.ParserCheck = true;
                          options.PatternCheck = true;
                          options.ParserHandle = false;
                      });
            collection.AddLogging();
            collection.AddSingleton<IRuleProvider, RuleProvider>();

            return new FileMagicBuilder(collection);
        }
    }
}