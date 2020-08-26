using System;

namespace Chronos.Libraries.FileClassifier.MagicRule
{
    public class Modifiers
    {
        public Tuple<char, uint> StrengthMod { get; set; } = new Tuple<char, uint>('\0', 0);
    }
}