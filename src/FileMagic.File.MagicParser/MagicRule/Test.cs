using ldy985.FileMagic.File.MagicParser.Enums;
using ldy985.FileMagic.File.MagicParser.Values;

namespace ldy985.FileMagic.File.MagicParser.MagicRule
{
    /// <summary>
    /// The test to run on the file.
    /// </summary>
    public class Test
    {
        /// <summary>
        /// Defines how the test is matched.
        /// </summary>
        public Operators Op { get; set; }

        /// <summary>
        /// The value of the test.
        /// </summary>
        public Value Value { get; set; }
    }
}