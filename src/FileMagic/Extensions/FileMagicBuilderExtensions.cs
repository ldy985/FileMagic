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
            builder.Services.AddSingleton(x =>
            {
                return new FileMagic(x.GetRequiredService<IOptions<FileMagicConfig>>(),
                    x.GetRequiredService<ILogger<FileMagic>>(), x.GetRequiredService<IRuleProvider>(),
                    x.GetRequiredService<IParallelMagicMatcher>(), x.GetService<IParsedHandlerProvider>());
            });

            builder.Services.AddSingleton<IFileMagic>(x => x.GetRequiredService<FileMagic>());
            return builder;
        }
    }
}