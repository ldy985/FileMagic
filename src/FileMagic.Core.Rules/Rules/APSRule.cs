using ldy985.FileMagic.Abstracts;
using Microsoft.Extensions.Logging;

namespace ldy985.FileMagic.Core.Rules.Rules
{
    public class APSRule : BaseRule
    {
        public APSRule(ILogger<BaseRule> logger) : base(logger) { }

        public override IMagic Magic { get; } = new Magic("0000000020000000FFFF0000FFFF000000000000000000000000000000000000??000000240000004800570042000000FFFF01000000000000000C0400000000");

        public override ITypeInfo TypeInfo { get; } = new TypeInfo("File created by Microsoft Visual C++, a software development application; stores the binary representation of a resource included with the project; enables the application to load resources more quickly.", "APS");
    }
}