using System;
using System.IO;
using ldy985.FileMagic.Core;
using ldy985.FileMagic.Core.Rules.Rules;
using ldy985.FileMagic.Core.Rules.Rules.Containers;
using ldy985.FileMagic.Core.Rules.Rules.Containers.Archive;
using Microsoft.Extensions.Options;

namespace ldy985.FileMagic.Examples
{
    internal static class Example2_TryingSpecificRules
    {
        public static void Start()
        {
            FileMagicConfig fileMagicConfig = new FileMagicConfig
            {
                ParserCheck = true,
                StructureCheck = true
            };

            using (FileMagic fileMagic = new FileMagic(Options.Create(fileMagicConfig)))
            using (MemoryStream memoryStream = new MemoryStream(new byte[] {0x4d, 0x5a}))
            {
                if (fileMagic.StreamMatches<EXERule>(memoryStream, out var result))
                    Console.WriteLine(result.Description);

                if (fileMagic.StreamMatches<RAR5Rule>(memoryStream, out var result2))
                    Console.WriteLine(result2.Description);
            }
        }
    }
}