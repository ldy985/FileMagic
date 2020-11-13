using ldy985.FileMagic.File.MagicParser.Enums;

namespace ldy985.FileMagic.File.MagicParser.MagicRule
{
    /// <summary>
    /// Indirection is when the offset is read from a value in the file.
    /// </summary>
    public class Indirection
    {
        /// <summary>
        /// The value to run the <see cref="Op" /> against.
        /// </summary>
        /// <value>
        /// The modifier value.
        /// </value>
        public long ModifierValue { get; set; }

        /// <summary>
        /// The op to apply on the indirection.
        /// </summary>
        /// <value>
        /// The operation.
        /// </value>
        public Operators Op { get; set; }

        /// <summary>
        /// Is the indirection relative.
        /// </summary>
        /// <value>
        ///   <c>true</c> if relative; otherwise, <c>false</c>.
        /// </value>
        public bool Relative { get; set; }

        /// <summary>
        /// The type of the offset data in the file.
        /// </summary>
        /// <value>
        /// The type.
        /// </value>
        public char Type { get; set; }

        /// <summary>
        /// Is the data unsigned.
        /// </summary>
        /// <value>
        ///   <c>true</c> if unsigned; otherwise, <c>false</c>.
        /// </value>
        public bool Unsigned { get; set; }
    }
}