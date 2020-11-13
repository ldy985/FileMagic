using System;
using ldy985.FileMagic.File.MagicParser.Enums;

namespace ldy985.FileMagic.File.MagicParser.Helpers
{
    public static class TypeHelper
    {
        public static bool IS_DOUBLE(ValueTypes dataTypeType)
        {
            return dataTypeType == ValueTypes.DOUBLE ||
                   dataTypeType == ValueTypes.BEDOUBLE ||
                   dataTypeType == ValueTypes.LEDOUBLE ||
                   dataTypeType == ValueTypes.FLOAT ||
                   dataTypeType == ValueTypes.LEFLOAT ||
                   dataTypeType == ValueTypes.BEFLOAT;
        }

        public static bool IS_STRING_OR_SPECIAl(ValueTypes typesToEnum)
        {
            return typesToEnum == ValueTypes.STRING ||
                   typesToEnum == ValueTypes.PSTRING ||
                   typesToEnum == ValueTypes.BESTRING16 ||
                   typesToEnum == ValueTypes.LESTRING16 ||
                   typesToEnum == ValueTypes.REGEX ||
                   typesToEnum == ValueTypes.SEARCH ||
                   typesToEnum == ValueTypes.INDIRECT ||
                   typesToEnum == ValueTypes.NAME ||
                   typesToEnum == ValueTypes.USE ||
                   typesToEnum == ValueTypes.DER;
        }

        public static ValueTypes TypeToEnum(string input)
        {
            if (Enum.TryParse<ValueTypes>(input, true, out var value))
            {
                return value;
            }

            return ValueTypes.INVALID;
        }
    }
}