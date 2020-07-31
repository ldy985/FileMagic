using System;

namespace ldy985.FileMagic.Abstracts
{
    public interface IMagic
    {
        string Pattern { get; }
        Lazy<byte?[]> MagicBytes { get; }
        ulong Offset { get; }
    }
}