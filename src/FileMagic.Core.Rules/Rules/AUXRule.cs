using ldy985.BinaryReaderExtensions;
using ldy985.FileMagic.Abstracts;
using Microsoft.Extensions.Logging;

namespace ldy985.FileMagic.Core.Rules.Rules
{
    /// <summary>
    ///     https://stackoverflow.com/questions/17681514/what-is-the-format-of-ni-dll-aux-files
    ///     each record has a subrecord type 0xa which appears to be a version number of sorts. subrecord type 0x3 might be a
    ///     GUID, just judging by its length. types 0x1 and 0xd are descriptive. I have no clue what subrecord types 0x7 and
    ///     0x2 may be. perhaps 0x7 is a 32-bit offset into the matching .dll, but the 64-bit number in type 0x2 doesn't
    ///     suggest anything in particular to me. type 0x8, 20 bytes long, could be some type of hash. perhaps others can fill
    ///     in the blanks.
    ///     string values, as you can see, end in 0x0 plus 0xcccc. record type 0xa is mostly string data, but preceded by an
    ///     0x1 byte, and fixed length of 0x24, so it is padded with extra 0x0s. other record types, but not all, also end in
    ///     0xcccc.
    /// </summary>
    /// <code>
    ///  #!/usr/bin/python    
    ///  import sys, os, struct    
    ///  def dump(infile):    
    ///   data = read(infile)    
    ///   filelength = len(data)    
    ///   filetype, length, data = next(data)    
    ///   assert filelength == length + 8    
    ///   print('{:08x} ({:08x}): {}'.format(filetype, length, snippet(data)))    
    ///   lengthcheck = 8    
    ///   while data:    
    ///    recordtype, recordlength, data = next(data)    
    ///    lengthcheck += 8 + recordlength    
    ///    #debug('remaining data: %s' % snippet(data))    
    ///    record, data = data[:recordlength], data[recordlength:]    
    ///    print(' {:08x} ({:08x}): {}'.format(recordtype, recordlength, snippet(record)))    
    ///    recordcheck = 0  # not 8 because record header was already not counted    
    ///    while record:    
    ///     subrecordtype, subrecordlength, record = next(record)    
    ///     recordcheck += 8 + subrecordlength    
    ///     datum, record = record[:subrecordlength], record[subrecordlength:]    
    ///     print('  {:08x}: ({:08x}) {}'.format(subrecordtype, subrecordlength, repr(datum)))    
    ///    assert recordcheck == recordlength    
    ///   assert lengthcheck == filelength    
    ///  def next(data):    
    ///   'each chunk is a type word followed by a length word'    
    ///   if not data:    
    ///    typeword, length = 0, 0    
    ///   elif len(data) > 16:    
    ///    typeword = struct.unpack('<I', data[:4])[0]    
    ///    length = struct.unpack('<I', data[4:8])[0]    
    ///   else:    
    ///    raise Exception('
    ///                                                                                    Invalid data length %
    ///                                                                                    d' % len(data))    
    ///   return typeword, length, data[8:]    
    ///  def read(filename):    
    ///   input = open(filename, '
    ///                                                                                    rb')    
    ///   data = input.read()    
    ///   input.close()    
    ///   return data        
    /// def snippet(data):
    ///  snippet = data[:12].hex()
    ///  if len(data) > 12:
    ///   snippet += '...'
    ///  if len(data) > 24:
    ///   snippet += data[-12:].hex()
    ///  return snippet
    /// def debug(message):
    ///  if __debug__:
    ///   if message:
    ///    print >>sys.stderr, message
    ///   return True
    /// if __name__ == '
    ///                                                                                    __main__':
    ///  for infile in sys.argv[1:]:
    ///   dump(infile)
    /// </code>
    public class AUXRule : BaseRule
    {
        /// <inheritdoc />
        public AUXRule(ILogger<AUXRule> logger) : base(logger) { }

        /// <inheritdoc />
        public override IMagic Magic { get; } = new Magic("05000000????????0B");

        /// <inheritdoc />
        public override Quality Quality => Quality.Medium;
        public override ITypeInfo TypeInfo { get; } = new TypeInfo("Unknown .NET resource", "AUX");

        protected override bool TryStructureInternal(BinaryReader reader, IResult result)
        {
            reader.SkipForwards(4); //already checked in magix
            uint size = reader.ReadUInt32();
            if (reader.GetLength() != size + 8)
                return false;

            return true;
        }
    }
}