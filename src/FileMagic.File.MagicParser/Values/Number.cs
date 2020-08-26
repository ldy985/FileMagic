using System;
using Chronos.Libraries.FileClassifier.Enums;

namespace Chronos.Libraries.FileClassifier.Values
{
    public class Number : Value
    {
        public Number(dynamic v)
        {
            Value = BitConverter.GetBytes(v);
        }

        /// <summary>
        /// Gets or sets the value bytes.
        /// </summary>
        private byte[] Value { get; set; }

        /// <inheritdoc/>
        public override byte GetStartingByte()
        {
            return Value[0];
        }

        /// <inheritdoc/>
        public override dynamic GetValue(ValueTypes type, bool unsigned)
        {
            dynamic output = null;
            if (unsigned)
            {
                switch (type)
                {
                    case ValueTypes.BYTE:
                        output = Value[0];
                        break;

                    case ValueTypes.BESHORT:
                    case ValueTypes.SHORT:
                    case ValueTypes.LESHORT:
                        output = BitConverter.ToUInt16(Value, 0);
                        break;

                    case ValueTypes.BELONG:
                    case ValueTypes.LONG:
                    case ValueTypes.LELONG:
                        output = BitConverter.ToUInt32(Value, 0);
                        break;

                    case ValueTypes.BEDATE:
                    case ValueTypes.BELDATE:
                    case ValueTypes.BEQUAD:
                    case ValueTypes.DATE:
                    case ValueTypes.LEDATE:
                    case ValueTypes.QDATE:
                    case ValueTypes.QLDATE:
                    case ValueTypes.QWDATE:
                    case ValueTypes.LELDATE:
                    case ValueTypes.LEQUAD:
                    case ValueTypes.QUAD:
                        output = BitConverter.ToUInt64(Value, 0);
                        break;
                }
            }
            else
            {
                switch (type)
                {
                    case ValueTypes.BYTE:
                        output = Value[0];
                        break;

                    case ValueTypes.BESHORT:
                    case ValueTypes.SHORT:
                    case ValueTypes.LESHORT:
                        output = BitConverter.ToInt16(Value, 0);
                        break;

                    case ValueTypes.BELONG:
                    case ValueTypes.LONG:
                    case ValueTypes.LELONG:
                        output = BitConverter.ToInt32(Value, 0);
                        break;

                    case ValueTypes.BEDATE:
                    case ValueTypes.BELDATE:
                    case ValueTypes.BEQUAD:
                    case ValueTypes.DATE:
                    case ValueTypes.LEDATE:
                    case ValueTypes.QDATE:
                    case ValueTypes.QLDATE:
                    case ValueTypes.QWDATE:
                    case ValueTypes.LELDATE:
                    case ValueTypes.LEQUAD:
                    case ValueTypes.QUAD:
                        output = BitConverter.ToInt64(Value, 0);
                        break;

                    case ValueTypes.FLOAT:
                        output = BitConverter.ToSingle(Value, 0);
                        break;

                    case ValueTypes.DOUBLE:
                        output = BitConverter.ToDouble(Value, 0);
                        break;
                }
            }

            return output;
        }

        public override byte[] GetBytes()
        {
            return Value;
        }

        public override string ToString()
        {
            return BitConverter.ToInt64(Value, 0).ToString();
        }
    }
}