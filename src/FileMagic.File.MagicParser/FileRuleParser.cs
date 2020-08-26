using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using Chronos.Libraries.FileClassifier.Enums;
using Chronos.Libraries.FileClassifier.Helpers;
using Chronos.Libraries.FileClassifier.Parser;
using Chronos.Libraries.FileClassifier.entries;
using Microsoft.Extensions.Logging;

namespace Chronos.Libraries.FileClassifier
{
    public class FileRuleParser
    {
        private readonly ILogger<FileRuleParser> _logger;
        private readonly MagicEntryParser _entryParser;

        /// <summary>
        /// Defines how deep magic rules can be.
        /// </summary>
        private const int MAX_LEVELS = 50;

        /// <summary>
        /// The parsed list of magic rules.
        /// </summary>
        public List<MagicEntry> EntryList = new List<MagicEntry>();

        public FileRuleParser(ILogger<FileRuleParser> logger, MagicEntryParser entryParser)
        {
            _logger = logger;
            _entryParser = entryParser;
        }

        /// <summary>
        /// The named magic rules.
        /// </summary>
        public Dictionary<string, MagicEntry> NamedEntries { get; } = new Dictionary<string, MagicEntry>();

        /// <summary>
        /// Tries to find a magic match for the input.
        /// </summary>
        /// <param name="fileReader">The bytes to test against.</param>
        /// <returns>The <see cref="ContentInfo"/>.</returns>
        public void LoadRulesWithParser(string fileOrFolder)
        {
            if (File.Exists(fileOrFolder))
            {
                using (var fileStream = File.OpenRead(fileOrFolder))
                using (var reader2 = new StreamReader(fileStream))
                {
                    ParseEntries(reader2);
                }
            }

            if (Directory.Exists(fileOrFolder))
            {
                foreach (var ruleFilePath in Directory.GetFiles(fileOrFolder, "*", SearchOption.AllDirectories))
                {
                    using (var fileStream = File.OpenRead(ruleFilePath))
                    using (var reader2 = new StreamReader(fileStream))
                    {
                        ParseEntries(reader2);
                    }
                }
            }
        }

        private void ParseEntries(StreamReader lineReader)
        {
            var levelParents = new MagicEntry[MAX_LEVELS];
            MagicEntry previousEntry = null;
            while (!lineReader.EndOfStream)
            {
                var line = lineReader.ReadLine();

                if (string.IsNullOrEmpty(line))
                {
                    _logger.LogTrace("Skipping blank line");
                    continue;
                }

                // skip blanks and comments
                if (line[0] == '#')
                {
                    _logger.LogTrace("Skipping comment: {comment}", line);
                    continue;
                }

                // we need the previous entry because of mime-type, etc. which augment the previous line
                var entry = _entryParser.ParseLine(previousEntry, line);

                if (entry == null)
                    continue;

                entry.FileRuleParserPtr = this;

                var level = entry.Level;
                if (previousEntry == null && level != 0)
                {
                    _logger.LogError("first entry of the file but the level ({level}) should be 0", level);
                    continue;
                }

                if (level == 0)
                {
                    if (entry.DataType.Type == ValueTypes.NAME)
                    {
                        var name = (string)entry.Test.Value.GetValue(ValueTypes.STRING, false);

                        if (!NamedEntries.ContainsKey(name))
                        {
                            NamedEntries.Add(name, entry);
                        }
                        else
                        {
                            _logger.LogWarning("Duplicate named entry: {name}", name);
                        }
                    }
                    else
                    {
                        EntryList.Add(entry);
                    }
                }
                else if (levelParents[level - 1] == null)
                {
                    // throw new Exception("entry has level " + level + " but no parent entry with
                    // level " + (level - 1), null);
                    _logger.LogError("entry has level {level} but no parent entry with level ", level - 1);
                    continue;
                }
                else
                {
                    // we are a child of the one above us
                    levelParents[level - 1].AddChild(entry);
                }

                levelParents[level] = entry;
                previousEntry = entry;
            }
        }
    }
}