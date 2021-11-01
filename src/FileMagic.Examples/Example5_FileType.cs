using System;
using ldy985.FileMagic.Abstracts;
using ldy985.FileMagic.Core;
using ldy985.FileMagic.Core.Rules.Rules.Media;

namespace ldy985.FileMagic.Examples
{
    internal static class Example5_FileType
    {
        public static void Start()
        {
            FileMagicConfig fileMagicConfig = new FileMagicConfig
            {
                ParserCheck = true,
                StructureCheck = true
            };

            using (FileMagic fileMagic = new FileMagic(fileMagicConfig))
            {
                string filePath = Program.BasePath(0) + "bmp";

                if (fileMagic.IdentifyFile(filePath, out IResult result))
                {
                    Console.WriteLine("The file: " + filePath);
                    Console.WriteLine("Was detected as matching the following rule: " + result.MatchedRule.Name);

                    if (result.MatchedRule is BitmapRule)
                    {
                        //Do something special if BMP
                    }
                }
            }
        }
    }
}