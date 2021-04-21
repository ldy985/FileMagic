using System;
using ldy985.FileMagic.Abstracts;
using ldy985.FileMagic.Core;
using ldy985.FileMagic.Core.Rules.Rules;
using ldy985.FileMagic.Core.Rules.Tests.Utils;
using ldy985.FileMagic.Extensions;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;

namespace ldy985.FileMagic.Examples
{
    internal static class Example4_ParsingHandlers
    {
        /// <summary>
        ///     Do something with the BMP data.
        /// </summary>
        /// <param name="obj">the BMP data.</param>
        private static void BMPAction(BitmapRule.Bmp obj)
        {
            Console.WriteLine("BMP type: " + obj.Type);
            Console.WriteLine($"Size: {obj.Height}x{obj.Width}");
        }

        public static void Start()
        {
            ServiceCollection serviceCollection = new ServiceCollection();
            serviceCollection.AddFileMagic(config =>
            {
                config.PatternCheck = true;
                config.StructureCheck = true;
                config.ParserCheck = true;
                config.ParserHandle = true;
            });

            serviceCollection.AddLogging(builder => builder.SetMinimumLevel(LogLevel.Debug).AddConsole());

            serviceCollection.AddSingleton<IParsedHandlerProvider>(provider =>
            {
                ParsedHandlerProvider parsedHandlerProvider = new ParsedHandlerProvider();
                //Add handlers here
                parsedHandlerProvider.AddParsedHandler<BitmapRule, BitmapRule.Bmp>(BMPAction);
                return parsedHandlerProvider;
            });

            using (ServiceProvider buildServiceProvider = serviceCollection.BuildServiceProvider())
            {
                IFileMagic fileMagic = buildServiceProvider.GetRequiredService<IFileMagic>();
                string filePath = Utilities.BasePath(0) + "bmp";

                //File Magic invokes BMPAction if the file is matching the rule specified when running AddParsedHandler
                if (fileMagic.IdentifyFile(filePath, out var result))
                {
                    Console.WriteLine(result.MatchedRuleName);
                }
            }
        }
    }
}