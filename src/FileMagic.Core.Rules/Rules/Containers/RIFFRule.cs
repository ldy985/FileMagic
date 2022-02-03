using ldy985.BinaryReaderExtensions;
using ldy985.FileMagic.Abstracts;
using Microsoft.Extensions.Logging;

namespace ldy985.FileMagic.Core.Rules.Rules.Containers;

/// <summary>
///     https://en.wikipedia.org/wiki/Resource_Interchange_File_Format
///     https://www.daubnet.com/en/file-format-riff
///     https://johnloomis.org/cpe102/asgn/asgn1/riff.html
/// </summary>
public class RIFFRule : BaseRule
{
    /// <inheritdoc />
    public RIFFRule(ILogger<RIFFRule> logger) : base(logger) { }

    /// <inheritdoc />
    public override IMagic Magic { get; } = new Magic("52494646");

    /// <inheritdoc />
    public override Quality Quality => Quality.High;

    public override ITypeInfo TypeInfo { get; } = new TypeInfo("Resource Interchange File", "ANI", "AVI", "BND", "DXR", "PAL", "RDI", "RMI", "WAV");

    protected override bool TryStructureInternal(BinaryReader reader, IResult result)
    {
        reader.SkipForwards(4);
        uint size = reader.ReadUInt32();
        if (reader.GetLength() != size + 8)
            return false;

        uint riffType = reader.ReadUInt32();

        if (riffType == 0x45564157) //WAVE
            result.Extensions = new[] { "WAV" };

        if (riffType == 0x504D4D52) //RMMP
            result.Extensions = new[] { "AVI" };

        if (riffType == 0x42494452) //RDIB
            result.Extensions = new[] { "RDI" };

        //TODO add more riff types

        return true;
    }
}