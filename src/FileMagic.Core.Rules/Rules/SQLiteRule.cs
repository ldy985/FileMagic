using ldy985.FileMagic.Abstracts;
using Microsoft.Extensions.Logging;

namespace ldy985.FileMagic.Core.Rules.Rules;

public class SQLiteRule : BaseRule
{
    /// <inheritdoc />
    public SQLiteRule(ILogger<SQLiteRule> logger) : base(logger) { }

    /// <inheritdoc />
    public override IMagic Magic { get; } = new Magic("53514C69746520666F726D6174203300");

    /// <inheritdoc />
    public override Quality Quality => Quality.Medium;

    public override ITypeInfo TypeInfo { get; } = new TypeInfo("SQLite database file", "DB", "IDE");
}