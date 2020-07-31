using JetBrains.Annotations;
using ldy985.FileMagic.Abstracts;
using ldy985.FileMagic.Core.Misc;
using Microsoft.Extensions.DependencyInjection;

namespace ldy985.FileMagic.Core.Extensions
{
    [PublicAPI]
    public static class ServiceCollectionExtensions
    {
        //public static IFileMagicBuilder AddFileMagicCore(this IServiceCollection collection, Action<S3Config, IServiceProvider> configureS3)
        //{
        //    collection?.Configure(configureS3);
        //    return AddFileMagicCore(collection);
        //}

        //public static IFileMagicBuilder AddFileMagicCore(this IServiceCollection collection, Action<S3Config> configureS3)
        //{
        //    collection?.Configure(configureS3);
        //    return AddFileMagicCore(collection);
        //}

        public static IFileMagicBuilder AddFileMagicCore(this IServiceCollection collection)
        {
            //collection.AddOptions();
            collection.AddLogging();
            collection.AddSingleton<IRuleProvider, RuleProvider>();

            return new FileMagicBuilder(collection);
        }
    }
}