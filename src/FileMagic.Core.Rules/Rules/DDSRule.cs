using ldy985.FileMagic.Abstracts;
using Microsoft.Extensions.Logging;

namespace ldy985.FileMagic.Core.Rules.Rules;

/// <summary>
///     http://doc.51windows.net/directx9_sdk/graphics/reference/DDSFileReference/ddsfileformat.htm
/// </summary>
public class DDSRule : BaseRule
{
    /// <inheritdoc />
    public DDSRule(ILogger<DDSRule> logger) : base(logger) { }

    /// <inheritdoc />
    public override IMagic Magic { get; } = new Magic("444453207C00");

    public override Quality Quality => Quality.Medium;

    public override ITypeInfo TypeInfo { get; } = new TypeInfo("Microsoft DirectDraw Surface file format ", "DDS", "PNG");
}