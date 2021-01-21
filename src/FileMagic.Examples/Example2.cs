using ldy985.FileMagic;
using ldy985.FileMagic.Core;
using Microsoft.Extensions.Options;

namespace ConsoleApp
{
    internal static class Example2
    {
        public static void Start()
        {
            FileMagicConfig fileMagicConfig = new FileMagicConfig();
            using (FileMagic fileMagic = new FileMagic(Options.Create(fileMagicConfig)))
            {
            }
        }
    }
}