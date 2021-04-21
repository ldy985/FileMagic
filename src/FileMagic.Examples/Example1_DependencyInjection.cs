using System;
using System.IO;
using ldy985.FileMagic.Abstracts;
using ldy985.FileMagic.Core.Rules.Rules;
using ldy985.FileMagic.Core.Rules.Tests.Utils;
using ldy985.FileMagic.Extensions;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;

namespace ldy985.FileMagic.Examples
{
    internal static class Example1_DependencyInjection
    {
        public static void Start()
        {
            ServiceCollection serviceCollection = new ServiceCollection();
            serviceCollection.AddFileMagic();
            serviceCollection.AddLogging(builder => builder.SetMinimumLevel(LogLevel.Debug).AddConsole());

            using (ServiceProvider buildServiceProvider = serviceCollection.BuildServiceProvider())
            {
                IFileMagic fileMagic = buildServiceProvider.GetRequiredService<IFileMagic>();

                string filePath = Utilities.BasePath(0) + "ico";

                using (FileStream fs = File.OpenRead(filePath))
                {
                    bool identifyStream = fileMagic.StreamMatches<ICORule>(fs, out var result);
                    Console.WriteLine(identifyStream);

                    fs.Seek(0, SeekOrigin.Begin);
                }
            }
        }
    }
}