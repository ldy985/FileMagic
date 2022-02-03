using ldy985.FileMagic.Abstracts;
using Microsoft.Extensions.Logging;

namespace ldy985.FileMagic.Core.Rules.Rules
{
    public class WASMRule : BaseRule
    {
        /// <inheritdoc />
        public WASMRule(ILogger<WASMRule> logger) : base(logger) { }

        /// <inheritdoc />
        public override IMagic Magic { get; } = new Magic("0061736d");
        /// <inheritdoc />
        public override Quality Quality => Quality.High;
        public override ITypeInfo TypeInfo { get; } = new TypeInfo("WebAssembly binary format", "WASM");
    }
}