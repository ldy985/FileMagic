using System;

namespace ldy985.FileMagic.Abstracts.Enums
{
    /// <summary>
    ///     Shows which type of check succeed on the stream.
    /// </summary>
    [Flags]
    public enum MatchTypes
    {
        /// <summary>
        ///     Default. Nothing matched.
        /// </summary>
        None = 0b0,

        /// <summary>
        ///     A signature check.
        /// </summary>
        Signature = 0b1,

        /// <summary>
        ///     A structure check.
        /// </summary>
        Structure = 0b10,

        /// <summary>
        ///     A full parse.
        /// </summary>
        Parser = 0b100
    }
}