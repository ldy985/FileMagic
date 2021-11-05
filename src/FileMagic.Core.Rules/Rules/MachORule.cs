using ldy985.FileMagic.Abstracts;
using Microsoft.Extensions.Logging;

namespace ldy985.FileMagic.Core.Rules.Rules
{
    public class MachORule : BaseRule
    {
        /// <inheritdoc />
        public MachORule(ILogger<MachORule> logger) : base(logger) { }

        /// <inheritdoc />
        public override IMagic Magic { get; } = new Magic("CAFEBABE");

        /// <inheritdoc />
        public override ITypeInfo TypeInfo { get; } = new TypeInfo("Mach-o Fat binary", "", "JNILIB");
    }

    public class MachO32Rule : BaseRule
    {
        /// <inheritdoc />
        public MachO32Rule(ILogger<MachO32Rule> logger) : base(logger) { }

        /// <inheritdoc />
        public override IMagic Magic { get; } = new Magic("FEEDFACE");

        /// <inheritdoc />
        public override ITypeInfo TypeInfo { get; } = new TypeInfo("Mach-o 32-bit binary", "", "JNILIB");
    }

    public class MachO64Rule : BaseRule
    {
        /// <inheritdoc />
        public MachO64Rule(ILogger<MachO64Rule> logger) : base(logger) { }

        /// <inheritdoc />
        public override IMagic Magic { get; } = new Magic("FEEDFACF");

        /// <inheritdoc />
        public override ITypeInfo TypeInfo { get; } = new TypeInfo("Mach-o 64-bit binary", "", "JNILIB");
    }

    public class MachO32BERule : BaseRule
    {
        /// <inheritdoc />
        public MachO32BERule(ILogger<MachO32BERule> logger) : base(logger) { }

        /// <inheritdoc />
        public override IMagic Magic { get; } = new Magic("CEFAEDFE");

        /// <inheritdoc />
        public override ITypeInfo TypeInfo { get; } = new TypeInfo("Mach-o 32-bit binary", "", "JNILIB");
    }

    public class MachO64BERule : BaseRule
    {
        /// <inheritdoc />
        public MachO64BERule(ILogger<MachO64BERule> logger) : base(logger) { }

        /// <inheritdoc />
        public override IMagic Magic { get; } = new Magic("CFFAEDFE");

        /// <inheritdoc />
        public override ITypeInfo TypeInfo { get; } = new TypeInfo("Mach-o 64-bit binary", "", "JNILIB");
    }
}