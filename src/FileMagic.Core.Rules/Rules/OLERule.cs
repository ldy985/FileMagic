using System.Diagnostics.CodeAnalysis;
using System.Text;
using ldy985.FileMagic.Abstracts;
using Microsoft.Extensions.Logging;
using OpenMcdf;
using OpenMcdf.Extensions;
using OpenMcdf.Extensions.OLEProperties;

namespace ldy985.FileMagic.Core.Rules.Rules;

public class OLERule : BaseRule
{
    private const string _metaInfoName = "\u0005SummaryInformation";

    /// <inheritdoc />
    public OLERule(ILogger<OLERule> logger) : base(logger)
    {
        Encoding.RegisterProvider(CodePagesEncodingProvider.Instance);
    }

    /// <inheritdoc />
    public override IMagic Magic { get; } = new Magic("D0CF11E0A1B11AE1");

    /// <inheritdoc />
    public override Quality Quality => Quality.High;

    public override ITypeInfo TypeInfo { get; } = new TypeInfo("Object Linking and Embedding (OLE) Compound File", "DOC", "DOT", "PPS", "PPT", "XLA",
        "XLS", "WIZ", "AC_", "ADP", "DB", "MSC", "MSG", "MSI", "MTW", "MXD", "OPT", "PUB", "QBM", "RVT", "SUO", "SPO", "VSD", "WPS");

    /// <inheritdoc />
    protected override bool TryParseInternal(BinaryReader reader, IResult result, [NotNullWhen(true)] out IParsed? parsed)
    {
        using CompoundFile compoundFile = new CompoundFile(reader.BaseStream, CFSUpdateMode.ReadOnly,
            CFSConfiguration.LeaveOpen | CFSConfiguration.NoValidationException);

        OLEData oleData = new OLEData();
        compoundFile.RootStorage.VisitEntries(item => oleData.Directories.Add(item.Name), true);

        if (oleData.Directories.Contains(_metaInfoName))
        {
            if (compoundFile.RootStorage.TryGetStream(_metaInfoName, out CFStream? tryGetStream))
                try
                {
                    OLEPropertiesContainer propertySetStream = tryGetStream.AsOLEPropertiesContainer();

                    foreach (OLEProperty oleProperty in propertySetStream.Properties)
                    {
                        string? name = oleProperty.PropertyIdentifier.GetDescription(ContainerType.SummaryInfo);
                        object? value = oleProperty.Value;
                        oleData.MetaInfo.Add(name, value);
                    }
                }
                catch (Exception ex)
                {
                    Logger.LogWarning(ex, "Failed to parse OLE SummaryInformation");
                }

            if (oleData.MetaInfo.TryGetValue("Application Name", out object? dataValue))
            {
                string value = (string)dataValue;
                bool any = false;

                if (value.Contains("Word", StringComparison.Ordinal))
                {
                    result.Description = "Microsoft Word document";
                    result.Extensions = new[] { "DOC", "DOT" };
                    any = true;
                }
                else if (value.Contains("Excel", StringComparison.Ordinal))

                {
                    result.Description = "Microsoft Excel document";
                    result.Extensions = new[] { "XLS" };
                    any = true;
                }
                else if (value.Contains("PowerPoint", StringComparison.Ordinal))
                {
                    result.Description = "Microsoft PowerPoint document";
                    result.Extensions = new[] { "PPT", "PPS" };
                    any = true;
                }
                else if (value.Contains("Installer", StringComparison.Ordinal) || value.Contains("InstallShield", StringComparison.Ordinal))
                {
                    result.Description = "Windows MSI installer";
                    result.Extensions = new[] { "MSI" };
                    any = true;
                }
                else
                {
                    Logger.LogWarning("Unhandled Application Name: {AppName}", value);
                }

                if (any)
                {
                    parsed = oleData;
                    return true;
                }
            }
        }

        foreach (string name in oleData.Directories)
        {
            bool any = false;

            switch (name)
            {
                case "WordDocument":
                    result.Description = "Microsoft Word document";
                    result.Extensions = new[] { "DOC", "DOT" };
                    any = true;
                    break;
                case "PowerPoint Document":
                    result.Description = "Microsoft PowerPoint document";
                    result.Extensions = new[] { "PPT", "PPS" };
                    any = true;
                    break;
                case "Workbook":
                    result.Description = "Microsoft Excel document";
                    result.Extensions = new[] { "XLS" };
                    any = true;
                    break;
                case "Envelope":
                    result.Description = "Microsoft Publisher document";
                    result.Extensions = new[] { "PUB" };
                    any = true;
                    break;
                case "DataAccessPages":
                    result.Description = "Microsoft Access document";
                    result.Extensions = new[] { "ADP" };
                    any = true;
                    break;
                case "__nameid_version1.0"
                    : //https://docs.microsoft.com/en-us/openspecs/exchange_server_protocols/ms-oxmsg/b046868c-9fbf-41ae-9ffb-8de2bd4eec82?redirectedfrom=MSDN
                    result.Description = "Microsoft Outlook Item File";
                    result.Extensions = new[] { "MSG" };
                    any = true;
                    break;
            }

            if (!any)
                continue;

            parsed = oleData;
            return true;
        }

        parsed = null!;

        return false;
    }
}

public class OLEData : IParsed
{
    public OLEData()
    {
        Directories = new List<string>();
        MetaInfo = new Dictionary<string, object>();
    }

    public IList<string> Directories { get; }
    public IDictionary<string, object> MetaInfo { get; }
}