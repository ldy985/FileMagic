using ldy985.FileMagic.Abstracts;
using Microsoft.Extensions.Logging;

namespace ldy985.FileMagic.Core.Rules.Rules;

/// <summary>
///     https://github.com/mitsuhiko/frostbite2-stuff
///     <code>
/// struct entry {
///    char header[4];
///    char sha1[20];
///    int32 data_length;
///    char padding[4];
/// };
/// </code>
/// </summary>
public class CASRule : BaseRule
{
    /// <inheritdoc />
    public CASRule(ILogger<CASRule> logger) : base(logger) { }

    /// <inheritdoc />
    public override IMagic Magic { get; } = new Magic("FACE0FF0");

    /// <inheritdoc />
    public override Quality Quality => Quality.High;

    public override ITypeInfo TypeInfo { get; } = new TypeInfo("Frostbite CAS file", "CAS");
}

/// <summary>
///     https://github.com/mitsuhiko/frostbite2-stuff
///     <code>
/// struct entry {
///     char sha1[20];
///     int32 offset;
///     int32 size;
///     int32 cas_num;
/// }
/// </code>
/// </summary>
public class CATRule : BaseRule
{
    /// <inheritdoc />
    public CATRule(ILogger<CATRule> logger) : base(logger) { }

    /// <inheritdoc />
    public override IMagic Magic { get; } = new Magic("4E79616E4E79616E4E79616E4E79616E");

    /// <inheritdoc />
    public override Quality Quality => Quality.High;

    public override ITypeInfo TypeInfo { get; } = new TypeInfo("Frostbite Catalog file ", "CAT");
}

/// <summary>
///     https://github.com/mitsuhiko/frostbite2-stuff
///     <code>
/// struct entry {
///     char sha1[20];
///     int32 offset;
///     int32 size;
///     int32 cas_num;
/// }
/// </code>
/// </summary>
public class SBRule : BaseRule
{
    /// <inheritdoc />
    public SBRule(ILogger<SBRule> logger) : base(logger) { }

    /// <inheritdoc />
    public override IMagic Magic { get; } = new Magic("00D1CE00");

    /// <inheritdoc />
    public override Quality Quality => Quality.High;

    public override ITypeInfo TypeInfo { get; } = new TypeInfo("Frostbite SuperBundle file ", "SB");
}