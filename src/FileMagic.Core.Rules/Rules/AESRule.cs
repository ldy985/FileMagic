using ldy985.FileMagic.Abstracts;
using Microsoft.Extensions.Logging;

namespace ldy985.FileMagic.Core.Rules.Rules
{
    /// <summary>
    /// https://www.aescrypt.com/aes_file_format.html
    /// </summary>
    public class AESRule : BaseRule
    {
        public AESRule(ILogger<AESRule> logger) : base(logger) { }

        public override IMagic Magic { get; } = new Magic("414553??00");

        public override ITypeInfo TypeInfo { get; } = new TypeInfo("AES Crypt file format", "AES");
    }
}