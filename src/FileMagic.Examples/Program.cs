using System.Threading.Tasks;
using ldy985.FileMagic.Core.Extensions;
using ldy985.FileMagic.Core.Rules;
using ldy985.FileMagic.Extensions;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;

namespace ldy985.FileMagic.Examples
{
    public class Program
    {
        public static Task Main(string[] args)
        {
            return CreateHostBuilder(args).RunConsoleAsync();
        }

        public static IHostBuilder CreateHostBuilder(string[] args)
        {
            return Host.CreateDefaultBuilder(args)
                .ConfigureServices((hostContext, services) =>
                {
                    services.AddFileMagicCore(config =>
                        {
                            config.PatternCheck = true;
                            config.StructureCheck = true;
                        })
                        .UseFileMagic()
                        .AddDefaultFileMagicRules();
                    // IParsedHandlerProvider handlerProvider = magicBuilder.AddDefaultParsedHandler();


                    services.AddHostedService<Worker>();
                }).ConfigureLogging(builder =>
                {
                    builder.AddSimpleConsole(options =>
                    {
                        options.SingleLine = true;
                        options.IncludeScopes = true;
                        options.TimestampFormat = "hh:mm:ss ";
                    });
                });
        }
    }
}