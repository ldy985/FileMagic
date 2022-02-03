using ldy985.FileMagic.Abstracts;
using Microsoft.Extensions.Logging;

namespace ldy985.FileMagic.Core.Rules.Rules;

/// <summary>
///     https://github.com/libyal/libagdb/blob/master/documentation/Windows%20SuperFetch%20(DB)%20format.asciidoc
/// </summary>
public class SuperFetchRule : BaseRule
{
    /// <inheritdoc />
    public SuperFetchRule(ILogger<SuperFetchRule> logger) : base(logger) { }

    /// <inheritdoc />
    public override IMagic Magic { get; } = new Magic("4d454d4f");

    /// <inheritdoc />
    public override Quality Quality => Quality.High;

    public override ITypeInfo TypeInfo { get; } = new TypeInfo("Compressed SuperFetch database");
}

/// <summary>
///     https://github.com/libyal/libagdb/blob/master/documentation/Windows%20SuperFetch%20(DB)%20format.asciidoc
/// </summary>
public class MEM0SuperFetchRule : BaseRule
{
    /// <inheritdoc />
    public MEM0SuperFetchRule(ILogger<MEM0SuperFetchRule> logger) : base(logger) { }

    /// <inheritdoc />
    public override IMagic Magic { get; } = new Magic("4d454d30");

    /// <inheritdoc />
    public override Quality Quality => Quality.High;

    public override ITypeInfo TypeInfo { get; } = new TypeInfo("Compressed SuperFetch database");
}

/// <summary>
///     https://github.com/libyal/libagdb/blob/master/documentation/Windows%20SuperFetch%20(DB)%20format.asciidoc
/// </summary>
public class MAMSuperFetchRule : BaseRule
{
    /// <inheritdoc />
    public MAMSuperFetchRule(ILogger<MAMSuperFetchRule> logger) : base(logger) { }

    /// <inheritdoc />
    public override IMagic Magic { get; } = new Magic("4d414d84");

    /// <inheritdoc />
    public override Quality Quality => Quality.High;

    public override ITypeInfo TypeInfo { get; } = new TypeInfo("Compressed SuperFetch database");
}