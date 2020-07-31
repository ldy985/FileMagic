using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using ldy985.FileMagic.Abstracts;
using Microsoft.Extensions.Logging;
using OpenMcdf;
using OpenMcdf.Extensions;
using OpenMcdf.Extensions.OLEProperties;

namespace ldy985.FileMagic.Core.Rules.Rules
{
    public class OLERule : BaseRule
    {
        private const string _metaInfoName = "\u0005SummaryInformation";

        /// <inheritdoc />
        public override IMagic Magic { get; } = new Magic("D0CF11E0A1B11AE1");

        public override ITypeInfo TypeInfo { get; } = new TypeInfo("Object Linking and Embedding (OLE) Compound File", "DOC", "DOT", "PPS", "PPT", "XLA", "XLS", "WIZ", "AC_", "ADP", "DB", "MSC", "MSG", "MSI", "MTW", "MXD", "OPT", "PUB", "QBM", "RVT", "SUO", "SPO", "VSD", "WPS");

        /// <inheritdoc />
        protected override bool TryParseInternal(BinaryReader reader, IResult result, out object parsed)
        {
            CompoundFile compoundFile = new CompoundFile(reader.BaseStream, CFSUpdateMode.ReadOnly, CFSConfiguration.LeaveOpen | CFSConfiguration.NoValidationException);
            OLEData oleData = new OLEData();
            compoundFile.RootStorage.VisitEntries(item => oleData.Directories.Add(item.Name), true);

            if (oleData.Directories.Contains(_metaInfoName))
            {
                oleData.MetaInfo = new Dictionary<string, object>();
                CFStream tryGetStream = compoundFile.RootStorage.TryGetStream(_metaInfoName);
                if (tryGetStream != null)
                {
                    try
                    {
                        //todo use new code from git repo.
                        PropertySetStream propertySetStream = tryGetStream.AsOLEProperties();
                        if (propertySetStream.PropertySet0.NumProperties == propertySetStream.PropertySet0.PropertyIdentifierAndOffsets.Count && propertySetStream.PropertySet0.NumProperties == propertySetStream.PropertySet0.Properties.Count)
                        {
                            for (int index = 0; index < propertySetStream.PropertySet0.PropertyIdentifierAndOffsets.Count; index++)
                            {
                                string name = propertySetStream.PropertySet0.PropertyIdentifierAndOffsets[index].PropertyIdentifier.GetDescription(SummaryInfoMap.SummaryInfo);
                                object value = propertySetStream.PropertySet0.Properties[index].Value;
                                if (name?.Length == 0)
                                    continue;

                                oleData.MetaInfo.Add(name, value);
                            }
                        }
                    }
                    catch (Exception)
                    {
                        // ignored
                    }
                }

                if (oleData.MetaInfo.TryGetValue("Application Name", out var dataValue))
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
                    else if (value.Contains("Installer", StringComparison.Ordinal) || value.Contains("InstallShieldr", StringComparison.Ordinal))
                    {
                        result.Description = "Windows MSI installer";
                        result.Extensions = new[] { "MSI" };
                        any = true;
                    }
                    else
                    {
                        Console.WriteLine(value);
                    }

                    //result.Description = "Microsoft PowerPoint document";
                    //result.Extensions = new[] { "PPT", "PPS" };
                    //any = true;
                    //result.Description = "Microsoft Excel document";
                    //result.Extensions = new[] { "XLS" };
                    //any = true;
                    //result.Description = "Microsoft Publisher document";
                    //result.Extensions = new[] { "PUB" };
                    //any = true;
                    //result.Description = "Microsoft Access document";
                    //result.Extensions = new[] { "ADP" };
                    //any = true;

                    if (any)
                    {
                        parsed = oleData;
                        compoundFile.Close();
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
                }

                if (!any)
                    continue;

                parsed = oleData;
                compoundFile.Close();
                return true;
            }

            parsed = null;
            compoundFile.Close();
            return false;
        }

        /// <inheritdoc />
        public OLERule(ILogger<OLERule> logger) : base(logger)
        {
            Encoding.RegisterProvider(CodePagesEncodingProvider.Instance);
        }
    }

    public class OLEData
    {
        /// <inheritdoc />
        public OLEData()
        {
            Directories = new List<string>();
        }

        public List<string> Directories { get; set; }
        public IDictionary<string, object> MetaInfo { get; set; }
    }
}