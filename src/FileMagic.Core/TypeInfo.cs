using ldy985.FileMagic.Abstracts;

namespace ldy985.FileMagic.Core
{
    /// <inheritdoc />
    public class TypeInfo : ITypeInfo
    {
        /// <summary>
        ///     <inheritdoc cref="TypeInfo" />
        /// </summary>
        /// <param name="description">
        ///     <inheritdoc cref="Description" />
        /// </param>
        /// <param name="extensions">
        ///     <inheritdoc cref="Extensions" />
        /// </param>
        public TypeInfo(string description, params string[] extensions)
        {
            Description = description;
            Extensions = extensions;
        }

        /// <inheritdoc />
        public string Description { get; }

        /// <inheritdoc />
        public string[] Extensions { get; }
    }
}