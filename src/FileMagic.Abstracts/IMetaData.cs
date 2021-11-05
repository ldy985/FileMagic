namespace ldy985.FileMagic.Abstracts
{
    /// <summary>
    ///     Mata data may give additional information that can help us skip checks if possible.
    /// </summary>
    public interface IMetaData
    {
        string Extension { get; }
    }
}