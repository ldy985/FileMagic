using System.IO;
using ldy985.FileMagic.Abstracts;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;

namespace ldy985.FileMagic.Examples
{
    internal class Program
    {
        public static void Main()
        {
            ServiceCollection serviceCollection = new ServiceCollection();
            serviceCollection.AddFileMagic();
            serviceCollection.AddLogging(builder => builder.SetMinimumLevel(LogLevel.Debug).AddConsole());
            using (ServiceProvider buildServiceProvider = serviceCollection.BuildServiceProvider())
            {
                var fileMagic = buildServiceProvider.GetRequiredService<IFileMagic>();
                var logger = buildServiceProvider.GetRequiredService<ILogger<Program>>();

                using (FileStream fs = File.OpenRead(@"O:\BitSync\Programering\C#\FileMagic\resources\test0.ico"))
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