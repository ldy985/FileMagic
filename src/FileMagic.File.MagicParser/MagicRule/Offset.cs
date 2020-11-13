namespace ldy985.FileMagic.File.MagicParser.MagicRule
{
    /// <summary>
    /// The offset defined by the magic.
    /// </summary>
    public class Offset
    {
        public Indirection Indirection { get; set; }

        /// <summary>
        /// Gets or sets whether the offset is relative to last match.
        /// </summary>
        public bool Realative { get; set; }

        /// <summary>
        /// The offset value.
        /// </summary>
        public long Value { get; set; }
    }
}