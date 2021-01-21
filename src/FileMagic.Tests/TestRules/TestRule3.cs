using System.Diagnostics.CodeAnalysis;
using ldy985.FileMagic.Abstracts;
using ldy985.FileMagic.Core;
using Microsoft.Extensions.Logging;

namespace ldy985.FileMagic.Tests.TestRules
{
    [SuppressMessage("General", "RCS1079:Throwing of new NotImplementedException.", Justification = "<Pending>")]
    internal class TestRule3 : BaseRule
    {
        /// <inheritdoc />
        public TestRule3(ILogger<TestRule3> logger) : base(logger) { }

        public override IMagic Magic { get; } = new Magic("000102??");

        /// <inheritdoc />
        public override ITypeInfo TypeInfo { get; } = new TypeInfo("test", "test");
    }
}