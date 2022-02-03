using ldy985.FileMagic.Abstracts;
using Microsoft.Extensions.Logging;

namespace ldy985.FileMagic.Core.Rules.Rules.Containers;

public class VHDRule : BaseRule
{
    /// <inheritdoc />
    public VHDRule(ILogger<VHDRule> logger) : base(logger) { }

    /// <inheritdoc />
    public override IMagic Magic { get; } = new Magic("636F6E6563746978");

    /// <inheritdoc />
    public override Quality Quality => Quality.High;

    public override ITypeInfo TypeInfo { get; } = new TypeInfo("Virtual PC Virtual HD image", "VHD");
}