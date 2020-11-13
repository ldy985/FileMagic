using System;
using System.Collections.Generic;
#if NETSTANDARD2_1
using System.Diagnostics.CodeAnalysis;
#endif
using System.IO;
using System.IO.Compression;
using ldy985.BinaryReaderExtensions;
using ldy985.FileMagic.Abstracts;
using Microsoft.Extensions.Logging;

namespace ldy985.FileMagic.Core.Rules.Rules.Containers
{
    /// <summary>
    /// https://pkware.cachefly.net/webdocs/casestudies/APPNOTE.TXT
    /// </summary>
    public class PKZipRule : BaseRule
    {
        /// <inheritdoc />
        public override IMagic Magic { get; } = new Magic("504B", 0);

        public override ITypeInfo TypeInfo { get; } = new TypeInfo("Zip file", "JAR", "WAR", "DOCX", "XLSX", "PPTX", "ODT", "ODS", "ODP", "ZIPX", "NUPKG", "ZIP", "APK", "EPUB");

        /// <inheritdoc />
#if NETSTANDARD2_1
        protected override bool TryParseInternal(BinaryReader reader, IResult result, [NotNullWhen(true)] out IParsed? parsed)
#else
        protected override bool TryParseInternal(BinaryReader reader, IResult result, out IParsed? parsed)
#endif
        {
            ZipArchive archive = new ZipArchive();
            using (System.IO.Compression.ZipArchive zipArchive = new System.IO.Compression.ZipArchive(reader.BaseStream, ZipArchiveMode.Read, true))
            {
                foreach (ZipArchiveEntry zipArchiveEntry in zipArchive.Entries)
                {
                    archive.AddFile(zipArchiveEntry);
                    string fullName = zipArchiveEntry.FullName;
                    switch (fullName)
                    {
                        case "word/document.xml":
                            result.Extensions = new[] { "DOCX", "DOCM", "DOTM" };
                            result.Description = "Microsoft Word document";
                            break;
                        case "ppt/presentation.xml":
                            result.Extensions = new[] { "PPTX", "PPTM", "POTX", "POTM", "PPSM", "PPTM", "PPAM" };
                            result.Description = "Microsoft PowerPoint document";
                            break;
                        case "xl/workbook.xml":
                        case "xl/workbook.bin":
                            result.Extensions = new[] { "XLSX", "XLSM", "XLTM", "XLTX", "XLSB", "XLAM" };
                            result.Description = "Microsoft Excel document";
                            break;
                    }
                }
            }

            parsed = archive;
            return true;
        }

        /// <inheritdoc />
        protected override bool TryStructureInternal(BinaryReader reader, IResult result)
        {
            switch (reader.ReadUInt32())
            {
                case 0x04034b50: // zip file record
                    reader.SkipForwards(14);
                    uint dataLen = reader.ReadUInt32();
                    reader.SkipForwards(4);
                    ushort len = reader.ReadUInt16();
                    ushort extralen = reader.ReadUInt16();
                    reader.SkipForwards(len + dataLen + extralen);
                    return reader.ReadUInt16() == 0x4b50;
                case 0x05054b50: // zip sig
                    ushort dataLen2 = reader.ReadUInt16();
                    reader.SkipForwards(dataLen2);
                    return reader.ReadUInt16() == 0x4b50;
                case 0x02014b50: // zip dir record
                    reader.SkipForwards(24);
                    ushort len2 = reader.ReadUInt16();
                    ushort extralen2 = reader.ReadUInt16();
                    ushort commentLen = reader.ReadUInt16();
                    reader.SkipForwards(12 + len2 + extralen2 + commentLen);
                    return reader.ReadUInt16() == 0x4b50;
                default:
                    return false;
            }

            //reader.SkipForwards(10);

            //reader.SkipForwards(10);
            //ushort time = reader.ReadUInt16(Endianness.LittleEndian);
            //ushort date = reader.ReadUInt16(Endianness.LittleEndian);
            //ushort time = 0x7d1c;
            //ushort date = 0x354b;
            //ushort hour = (ushort)(time >> 11);
            //ushort min = (ushort)((ushort)(time << 5) >> 5 + 5);
            //ushort sec = (ushort)((ushort)(time << 11) >> 11);

            //return false;
        }

        public class ZipArchive : IParsed
        {
            public List<ZipFile> Files { get; } = new List<ZipFile>();

            public void AddFile(ZipArchiveEntry zipArchiveEntry)
            {
                using (Stream stream = zipArchiveEntry.Open())
                {
                    int streamLength = (int)zipArchiveEntry.Length;
                    byte[] data = new byte[streamLength];
                    stream.Read(data, 0, streamLength);

                    ZipFile zipFile = new ZipFile();

                    zipFile.FullName = zipArchiveEntry.FullName;
                    zipFile.LastWriteTime = zipArchiveEntry.LastWriteTime;
                    zipFile.Data = data;
                    Files.Add(zipFile);
                }
            }
        }

        public class ZipFile
        {
            public string? FullName { get; set; }
            public DateTimeOffset? LastWriteTime { get; set; }
            public byte[]? Data { get; set; }
        }

        /// <inheritdoc />
        public PKZipRule(ILogger<PKZipRule> logger) : base(logger) { }
    }
}