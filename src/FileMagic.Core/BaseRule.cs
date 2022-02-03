using System;
using System.Diagnostics.CodeAnalysis;
using System.IO;
using System.Reflection;
using JetBrains.Annotations;
using ldy985.BinaryReaderExtensions;
using ldy985.FileMagic.Abstracts;
using ldy985.FileMagic.Abstracts.Enums;
using Microsoft.Extensions.Logging;

namespace ldy985.FileMagic.Core
{
    /// <summary>
    ///     The Base for all rules.
    /// </summary>
    [PublicAPI]
    [UsedImplicitly]
    public abstract class BaseRule : IRule
    {
        protected BaseRule(ILogger logger)
        {
            Logger = logger;
            Name = GetType().Name;
        }

        protected ILogger Logger { get; }

        /// <inheritdoc />
        public string Name { get; }

        /// <inheritdoc />
        public bool HasMagic => Magic != null;

        /// <inheritdoc />
        public abstract IMagic? Magic { get; }

        /// <inheritdoc />
        public abstract Quality Quality { get; }

        /// <inheritdoc />
        public abstract ITypeInfo TypeInfo { get; }

        /// <inheritdoc />
        public bool HasParser => IsOverridden(GetType(), nameof(TryParseInternal));

        /// <inheritdoc />
        public bool HasStructure => IsOverridden(GetType(), nameof(TryStructureInternal));

        /// <inheritdoc />
        /// <exception cref="IOException"></exception>
        /// <exception cref="ObjectDisposedException"></exception>
        /// <exception cref="ArgumentException">Thrown when the <see cref="IRule" /> has no magic.</exception>
        public bool TryMagic(Stream stream)
        {
            if (!HasMagic)
                throw new ArgumentException("The rule has no magic");

            IMagic magic = Magic!; //Now we know the magic exists.

            long position = stream.Position;
            long length = stream.Length;

            byte?[] magicBytesValue = magic.MagicBytes;
            long magicOffset = (long)magic.Offset;

            if (position + magicOffset + (magic.Pattern.Length >> 1) > length)
                return false;

            stream.Seek(position + (long)magic.Offset, SeekOrigin.Begin);

            foreach (byte? b in magicBytesValue)
            {
                if (!b.HasValue)
                {
                    stream.Position++;
                    continue;
                }

                int readByte = stream.ReadByte();
                if (readByte == -1 || readByte != b.Value)
                    return false;
            }

            return true;
        }

        /// <inheritdoc />
        /// <exception cref="IOException"></exception>
        public bool TryParse(BinaryReader reader, ref IResult result, [NotNullWhen(true)] out IParsed? parsed)
        {
            long position = reader.GetPosition();
            bool tryParseInternal = false;

            try
            {
                tryParseInternal = TryParseInternal(reader, result, out parsed);
            }
            catch (EndOfStreamException)
            {
                //ignore
                parsed = null;
            }
            catch (Exception ex)
            {
                Logger.LogError(ex, "Error while parsing with: {RuleName}", GetType().Name);
                parsed = null;
            }

            reader.SetPosition(position);
            return tryParseInternal;
        }

        /// <inheritdoc />
        public bool TryStructure(BinaryReader reader, ref IResult result)
        {
            long position = reader.GetPosition();
            bool tryStructureInternal = false;

            try
            {
                tryStructureInternal = TryStructureInternal(reader, result);
            }
            catch (EndOfStreamException)
            {
                //ignore
            }
            catch (Exception ex)
            {
                Logger.LogError(ex, "Error while TryingStructure with: {RuleName}", GetType().Name);
            }

            reader.SetPosition(position);
            return tryStructureInternal;
        }

        /// <inheritdoc />
        public override string ToString()
        {
            return Name;
        }

        /// <summary>
        ///     Checks if a given method is set in the derived type.
        /// </summary>
        /// <param name="t">The type to check the method in.</param>
        /// <param name="methodName">The name of the method to check.</param>
        /// <returns>True if the method is defined.</returns>
        private static bool IsOverridden(Type t, string methodName)
        {
            return t.GetMember(methodName, BindingFlags.NonPublic | BindingFlags.Instance | BindingFlags.DeclaredOnly).Length > 0;
        }

        /// <summary>
        ///     Tries to parse as much as the identified structure in the stream as possible.
        /// </summary>
        /// <param name="reader">The stream to parse packed in a <see cref="BinaryReader" />.</param>
        /// <param name="result">Common information about the parsed structure from the stream.</param>
        /// <param name="parsed">The parsed structure from the stream.</param>
        /// <returns>True if parsing was successful.</returns>
        protected virtual bool TryParseInternal(BinaryReader reader, IResult result, [NotNullWhen(true)] out IParsed? parsed)
        {
            parsed = null;
            return false;
        }

        /// <summary>
        ///     Tries if the stream matches the structure defined in the rule.
        /// </summary>
        /// <param name="reader">The stream to check packed in a <see cref="BinaryReader" />.</param>
        /// <param name="result">Common information about the checked structure from the stream.</param>
        /// <returns>True if parsing was successful.</returns>
        protected virtual bool TryStructureInternal(BinaryReader reader, IResult result)
        {
            return false;
        }
    }
}