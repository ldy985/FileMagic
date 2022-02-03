using ldy985.FileMagic.Abstracts;
using Microsoft.Extensions.Logging;

namespace ldy985.FileMagic.Core.Rules.Rules.FileSystems;

/// <summary>
///     https://en.wikipedia.org/wiki/NTFS
///     https://github.com/libyal/libfsntfs/blob/master/documentation/New%20Technologies%20File%20System%20(NTFS).asciidoc
/// </summary>
public class NTFSRule : BaseRule
{
    public NTFSRule(ILogger<NTFSRule> logger) : base(logger) { }
    public override IMagic Magic { get; } = new Magic("4e54465320202020", 3);

    /// <inheritdoc />
    public override Quality Quality => Quality.High;

    public override ITypeInfo TypeInfo { get; } = new TypeInfo("New Technologies File System (NTFS)");
}