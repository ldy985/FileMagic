using ldy985.FileMagic.Abstracts;
using Microsoft.Extensions.Logging;

namespace ldy985.FileMagic.Core.Rules.Rules.Containers
{
    public class GZipRule : BaseRule
    {
        /// <inheritdoc />
        public override IMagic Magic { get; } = new Magic("1f8b", 0);

        public override ITypeInfo TypeInfo { get; } = new TypeInfo("GZIP file format", "GZ", "TAR.GZ", "TGZ");

        ///// <inheritdoc />
        //protected override bool TryParseInternal(BinaryReader reader, Result result, out object parsed)
        //{
        //    using (GZipStream gZipStream = new GZipStream(reader.BaseStream, CompressionMode.Decompress, true))
        //    {
        //        using (var stream = new MemoryStream())
        //        {
        //            byte[] buffer = new byte[2048]; // read in chunks of 2KB
        //            int bytesRead;
        //            while((bytesRead = gZipStream.Read(buffer, 0, buffer.Length)) > 0)
        //            {
        //                stream.Write(buffer, 0, bytesRead);
        //            }

        //            byte[] array = stream.ToArray();
        //            parsed = array;
        //            string s = Encoding.ASCII.GetString(array);    
        //        }
        //    }
        //    return true;
        //}

        /// <inheritdoc />
        public GZipRule(ILogger<GZipRule> logger) : base(logger) { }
    }
}