using Chronos.Libraries.FileClassifier.Enums;
using JetBrains.Annotations;

namespace Chronos.Libraries.FileClassifier.Values
{
    /// <summary>
    ///     The value.
    /// </summary>
    public abstract class Value
    {
        /// <summary>
        ///     Gets the starting byte of the value.
        /// </summary>
        /// <returns>The <see cref="byte" />.</returns>
        [UsedImplicitly]
        public abstract byte GetStartingByte();

        /// <summary>
        ///     Gets the value.
        /// </summary>
        /// <param name="type">The type.</param>
        /// <param name="unsigned">If the return value should be unsigned.</param>
        /// <returns>The <see cref="dynamic" />.</returns>
        public abstract dynamic GetValue(ValueTypes type, bool unsigned);

        [UsedImplicitly]
        public abstract byte[] GetBytes();

        /// <inheritdoc />
        public abstract override string ToString();
    }
}