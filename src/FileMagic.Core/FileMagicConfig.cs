namespace ldy985.FileMagic.Core
{
    /// <summary>
    ///     The config for FileMagic.
    /// </summary>
    public class FileMagicConfig
    {
        /// <summary>
        ///     Uses simple pattern check to determine filetype.
        /// </summary>
        public bool PatternCheck { get; set; } = true;

        /// <summary>
        ///     Tries to determine the filetype by trying if the stream matched the structure we would expect for a given filetype.
        /// </summary>
        public bool StructureCheck { get; set; }

        /// <summary>
        ///     Enables parsing of the file if we have a parser for it. The parsing also determines if the file is valid.
        /// </summary>
        public bool ParserCheck { get; set; }

        /// <summary>
        ///     Enables callback to registered handlers of a parsed file.
        /// </summary>
        public bool ParserHandle { get; set; }
    }
}