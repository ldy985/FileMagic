using System.Diagnostics.CodeAnalysis;
using ldy985.BinaryReaderExtensions;
using ldy985.FileMagic.Abstracts;
using Microsoft.Extensions.Logging;

namespace ldy985.FileMagic.Core.Rules.Rules.Media;

public class GIFRule : BaseRule
{
    /// <inheritdoc />
    public GIFRule(ILogger<GIFRule> logger) : base(logger) { }

    /// <inheritdoc />
    public override IMagic Magic { get; } = new Magic("474946");

    /// <inheritdoc />
    public override ITypeInfo TypeInfo { get; } = new TypeInfo("Gif image", "GIF");

    /// <inheritdoc />
    public override Quality Quality => Quality.VeryHigh;

    /// <inheritdoc />
    protected override bool TryParseInternal(BinaryReader reader, IResult result, [NotNullWhen(true)] out IParsed? parsed)
    {
        reader.SkipForwards(6);
        GIF gif = new GIF();
        gif.Width = reader.ReadUInt16();
        gif.Height = reader.ReadUInt16();

        parsed = gif;
        return true;
    }

    protected override bool TryStructureInternal(BinaryReader reader, IResult result)
    {
        reader.SkipForwards(3);
        ushort type = reader.ReadUInt16();
        byte a = reader.ReadByte();

        switch (type)
        {
            case 14136 when a == 97: //87a
            case 14648 when a == 97: //89a
                return true;
            default:
                return false;
        }
    }

    public class GIF : IParsed
    {
        public ushort Height { get; set; }
        public ushort Width { get; set; }
    }
}