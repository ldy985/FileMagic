using ldy985.FileMagic.File.MagicParser.Enums;

namespace ldy985.FileMagic.File.MagicParser.MagicRule
{
    /// <summary>
    /// The type of data the magic contains and should be tested as.
    /// </summary>
    public class DataType
    {
        /// <summary>
        /// Gets or sets the modifier value.
        /// </summary>
        /// <value>
        /// The modifier value.
        /// </value>
        public long ModifierValue { get; set; }

        /// <summary>
        /// Gets or sets the op.
        /// </summary>
        /// <value>
        /// The operation.
        /// </value>
        public Operators Op { get; set; }

        /// <summary>
        /// Gets or sets the type.
        /// </summary>
        /// <value>
        /// The type.
        /// </value>
        public ValueTypes Type { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether this <see cref="DataType"/> is unsigned.
        /// </summary>
        /// <value>
        ///   <c>true</c> if unsigned; otherwise, <c>false</c>.
        /// </value>
        public bool Unsigned { get; set; }
    }
}