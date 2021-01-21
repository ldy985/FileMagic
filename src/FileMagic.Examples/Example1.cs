using System.IO;
using ldy985.FileMagic;
using ldy985.FileMagic.Abstracts;
using ldy985.FileMagic.Core;
using ldy985.FileMagic.Core.Rules.Rules;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;

namespace ConsoleApp
{
    internal static class Example1
    {
        /// <summary>
        ///     Do something with the BMP.
        /// </summary>
        /// <param name="obj">the BMP data.</param>
        private static void BMPAction(BitmapRule.Bmp obj)
        {
        }

        public static void Start()
        {
            ServiceCollection serviceCollection = new ServiceCollection();
            serviceCollection.AddFileMagic();
            serviceCollection.AddLogging(builder => builder.SetMinimumLevel(LogLevel.Debug).AddConsole());

            serviceCollection.AddSingleton<IParsedHandlerProvider>(provider =>
            {
                ParsedHandlerProvider parsedHandlerProvider = new ParsedHandlerProvider();
                parsedHandlerProvider.AddParsedHandler<BitmapRule, BitmapRule.Bmp>(BMPAction);
                return parsedHandlerProvider;
            });
            using (ServiceProvider buildServiceProvider = serviceCollection.BuildServiceProvider())
            {
                IFileMagic fileMagic = buildServiceProvider.GetRequiredService<IFileMagic>();
                ILogger<Program> logger = buildServiceProvider.GetRequiredService<ILogger<Program>>();

                using (FileStream fs = File.OpenRead(@"O:\BitSync\Programering\C#\FileMagic\resources\test0.chm"))
                {
                    //bool identifyStream = fileMagic.StreamMatches<ICORule>(fs, out var result);
                    //Console.WriteLine(identifyStream);

                    //fs.Seek(0, SeekOrigin.Begin);

                    //Thread.Sleep(1000);

                    bool identifyStream = fileMagic.IdentifyStream(fs, out IResult result);
                    logger.LogWarning(identifyStream.ToString());
                    logger.LogWarning(result?.MatchedRuleName);
                }
            }
        }
    }
}