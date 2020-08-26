using System;

namespace Chronos.Libraries.FileClassifier.MagicRule
{
    [Flags]
    public enum Flags
    {
        INDIRECT_RELATIVE,

        STRING_COMPACT_WHITESPACE,

        STRING_COMPACT_OPTIONAL_WHITESPACE,

        STRING_IGNORE_LOWERCASE,

        STRING_IGNORE_UPPERCASE,

        REGEX_OFFSET_START,

        STRING_BINTEST,

        STRING_TEXTTEST,

        STRING_TRIM,

        PSTRING_LENGTH_INCLUDES_ITSELF
    }
}