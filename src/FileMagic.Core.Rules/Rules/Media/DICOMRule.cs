using ldy985.FileMagic.Abstracts;
using Microsoft.Extensions.Logging;

namespace ldy985.FileMagic.Core.Rules.Rules.Media;

/// <summary>
///     https://twitter.com/angealbertini/status/1616898220210229248
/// </summary>
public class DICOMRule : BaseRule
{
    /// <inheritdoc />
    public DICOMRule(ILogger<DICOMRule> logger) : base(logger) { }

    /// <inheritdoc />
    public override IMagic Magic { get; } = new Magic("4449434D", 128);

    /// <inheritdoc />
    public override Quality Quality => Quality.High;

    public override ITypeInfo TypeInfo { get; } = new TypeInfo("DICOM is the international standard to transmit, store, retrieve, print, process, and display medical imaging information.", "DCM");
}