using ldy985.BinaryReaderExtensions;
using ldy985.FileMagic.Abstracts;
using Microsoft.Extensions.Logging;

namespace ldy985.FileMagic.Core.Rules.Rules.Media;

public class ICORule : BaseRule
{
    /// <inheritdoc />
    public ICORule(ILogger<ICORule> logger) : base(logger) { }

    /// <inheritdoc />
    public override IMagic? Magic { get; }

    public override ITypeInfo TypeInfo { get; } = new TypeInfo("Icon file", "ICO", "CUR");

    /// <inheritdoc />
    public override Quality Quality => Quality.High;

    protected override bool TryStructureInternal(BinaryReader reader, IResult result)
    {
        if (reader.ReadUInt16() != 0)
            return false;

        ushort type = reader.ReadUInt16();
        if (type != 1 && type != 2)
            return false;

        bool foundError = false;
        ushort imageCount = reader.ReadUInt16();

        for (int i = 0; i < imageCount; i++)
        {
            reader.SkipForwards(3);
            uint reserved = reader.ReadByte();
            if (reserved is 0 or 255)
                continue;

            foundError = true;
            break;
        }

        return !foundError;
    }
}