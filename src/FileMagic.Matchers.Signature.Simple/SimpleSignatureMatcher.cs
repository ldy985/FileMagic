//using System.IO;
//using ldy985.BinaryReaderExtensions;
//using ldy985.FileMagic.Abstracts;
//using Shared.Interfaces;

//namespace ldy985.FileMagic.Matchers.Signature.Simple
//{
//    public class SimpleSignatureMatcher : ISignatureMatcher
//    {
//        /// <inheritdoc />
//        public bool TestRule(BinaryReader stream, in IRule rule)
//        {
//            if (rule.Magic.Offset != 0)
//            {
//                long streamPosition = stream.GetPosition() + (long)rule.Magic.Offset;
//                if (streamPosition >= stream.GetLength())
//                    return false;

//                stream.SetPosition(streamPosition);
//            }

//            byte?[] magicBytesValue = rule.Magic.MagicBytes.Value;
//            if (stream.GetPosition() + magicBytesValue.Length > stream.GetLength())
//                return false;

//            foreach (byte? b in magicBytesValue)
//            {
//                if (!b.HasValue)
//                {
//                    stream.SkipBackwards(1);
//                    continue;
//                }

//                int readByte = stream.ReadByte();
//                if (readByte == -1 || readByte != b.Value)
//                    return false;
//            }

//            return true;
//        }
//    }
//}