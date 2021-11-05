namespace ldy985.FileMagic.Abstracts
{
    /// <summary>
    ///     A Magic is a series of bytes that defines a type of stream.
    ///     Files for example, often contain a header that specifies a filetype.
    /// </summary>
    public interface IMagic
    {
        /// <summary>
        ///     The byte pattern encoded as a HEX string. It MUST be multiples of two in lenght.
        ///     The pattern supports wildcards denoted by '?'. Wildcards MUST come in pairs of two like this example:
        ///     0000??3000
        /// </summary>
        string Pattern { get; }

        /// <summary>
        ///     This is the internal representation of the magic used by the <see cref="IParallelMagicMatcher" /> matcher.
        /// </summary>
        byte?[] MagicBytes { get; }

        /// <summary>
        ///     The offset shifts the <see cref="Pattern" /> by this amount from the start of the stream. This is used when magic
        ///     bytes are located beyond the start of the stream.
        /// </summary>
        ulong Offset { get; }
    }
}