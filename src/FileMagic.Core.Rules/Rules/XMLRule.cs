using ldy985.FileMagic.Abstracts;
using Microsoft.Extensions.Logging;

namespace ldy985.FileMagic.Core.Rules.Rules;

public class XMLRule : BaseRule
{
    /// <inheritdoc />
    public XMLRule(ILogger<XMLRule> logger) : base(logger) { }

    /// <inheritdoc />
    public override IMagic Magic { get; } = new Magic("3c3f786d6c2076657273696f6e3d");

    /// <inheritdoc />
    public override Quality Quality => Quality.Medium;

    public override ITypeInfo TypeInfo { get; } = new TypeInfo("XML based file", "WSDL", "XML", "CONFIG");
}

public class XMLBOMRule : BaseRule
{
    /// <inheritdoc />
    public XMLBOMRule(ILogger<XMLBOMRule> logger) : base(logger) { }

    /// <inheritdoc />
    public override IMagic Magic { get; } = new Magic("EFBBBF3C3F786D6C2076657273696F6E3D");

    /// <inheritdoc />
    public override Quality Quality => Quality.Medium;

    public override ITypeInfo TypeInfo { get; } = new TypeInfo("XML with BOM based file", "WSDL", "XML", "CONFIG");
}