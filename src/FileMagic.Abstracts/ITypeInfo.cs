namespace ldy985.FileMagic.Abstracts
{
    /// <summary>
    ///     Contains information about a detected type.
    /// </summary>
    public interface ITypeInfo
    {
        /// <summary>
        ///     A descriptions of the type.
        /// </summary>
        string Description { get; }

        /// <summary>
        ///     Extensions normally used with the data type.
        /// </summary>
        string[] Extensions { get; }
    }
}