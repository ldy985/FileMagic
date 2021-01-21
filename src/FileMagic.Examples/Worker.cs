using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using ldy985.FileMagic.Abstracts;
using ldy985.FileMagic.Core;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;

namespace ConsoleApp
{
    public class Worker : BackgroundService
    {
        private readonly IFileMagic _fileMagic;
        private readonly ILogger<Worker> _logger;

        public Worker(ILogger<Worker> logger, IFileMagic fileMagic)
        {
            _logger = logger;
            _fileMagic = fileMagic;
        }
        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            IEnumerable<string> filePaths = GetFiles(@"o:\Projects\FileProject\FileStorage\aac\", "*.aac", SearchOption.AllDirectories);

            foreach (string filePath in filePaths)
            {
                if (stoppingToken.IsCancellationRequested)
                    break;
                try
                {
                    Transform(filePath);
                }
                catch (Exception e)
                {   
                    Console.WriteLine(e);
                }
            }

            //Example1.Start();
            //Example2.Start();
            //Example3.Start();
        }

        private void Transform(string filePath)
        {
            string? extension = Path.GetExtension(filePath);

            if (new[]
            {
                ".yml", ".txt", ".cs", ".txt", ".torrent", ".pl", ".py", ".md", ".html", ".csproj", ".config",
                ".xml", ".c", ".h", ".g4", ".js", ".json", ".js", ".lua", ".css", ".go", ".ps1", ".pem", ".cmd",
                ".mht", ".vbs", ".pfx", ".p12", ".wsf", ".ctl", ".csv", ".bat", ".ini", ".inf", ".htm", ".cache", ".htm"
            }.Contains(extension))
                return;

            using (FileStream fileStream = new FileStream(filePath, FileMode.Open))
            using (BufferedStream bs = new BufferedStream(fileStream, 8192))
            {
                bool identifyStream;
                IResult result;

                if (!string.IsNullOrWhiteSpace(extension))
                {
                    IMetaData metaData = new MetaData(extension);
                    identifyStream = _fileMagic.IdentifyStream(bs, out result, ref metaData);
                }
                else
                {
                    identifyStream = _fileMagic.IdentifyStream(bs, out result);
                }

                if (identifyStream)
                    _logger.LogInformation("+ {Type} {FilePath}", result.MatchedRuleName, Path.GetFileName(filePath));
                else
                    _logger.LogInformation("- {name} {FilePath}", Path.GetExtension(filePath), filePath);
            }
        }

        public static IEnumerable<string> GetFiles(string root, string searchPattern, SearchOption allDirectories)
        {
            Stack<string> pending = new Stack<string>();
            pending.Push(root);
            while (pending.Count != 0)
            {
                string path = pending.Pop();
                string[] next = null;
                try
                {
                    next = Directory.GetFiles(path, searchPattern, allDirectories);
                }
                catch
                {
                }

                if (next != null && next.Length != 0)
                    foreach (string file in next)
                        yield return file;
                try
                {
                    next = Directory.GetDirectories(path);
                    foreach (string subdir in next) pending.Push(subdir);
                }
                catch
                {
                }
            }
        }
    }
}