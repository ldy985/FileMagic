using System;
using System.IO;
using ldy985.FileMagic.Core.Rules;
using ldy985.FileMagic.Core.Rules.Rules.Media;

namespace ldy985.FileMagic.Examples
{
    /// <summary>
    ///     Checks if a specific stream is of a specific type.
    /// </summary>
    internal static class Example3_QuickPatternMatch
    {
        public static void Start()
        {
            PNGRule pngRule = FileMagicRuleHelpers.CreateRule<PNGRule>();

            using (MemoryStream memoryStream = new MemoryStream(new byte[] { 0x1, 0x2, 0x3 }))
            {
                Console.WriteLine(pngRule.TryMagic(memoryStream));
            }
        }
    }
}