using System;
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
        public static IFileMagicBuilder AddFileMagic(this IServiceCollection collection)
        {
            IFileMagicBuilder coreBuilder = collection.AddFileMagicCore();
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