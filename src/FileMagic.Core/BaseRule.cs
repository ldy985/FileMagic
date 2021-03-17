using System;
using System.Diagnostics;
using System.Diagnostics.CodeAnalysis;
using System.IO;
using System.Reflection;
using ldy985.BinaryReaderExtensions;
using ldy985.FileMagic.Abstracts;
using Microsoft.Extensions.Logging;

namespace ldy985.FileMagic.Core
{
    public abstract class BaseRule : IRule
    {
        public override string ToString()
        {
            return Name;
        }

        private readonly ILogger _logger;

        protected BaseRule(ILogger logger)
        {
            _logger = logger;
            HasParser = IsOverridden(GetType(), nameof(TryParseInternal));
            HasStructure = IsOverridden(GetType(), nameof(TryStructureInternal));
            Name = GetType().Name;
        }

        private static bool IsOverridden(Type t, string methodName)
        {
            return t.GetMember(methodName, BindingFlags.NonPublic | BindingFlags.Instance | BindingFlags.DeclaredOnly).Length > 0;
        }

        public string Name { get; }

        /// <inheritdoc />
        public bool HasMagic => Magic != null;

        /// <inheritdoc />
        public abstract IMagic? Magic { get; }

        /// <inheritdoc />
        public abstract ITypeInfo TypeInfo { get; }

        /// <inheritdoc />
        public bool HasParser { get; }

        /// <inheritdoc />
        public bool HasStructure { get; }

        /// <summary>
        /// TryMagic
        /// </summary>
        /// <param name="stream"></param>
        /// <returns></returns>
        /// <exception cref="IOException"></exception>
        /// <exception cref="ObjectDisposedException"></exception>
        public bool TryMagic([NotNull] Stream stream)
        {
            var position = stream.Position;
            var length = stream.Length;
          
            byte?[] magicBytesValue = Magic!.MagicBytes.Value;
            if (position + magicBytesValue.Length +(long) Magic.Offset > length)
                return false;

            stream.Seek(position + (long) Magic.Offset, SeekOrigin.Begin);

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
        public bool TryParse([NotNull] BinaryReader reader, IResult result, [NotNullWhen(true)] out IParsed? parsed)
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
                _logger.LogError(ex, "Error while parsing with: {RuleName}", GetType().Name);
                parsed = null;
            }

            reader.SetPosition(position);
            return tryParseInternal;
        }

        protected virtual bool TryParseInternal(BinaryReader reader, IResult result, [NotNullWhen(true)] out IParsed? parsed)
        {
            parsed = null;
            return false;
        }

        /// <inheritdoc />
        public bool TryStructure([NotNull] BinaryReader reader, IResult result)
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
                _logger.LogError(ex, "Error while TryingStructure with: {RuleName}", GetType().Name);
            }

            reader.SetPosition(position);
            return tryStructureInternal;
        }

        protected virtual bool TryStructureInternal([NotNull] BinaryReader reader, IResult result)
        {
            return false;
        }
    }
}