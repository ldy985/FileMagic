using System;
using ldy985.FileMagic.Abstracts;
using ldy985.FileMagic.Core;
using ldy985.FileMagic.Core.Extensions;
using Microsoft.Extensions.DependencyInjection;

namespace ldy985.FileMagic.Extensions
{
    public static class ServiceCollectionExtensions
    {
        public static IFileMagicBuilder AddFileMagic(this IServiceCollection collection, Action<FileMagicConfig>? config = null)
        {
            IFileMagicBuilder coreBuilder = collection.AddFileMagicCore(config);
            coreBuilder.UseFileMagic();

            return new Core.Misc.FileMagicBuilder(collection);
        }

        public static IParsedHandlerProvider AddDefaultParsedHandler(this IFileMagicBuilder fileMagicBuilder)
        {
            ParsedHandlerProvider parsedHandlerProvider = new ParsedHandlerProvider();

            fileMagicBuilder.Services.AddSingleton<IParsedHandlerProvider>(parsedHandlerProvider);

            return parsedHandlerProvider;
        }
    }
}