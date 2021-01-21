using System;
using System.IO;
using ldy985.FileMagic.Core.Extensions;
using ldy985.FileMagic.Core.Rules.Rules;
using Microsoft.Extensions.Logging.Abstractions;

namespace ConsoleApp
{
    internal static class Example3
    {
        public static void Start()
        {
            PNGRule pngRule = new PNGRule(NullLogger<PNGRule>.Instance);

            using (MemoryStream memoryStream = new MemoryStream(new byte[] {0x1, 0x2, 0x3}))
            {
                Console.WriteLine(pngRule.TryMagic(memoryStream));
            }
        }
    }
}