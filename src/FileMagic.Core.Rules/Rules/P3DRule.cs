using ldy985.FileMagic.Abstracts;
using Microsoft.Extensions.Logging;

namespace ldy985.FileMagic.Core.Rules.Rules
{
    /// <summary>
    ///     https://community.bistudio.com/wiki/P3D_File_Format_-_ODOLV4x
    /// </summary>
    public class P3DRule : BaseRule
    {
        /// <inheritdoc />
        public P3DRule(ILogger<P3DRule> logger) : base(logger) { }

        /// <inheritdoc />
        public override IMagic Magic { get; } = new Magic("4F444F4C34000000");

        public override ITypeInfo TypeInfo { get; } = new TypeInfo("Bohemia Interactive p3d file", "P3D");
    }
}