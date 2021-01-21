using System.Threading.Tasks;
using ldy985.FileMagic;
using ldy985.FileMagic.Abstracts;
using ldy985.FileMagic.Core.Extensions;
using ldy985.FileMagic.Core.Rules;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

namespace ConsoleApp
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
                    IFileMagicBuilder magicBuilder = services.AddFileMagicCore(config =>
                        {
                            config.PatternCheck = true;
                            config.StructureCheck = true;
                        })
                        .UseFileMagic()
                        .AddDefaultFileMagicRules();
                    IParsedHandlerProvider handlerProvider = magicBuilder.AddDefaultParsedHandler();


                    services.AddHostedService<Worker>();
                });
        }
    }
}