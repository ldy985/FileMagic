using System;
using System.Collections.Generic;
using ldy985.FileMagic.File.MagicParser;
using ldy985.FileMagic.File.MagicParser.MagicRule;
using ldy985.FileMagic.File.MagicParser.Parser;
using Microsoft.Extensions.Logging;

namespace ldy985.FileMagic.File.RuleGenerator
{
    class Program
    {
        static void Main(string[] args)
        {
            ILoggerFactory loggerFactory = LoggerFactory.Create(builder => builder.AddConsole());
            Logger<FileRuleParser> logger = new Logger<FileRuleParser>(loggerFactory);
            Logger<MagicEntryParser> logger2 = new Logger<MagicEntryParser>(loggerFactory);
            FileRuleParser fileRuleParser = new FileRuleParser(logger, new MagicEntryParser(logger2));

            fileRuleParser.LoadRulesWithParser(@"o:\BitSync\Programering\C#\AgileResponse\FileClassifier\Magdir5.34-Fixed\");

            foreach (MagicEntry magicEntry in fileRuleParser.EntryList)
            {
                Console.WriteLine("------------------");
                IEnumerable<MagicEntry> t = GetEntries(magicEntry);
                foreach (MagicEntry entry in t)
                {
                    Console.WriteLine(entry);
                }
            }
        }

        private static IEnumerable<MagicEntry> GetEntries(MagicEntry magicEntry)
        {
            yield return magicEntry;

            if (magicEntry.Children == null)
                yield break;

            foreach (MagicEntry magicEntryChild in magicEntry.Children)
            {
                yield return magicEntryChild;
            }

            foreach (MagicEntry magicEntryChild in magicEntry.Children)
            {
                foreach (MagicEntry entry in GetEntries(magicEntryChild))
                {
                    yield return entry;
                }
            }
        }
    }
}