using ldy985.FileMagic.Abstracts;
using ldy985.FileMagic.Core;
using Microsoft.Extensions.Logging;

namespace ldy985.FileMagic.Benchmarks.Helpers
{
    public class TestRule : BaseRule
    {
        public override IMagic Magic { get; } = new Magic("00000000000102");
        public override ITypeInfo TypeInfo { get; } = new TypeInfo("Test", "EXE");

        /// <inheritdoc />
        public TestRule(ILogger<TestRule> logger) : base(logger) { }
    }
}