using System.IO;
using ldy985.FileMagic.Abstracts;
using ldy985.FileMagic.Core;
using ldy985.FileMagic.Core.Rules.Rules;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;

namespace ldy985.FileMagic.Examples
{
    internal class Program
    {
        public static void Main()
        {
            Example1.Start();
            Example2.Start();
            Example3.Start();
        }
    }

    internal static class Example3
    {
        public static void Start()
        {
            FileMagicConfig fileMagicConfig = new FileMagicConfig();
            FileMagic fileMagic = FileMagicBuilder.Create(fileMagicConfig).Build();
        }
    }

    internal static class Example2
    {
        public static void Start()
        {
            FileMagicConfig fileMagicConfig = new FileMagicConfig();
            FileMagic fileMagic = new FileMagic(Microsoft.Extensions.Options.Options.Create(fileMagicConfig));
        }
    }

    internal static class Example1
    {
        private static void BMPAction(BitmapRule.BMP obj) { }

        public static void Start()
        {
            ServiceCollection serviceCollection = new ServiceCollection();
            serviceCollection.AddFileMagic();
            serviceCollection.AddLogging(builder => builder.SetMinimumLevel(LogLevel.Debug).AddConsole());

            serviceCollection.AddSingleton<IParsedHandlerProvider>(provider =>
            {
                ParsedHandlerProvider parsedHandlerProvider = new ParsedHandlerProvider();
                parsedHandlerProvider.AddParsedHandler<BitmapRule, BitmapRule.BMP>(BMPAction);
                return parsedHandlerProvider;
            });
            using (ServiceProvider buildServiceProvider = serviceCollection.BuildServiceProvider())
            {
                var fileMagic = buildServiceProvider.GetRequiredService<IFileMagic>();
                var logger = buildServiceProvider.GetRequiredService<ILogger<Program>>();

                using (FileStream fs = File.OpenRead(@"O:\BitSync\Programering\C#\FileMagic\resources\test0.chm"))
                {
                    //bool identifyStream = fileMagic.StreamMatches<ICORule>(fs, out var result);
                    //Console.WriteLine(identifyStream);

                    //fs.Seek(0, SeekOrigin.Begin);

                    //Thread.Sleep(1000);

                    bool identifyStream = fileMagic.IdentifyStream(fs, out var result);
                    logger.LogWarning(identifyStream.ToString());
                    logger.LogWarning(result?.MatchedRuleName);
                }
            }
        }
    }
}