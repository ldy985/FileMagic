using ldy985.FileMagic.Abstracts;
using ldy985.FileMagic.Core;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;

namespace ldy985.FileMagic
{
    public static class FileMagicBuilderExtensions
    {
        public static IFileMagicBuilder UseFileMagic(this IFileMagicBuilder builder)
        {
            builder.Services.TryAddSingleton(x =>
            {
                //We have to call a specific constructor for dependency injection

                return new FileMagic(x.GetRequiredService<ILogger<FileMagic>>(),
                                     x.GetRequiredService<IRuleProvider>(),
                                     x.GetRequiredService<IParallelMagicMatcher>(),
                                     x.GetRequiredService<IParsedHandlerProvider>(),
                                     x.GetRequiredService<IOptions<FileMagicConfig>>());
            });
            builder.Services.TryAddSingleton<IFileMagic, FileMagic>();
            return builder;
        }
    }
}