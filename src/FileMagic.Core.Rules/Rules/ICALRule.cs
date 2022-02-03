using ldy985.FileMagic.Abstracts;
using Microsoft.Extensions.Logging;

namespace ldy985.FileMagic.Core.Rules.Rules;

/// <summary>
///     https://www.webdavsystem.com/server/creating_caldav_carddav/calendar_ics_file_structure/
///     https://icalendar.org/
/// </summary>
public class ICALRule : BaseRule
{
    /// <inheritdoc />
    public ICALRule(ILogger<ICALRule> logger) : base(logger) { }

    /// <inheritdoc />
    public override IMagic Magic { get; } = new Magic("424547494E3A5643414C454E444152");

    /// <inheritdoc />
    public override Quality Quality => Quality.High;

    public override ITypeInfo TypeInfo { get; } = new TypeInfo("iCalender file", "ICAL", "ICS", "IFB", "ICALENDAR");
}