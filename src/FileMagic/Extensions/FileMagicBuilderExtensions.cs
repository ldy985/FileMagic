using ldy985.FileMagic.Abstracts;
using ldy985.FileMagic.Core;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;

namespace ldy985.FileMagic.Extensions
{
    public static class FileMagicBuilderExtensions
    {
        public static IFileMagicBuilder UseFileMagic(this IFileMagicBuilder builder)
        {
            builder.Services.AddSingleton(x =>
            {
                ILogger<FileMagic> logger = x.GetRequiredService<ILogger<FileMagic>>();
                IOptions<FileMagicConfig>? options = x.GetRequiredService<IOptions<FileMagicConfig>>();
                IRuleProvider ruleProvider = x.GetRequiredService<IRuleProvider>();
                IParsedHandlerProvider? handlerProvider = x.GetService<IParsedHandlerProvider>();
                IParallelMagicMatcher parallelMagicMatcher = x.GetRequiredService<IParallelMagicMatcher>();

                return new FileMagic(options, logger, ruleProvider, parallelMagicMatcher, handlerProvider);
            });

            builder.Services.AddSingleton<IFileMagic>(x => x.GetRequiredService<FileMagic>());
            return builder;
        }
    }
}