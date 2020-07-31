using ldy985.FileMagic.Abstracts;
using Microsoft.Extensions.Logging;

namespace ldy985.FileMagic.Core.Rules.Rules
{
    public class MachORule : BaseRule
    {
        /// <inheritdoc />
        public override IMagic Magic { get; } = new Magic("CAFEBABE", 0);

        /// <inheritdoc />
        public override ITypeInfo TypeInfo { get; } = new TypeInfo("Mach-o Fat binary", "", "JNILIB");

        /// <inheritdoc />
        public MachORule(ILogger<MachORule> logger) : base(logger) { }
    }

    public class MachO32Rule : BaseRule
    {
        /// <inheritdoc />
        public override IMagic Magic { get; } = new Magic("FEEDFACE", 0);

        /// <inheritdoc />
        public override ITypeInfo TypeInfo { get; } = new TypeInfo("Mach-o 32-bit binary", "", "JNILIB");

        /// <inheritdoc />
        public MachO32Rule(ILogger<MachO32Rule> logger) : base(logger) { }
    }

    public class MachO64Rule : BaseRule
    {
        /// <inheritdoc />
        public override IMagic Magic { get; } = new Magic("FEEDFACF", 0);

        /// <inheritdoc />
        public override ITypeInfo TypeInfo { get; } = new TypeInfo("Mach-o 64-bit binary", "", "JNILIB");

        /// <inheritdoc />
        public MachO64Rule(ILogger<MachO64Rule> logger) : base(logger) { }
    }

    public class MachO32BERule : BaseRule
    {
        /// <inheritdoc />
        public override IMagic Magic { get; } = new Magic("CEFAEDFE", 0);

        /// <inheritdoc />
        public override ITypeInfo TypeInfo { get; } = new TypeInfo("Mach-o 32-bit binary", "", "JNILIB");

        /// <inheritdoc />
        public MachO32BERule(ILogger<MachO32BERule> logger) : base(logger) { }
    }

    public class MachO64BERule : BaseRule
    {
        /// <inheritdoc />
        public override IMagic Magic { get; } = new Magic("CFFAEDFE", 0);

        /// <inheritdoc />
        public override ITypeInfo TypeInfo { get; } = new TypeInfo("Mach-o 64-bit binary", "", "JNILIB");

        /// <inheritdoc />
        public MachO64BERule(ILogger<MachO64BERule> logger) : base(logger) { }
    }
}