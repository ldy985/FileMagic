using Chronos.Libraries.FileClassifier.Enums;
using Chronos.Libraries.FileClassifier.Values;

namespace Chronos.Libraries.FileClassifier.MagicRule
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