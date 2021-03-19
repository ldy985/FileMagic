using ldy985.FileMagic.Abstracts;
using Microsoft.Extensions.Logging;

namespace ldy985.FileMagic.Core.Rules.Rules
{
    /// <summary>
    /// https://docs.microsoft.com/en-us/uwp/api/windows.ui.xaml.markup.xamlbinarywriter?view=winrt-19041
    /// https://en.wikipedia.org/wiki/Binary_Application_Markup_Language
    /// </summary>
    public class XBFRule : BaseRule
    {
        /// <inheritdoc />
        public override IMagic Magic { get; } = new Magic("58424600????0000", 0);

        /// <inheritdoc />
        public override ITypeInfo TypeInfo { get; } = new TypeInfo("Binary representation of a XAML", "XBF");

        /// <inheritdoc />
        public XBFRule(ILogger<XBFRule> logger) : base(logger)
        {
        }
    }
}