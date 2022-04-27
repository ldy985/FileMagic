using ldy985.FileMagic.Abstracts;
using ldy985.FileMagic.Abstracts.Enums;
using ldy985.FileMagic.Core;
using Microsoft.Extensions.Logging;

namespace FileMagic.Benchmarks.Helpers
{
    public class TestRule : BaseRule
    {
        /// <inheritdoc />
        public TestRule(ILogger<TestRule> logger) : base(logger) { }

        public override IMagic Magic { get; } = new Magic("00000000000102");
        /// <inheritdoc />
        public override Quality Quality => Quality.Medium;
        public override ITypeInfo TypeInfo { get; } = new TypeInfo("Test", "EXE");
    }
}