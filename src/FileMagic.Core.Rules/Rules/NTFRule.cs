using ldy985.FileMagic.Abstracts;
using Microsoft.Extensions.Logging;

namespace ldy985.FileMagic.Core.Rules.Rules
{
    /// <summary>
    ///     <para>https://docs.microsoft.com/en-us/windows-hardware/drivers/print/pscript-minidrivers#ntf-files</para>
    ///     <para>
    ///         Windows 2000 and later font files (.ntf files) are binary files used for describing the device fonts of
    ///         printers supported by Pscript.
    ///         Microsoft provides a default .ntf file, named pscript.ntf, that contains descriptions of commonly encountered
    ///         US device fonts. For East Asian printers, Microsoft also provides a default .ntf file, named pscrptfe.ntf,
    ///         which contains descriptions of commonly encountered East Asian device fonts.
    ///         In addition, hardware vendors can supply device font descriptions for fonts not supported by pscript.ntf. These
    ///         font descriptions can be created by converting AFM files to NTF files. Customized, printer model-specific .ntf
    ///         files can be installed by listing them as dependent files in a printer INF file. For more information, see
    ///         Installing a Pscript Minidriver.
    ///         Pscript searches for font metrics by first checking printer model-specific .ntf files, then checking
    ///         pscript.ntf, and using the first font description found.
    ///     </para>
    /// </summary>
    public class NTFRule : BaseRule
    {
        /// <inheritdoc />
        public NTFRule(ILogger<NTFRule> logger) : base(logger) { }

        /// <inheritdoc />
        public override IMagic Magic { get; } = new Magic("3146544E5350544E");

        /// <inheritdoc />
        public override ITypeInfo TypeInfo { get; } = new TypeInfo("Binary device font descriptor for Pscript", "NTF");
    }
}