using System;

namespace ldy985.FileMagic.Abstracts.Enums
{
    [Flags]
    public enum MatchTypes
    {
        Unknown = 0b0,
        Signature = 0b01,
        Structure = 0b10,
        Parser = 0b100,
    }
}