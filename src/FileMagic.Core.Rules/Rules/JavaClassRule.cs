using System.Diagnostics.CodeAnalysis;
using System.IO;
using ldy985.BinaryReaderExtensions;
using ldy985.FileMagic.Abstracts;
using ldy985.NumberExtensions;
using Microsoft.Extensions.Logging;

namespace ldy985.FileMagic.Core.Rules.Rules
{
    /// <summary>https://docs.oracle.com/javase/specs/jvms/se12/html/jvms-4.html</summary>
    public class JavaClassRule : BaseRule
    {
        private const short _newestJavaMajorVersion = 56;

        /// <inheritdoc />
        public override IMagic Magic { get; } = new Magic("CAFEBABE");

        public override ITypeInfo TypeInfo { get; } = new TypeInfo("Java class file", "CLASS");

        /// <inheritdoc />
        protected override bool TryStructureInternal([NotNull] BinaryReader reader, IResult result)
        {
            reader.SkipForwards(4);
            ushort minorVersion = reader.ReadUInt16().Reverse();
            ushort majorVersion = reader.ReadUInt16().Reverse();
            ushort constantPoolCount = reader.ReadUInt16().Reverse();

            if (majorVersion > _newestJavaMajorVersion)
                return false;

            if (majorVersion >= 56 && (minorVersion == 0 || minorVersion == 65535))
                return true;

            if (majorVersion > 45 && majorVersion <= 55)
                return constantPoolCount > 0;

            return false;
        }

        /// <inheritdoc />
        public JavaClassRule(ILogger<JavaClassRule> logger) : base(logger) { }
    }
}