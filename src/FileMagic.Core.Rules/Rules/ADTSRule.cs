using ldy985.FileMagic.Abstracts;
using ldy985.NumberExtensions;
using Microsoft.Extensions.Logging;

namespace ldy985.FileMagic.Core.Rules.Rules;

public class ADTS4Rule : BaseRule
{
    /// <inheritdoc />
    public ADTS4Rule(ILogger<ADTS4Rule> logger) : base(logger) { }

    /// <inheritdoc />
    public override IMagic Magic { get; } = new Magic("FFF1");

    /// <inheritdoc />
    public override Quality Quality => Quality.Medium;

    /// <inheritdoc />
    public override ITypeInfo TypeInfo { get; } = new TypeInfo("Audio Data Transport Stream", "AAC");

    protected override bool TryStructureInternal(BinaryReader reader, IResult result)
    {
        return ADTSShared.B(reader);
    }
}

public class ADTS2Rule : BaseRule
{
    /// <inheritdoc />
    public ADTS2Rule(ILogger<ADTS2Rule> logger) : base(logger) { }

    /// <inheritdoc />
    public override IMagic Magic { get; } = new Magic("FFF9");

    /// <inheritdoc />
    public override Quality Quality => Quality.Medium;

    /// <inheritdoc />
    public override ITypeInfo TypeInfo { get; } = new TypeInfo("Audio Data Transport Stream", "AAC");

    protected override bool TryStructureInternal(BinaryReader reader, IResult result)
    {
        return ADTSShared.B(reader);
    }
}

internal static class ADTSShared
{
    internal static bool B(BinaryReader reader)
    {
        if (reader.ReadByte() != 0b11111111)
            return false;

        byte sync = reader.ReadByte();

        // AAAABCCD
        // A MUST be 1
        // C MUST be 0
        if (!sync.GetBit(7) || !sync.GetBit(6) || !sync.GetBit(5) || !sync.GetBit(4))
            return false;

        if (sync.GetBit(2) || sync.GetBit(1))
            return false;

        return true;
    }
}