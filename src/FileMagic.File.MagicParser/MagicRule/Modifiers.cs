using System;

namespace ldy985.FileMagic.File.MagicParser.MagicRule
{
    public class Modifiers
    {
        public Tuple<char, uint> StrengthMod { get; set; } = new Tuple<char, uint>('\0', 0);
    }
}