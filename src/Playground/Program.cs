#define test
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using CsvHelper;
using ldy985.BinaryReaderExtensions;
using ldy985.FileMagic;
using ldy985.FileMagic.Abstracts;
using ldy985.FileMagic.Core;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Logging.Console;

namespace ldy985.Playground
{
    class Program
    {
        static void Main(string[] args)
        {
            List<string> list = new List<string>();

            //Log.Logger = new LoggerConfiguration()
            //             .WriteTo.ColoredConsole(outputTemplate: "[{Timestamp:HH:mm:ss} {Level:u3}] [{SourceContext}] {Message:lj}{NewLine}{Exception}")
            //             //.MinimumLevel.Is(LogEventLevel.Verbose)
            //             .MinimumLevel.Is(LogEventLevel.Fatal)
            //             .CreateLogger();
            //ILog log = LogProvider.GetCurrentClassLogger();

            //FileGuesser fileGuesser = new FileGuesserBuilder(DetectionOptions.All).AddDefault().
            //                                                                      AddParsedHandler<BitmapRule, BitmapRule.BMP>(bmp => Console.WriteLine($"{bmp.Height}x{bmp.Width}")).
            //                                                                      AddParsedHandler<GIFRule, GIFRule.GIF>(gif => Console.WriteLine($"{gif.Height}x{gif.Width}")).
            //                                                                      Build();

            ServiceCollection serviceCollection = new ServiceCollection();
            serviceCollection.AddFileMagic();
            serviceCollection.AddLogging(builder => builder.SetMinimumLevel(LogLevel.Information).AddConsole(options => options.Format=ConsoleLoggerFormat.Systemd));
            using (ServiceProvider buildServiceProvider = serviceCollection.BuildServiceProvider())
            {
                var fileGuesser = buildServiceProvider.GetRequiredService<IFileMagic>();
                var log = buildServiceProvider.GetRequiredService<ILogger<Program>>();

                ////Register zip handler
                //fileGuesser.AddParsedHandler<PKZipRule, PKZipRule.ZipArchive>(zip =>
                //{
                //    foreach (PKZipRule.ZipFile objFile in zip.Files)
                //    {
                //        log.Debug(objFile.FullName);
                //        using (MemoryStream memoryStream = new MemoryStream(objFile.Data))
                //        {
                //            if (fileGuesser.IdentifyStream(memoryStream, out var res))
                //            {
                //                log.Debug(res.Description);
                //            }
                //        }

                //    }
                //});

                //using (FileStream fileStream = File.OpenRead("c:\\Users\\ldy\\Downloads\\GRN.BMP"))
                //    fileGuesser.IdentifyStream(fileStream, out var rule);

                //using (FileStream fileStream = File.OpenRead("c:\\Users\\ldy\\Downloads\\noisebackground.gif"))
                //    fileGuesser.IdentifyStream(fileStream, out var rule);

                Analyzer analyzer = new Analyzer(1024);
                ulong count = 0;
                ulong found = 0;

                IEnumerable<string> filePaths = GetFiles(@"o:\Projects\FileProject\FileStorage\", "*.*", SearchOption.AllDirectories);
                //string[] filePaths = File.ReadAllLines(@"G:\Test.txt");

                foreach (string filePath in filePaths)
                {
                    //var filePath = @"C:\Users\ldy\Downloads\openmcdf-master\sources\Test\TestFiles\corrupted-sector-chain.doc";
                    string extension = Path.GetExtension(filePath);

                    if (new[]
                    {
                        ".yml", ".txt", ".cs", ".txt", ".torrent", ".pl", ".py", ".md", ".html", ".csproj", ".config",
                        ".xml", ".c", ".h", ".g4", ".js", ".json", ".js", ".lua", ".css", ".go", ".ps1", ".pem", ".cmd",
                        ".mht", ".vbs", ".pfx", ".p12", ".wsf", ".ctl", ".csv", ".bat", ".ini", ".inf", ".htm", ".cache", ".htm"
                    }.Contains(extension))
                    {
                        continue;
                    }

                    count++;
                    using (FileStream fileStream = new FileStream(filePath, FileMode.Open))
                    using (BufferedStream bs = new BufferedStream(fileStream, 8192))
                    using (BinaryReader binaryReader = new BinaryReader(bs))
                    {
#if test
                        bool identifyStream;
                        IResult result;

                        if (!string.IsNullOrWhiteSpace(extension))
                        {
                            IMetaData metaData = new MetaData(extension);
                            identifyStream = fileGuesser.IdentifyStream(fileStream, out result, ref metaData);
                        }
                        else
                        {
                            identifyStream = fileGuesser.IdentifyStream(fileStream, out result);
                        }

                        if (identifyStream)
                        {
                            //log.Information(extension);
                            //log.LogInformation(result.Description);

                            string trimStart = extension.TrimStart('.');

                            //if (result.Extensions.Contains(trimStart, StringComparer.OrdinalIgnoreCase))
                            //{
                            //    var s = $"g:\\FileStorage\\{trimStart}\\";
                            //    if (!Directory.Exists(s))
                            //        Directory.CreateDirectory(s);
                            //    File.Copy(filePath,$"{s}{Path.GetFileName(filePath)}",true);
                            //}

                           // log.LogInformation(string.Join(", ", result.Extensions));
                            log.LogInformation("+ {FilePath}", Path.GetFileName(filePath));
                            found++;
                        }
                        else
                        {
                            log.LogInformation("- {name} {FilePath}", Path.GetExtension(filePath), filePath);

                            //Thread.Sleep(500);
                            list.Add(extension);
                        }

                        //log.Information("-----------------");
#else
                        analyzer.AddFile(binaryReader);
                        Console.WriteLine("Added: " + filePath);
#endif
                    }
                }
#if !test
                analyzer.DumpStats("g:\\tmp\\dump.csv");

#endif
#if test
                Console.WriteLine((double)found / (double)count * 100);
                foreach (string s in list)
                {
                    Console.WriteLine(s);
                }
#endif
            }
        }

        public static IEnumerable<string> GetFiles(string root, string searchPattern, SearchOption allDirectories)
        {
            Stack<string> pending = new Stack<string>();
            pending.Push(root);
            while (pending.Count != 0)
            {
                var path = pending.Pop();
                string[] next = null;
                try
                {
                    next = Directory.GetFiles(path, searchPattern, allDirectories);
                }
                catch { }

                if (next != null && next.Length != 0)
                    foreach (var file in next)
                        yield return file;
                try
                {
                    next = Directory.GetDirectories(path);
                    foreach (var subdir in next) pending.Push(subdir);
                }
                catch { }
            }
        }
    }

    public class Analyzer
    {
        private readonly int _size;
        private readonly List<Dictionary<byte, ulong>> _stats;

        /// <inheritdoc />
        public Analyzer(int size)
        {
            _size = size;
            _stats = new List<Dictionary<byte, ulong>>(size);
            for (int i = 0; i < size; i++)
            {
                _stats.Add(new Dictionary<byte, ulong>());
            }
        }

        public void AddFile(BinaryReader br)
        {
            long l = br.GetLength();
            long length = l > _size ? _size : l;

            for (int i = 0; i < length; i++)
            {
                byte readByte = br.ReadByte();
                if (_stats[i].TryGetValue(readByte, out var val))
                {
                    _stats[i][readByte] = val + 1;
                }
                else
                {
                    _stats[i][readByte] = 1;
                }
            }
        }

        public void DumpStats(string path)
        {
            using (var stream = new FileStream(path, FileMode.OpenOrCreate))
            using (var writer = new StreamWriter(stream))
            using (var csv = new CsvWriter(writer))
            {
                foreach (Dictionary<byte, ulong> dictionary in _stats)
                {
                    foreach ((byte key, ulong value) in dictionary.OrderByDescending(pair => pair.Value))
                    {
                        csv.WriteField($" {key:X2}: {value}");
                    }

                    csv.NextRecord();
                }
            }
        }
    }
}