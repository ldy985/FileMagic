namespace Chronos.Libraries.FileClassifier.MagicRule
{
    public class PatternInfo
    {
        /// <summary>
        /// Should we test against <see cref="ScanSize"/>  bytes or lines. (True for lines).
        /// </summary>
        internal bool ByteOrLine;

        /// <summary>
        /// The max size to run the test against.
        /// </summary>
        internal int ScanSize = 8 * 1024;
    }
}