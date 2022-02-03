using ldy985.BinaryReaderExtensions;
using ldy985.FileMagic.Abstracts;
using Microsoft.Extensions.Logging;

namespace ldy985.FileMagic.Core.Rules.Rules;

/// <summary>
///     https://docs.microsoft.com/en-us/previous-versions/bb417345(v=msdn.10)
///     Windows Update Binary Delta Compression
///     https://github.com/hfiref0x/SXSEXP
/// </summary>
public class MSPatchRule : BaseRule
{
    /// <inheritdoc />
    public MSPatchRule(ILogger<MSPatchRule> logger) : base(logger) { }

    /// <inheritdoc />
    public override IMagic Magic { get; } = new Magic("50413330", 4);

    /// <inheritdoc />
    public override Quality Quality => Quality.High;

    public override ITypeInfo TypeInfo { get; } = new TypeInfo("Microsoft delta patch data");
}

public class DC_MSPatchRule : BaseRule
{
    /// <inheritdoc />
    public DC_MSPatchRule(ILogger<DC_MSPatchRule> logger) : base(logger) { }

    /// <inheritdoc />
    public override IMagic Magic { get; } = new Magic("4443??01");

    /// <inheritdoc />
    public override Quality Quality => Quality.High;

    public override ITypeInfo TypeInfo { get; } = new TypeInfo("Microsoft delta patch data");

    protected override bool TryStructureInternal(BinaryReader reader, IResult result)
    {
        reader.SkipForwards(2); //already checked by magic
        byte type = reader.ReadByte();

        return type switch
        {
            (byte)'D' => true,
            (byte)'H' => true,
            (byte)'M' => true,
            (byte)'N' => true,
            (byte)'S' => true,
            (byte)'X' => true,
            _ => false
        };
    }
}