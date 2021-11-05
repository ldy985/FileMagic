using ldy985.FileMagic.Abstracts;
using Microsoft.Extensions.Logging;

namespace ldy985.FileMagic.Core.Rules.Rules
{
    /// <summary>
    ///     http://justsolve.archiveteam.org/wiki/PVK
    ///     PVK File Format
    ///     PVK is a Microsoft proprietary format for storing a single RSA Private Key. The file consists of a number of
    ///     Windows cryptographic structures serialised into the file with a header. Because the format uses generic Windows
    ///     structures but can only store an RSA private key, several of the fields only have one possible value. The format
    ///     supports password protection using RC4 encryption. The format is little-endian.
    ///     Header
    ///     The file header is as follows:
    ///     Magic	int32	File identification value. Always 0xb0b5f11e.
    ///     Reserved	int32	Unknown. Always observed as zero.
    ///     Keytype	int32
    ///     Encrypted	int32	1 if the file is password protected (encrypted), 0 otherwise.
    ///     SaltLength	int32	The length of the salt data, in bytes. Non-zero (typically 16) if the file is password protected,
    ///     0 otherwise.
    ///     KeyLength	int32	The length of the key data, in bytes.
    ///     Salt	byte[$SaltLength]	The salt data, if the file is encrypted.
    ///     This is followed by three Windows' cryptography structures, PRIVATEKEYBLOB, PUBLICKEYSTRUC (also known as
    ///     BLOBHEADER), and RSAPUBKEY. (Technically there is only a PRIVATEKEYBLOB structure, but it incorporates the other
    ///     two.)
    ///     PRIVATEKEYBLOB
    ///     PRIVATEKEYBLOB structure
    ///     PUBLICKEYSTRUC structure
    ///     bType byte
    ///     bVersion byte
    ///     reserved int16
    ///     aiKeyAlg int32
    ///     rsapubkey RSAPUBKEY structure
    ///     magic int32
    ///     bitlen int32
    ///     pubexp int32
    ///     modulus byte[$rsapubkey.$bitlen / 8]
    ///     prime1 byte[$rsapubkey.$bitlen / 16]
    ///     prime2 byte[$rsapubkey.$bitlen / 16]
    ///     exponent1 byte[$rsapubkey.$bitlen / 16]
    ///     exponent2 byte[$rsapubkey.$bitlen / 16]
    ///     coefficient byte[$rsapubkey.$bitlen / 16]
    ///     privateExponent byte[$rsapubkey.$bitlen / 8]
    /// </summary>
    public class PVKRule : BaseRule
    {
        /// <inheritdoc />
        public PVKRule(ILogger<PVKRule> logger) : base(logger) { }

        /// <inheritdoc />
        public override IMagic Magic { get; } = new Magic("B0B5F11E");

        /// <inheritdoc />
        public override ITypeInfo TypeInfo { get; } = new TypeInfo("Microsoft proprietary format for storing a single RSA Private Key", "PVK");
    }
}