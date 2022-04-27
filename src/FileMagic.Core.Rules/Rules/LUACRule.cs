using ldy985.FileMagic.Abstracts;
using Microsoft.Extensions.Logging;

namespace ldy985.FileMagic.Core.Rules.Rules;

/// <summary>
///     http://domoticx.com/lua-bytecode-ontcijferen-luac/
///     https://github.com/horsicq/Detect-It-Easy/blob/master/db/Binary/formats.1.sg#L103
/// </summary>
public class LUACRule : BaseRule
{
    /// <inheritdoc />
    public LUACRule(ILogger<LUACRule> logger) : base(logger) { }

    /// <inheritdoc />
    public override IMagic Magic { get; } = new Magic("1b4c7561??000104040408");

    /// <inheritdoc />
    public override Quality Quality => Quality.High;

    public override ITypeInfo TypeInfo { get; } = new TypeInfo("Lua bytecode", "LUAC");
}