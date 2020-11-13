using System;
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
        private readonly ILogger<BaseRule> _logger;

        protected BaseRule(ILogger<BaseRule> logger)
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
        public abstract IMagic Magic { get; }

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
#if NETSTANDARD2_1
        public bool TryMagic(BinaryReader stream)
#else
        public bool TryMagic(BinaryReader stream)
#endif
        {
            if (Magic.Offset != 0)
            {
                long streamPosition = stream.GetPosition() + (long)Magic.Offset;
                if (streamPosition >= stream.GetLength())
                    return false;

                stream.SetPosition(streamPosition);
            }

            byte?[] magicBytesValue = Magic.MagicBytes.Value;
            if (stream.GetPosition() + magicBytesValue.Length > stream.GetLength())
                return false;

            foreach (byte? b in magicBytesValue)
            {
                if (!b.HasValue)
                {
                    stream.SkipBackwards(1);
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
#if NETSTANDARD2_1
        public bool TryParse(BinaryReader reader, IResult result, [NotNullWhen(true)]out IParsed? parsed)
#else
        public bool TryParse(BinaryReader reader, IResult result, out IParsed? parsed)
#endif
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

#if NETSTANDARD2_1
        protected virtual bool TryParseInternal(BinaryReader reader, IResult result, [NotNullWhen(true)]out IParsed? parsed)
#else
        protected virtual bool TryParseInternal(BinaryReader reader, IResult result, out IParsed? parsed)
#endif
        {
            parsed = null;
            return false;
        }

        /// <inheritdoc />
        public bool TryStructure(BinaryReader reader, IResult result)
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

        protected virtual bool TryStructureInternal(BinaryReader reader, IResult result)
        {
            return false;
        }
    }
}