using ldy985.FileMagic.Abstracts;
using Microsoft.Extensions.Logging;

namespace ldy985.FileMagic.Core.Rules.Rules.Media;

/// <summary>
///     https://en.wikipedia.org/wiki/ISO_base_media_file_format
///     http://www.file-recovery.com/mp4-signature-format.htm
///     http://www.ftyps.com/
/// </summary>
public class ISO_MediaContainerRule : BaseRule
{
    /// <inheritdoc />
    public ISO_MediaContainerRule(ILogger<ISO_MediaContainerRule> logger) : base(logger) { }

    /// <inheritdoc />
    public override IMagic Magic { get; } = new Magic("66747970", 4);

    /// <inheritdoc />
    public override Quality Quality => Quality.Medium;

    public override ITypeInfo TypeInfo { get; } = new TypeInfo("ISO Media Container File ", "MP4", "MOV", "MANY OTHER");
}

public class QuickTimeContainerRule : BaseRule
{
    /// <inheritdoc />
    public QuickTimeContainerRule(ILogger<QuickTimeContainerRule> logger) : base(logger) { }

    /// <inheritdoc />
    public override IMagic Magic { get; } = new Magic("6D6F6F76", 4);

    /// <inheritdoc />
    public override Quality Quality => Quality.Medium;

    public override ITypeInfo TypeInfo { get; } = new TypeInfo("QuickTime Container File ", "MOV");
}