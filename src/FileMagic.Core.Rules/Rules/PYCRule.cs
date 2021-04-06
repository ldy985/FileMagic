using System;
using System.IO;
using ldy985.BinaryReaderExtensions;
using ldy985.FileMagic.Abstracts;
using ldy985.NumberExtensions;
using Microsoft.Extensions.Logging;

namespace ldy985.FileMagic.Core.Rules.Rules
{
    /// <summary>
    /// https://www.python.org/dev/peps/pep-0552/
    /// https://nedbatchelder.com/blog/200804/the_structure_of_pyc_files.html
    /// https://formats.kaitai.io/python_pyc_27/index.html
    /// </summary>
    public class PYCRule : BaseRule
    {
        /// <inheritdoc />
        public override IMagic Magic { get; } = new Magic("0D0A", 2);

        public override ITypeInfo TypeInfo { get; } = new TypeInfo("Python bytecode", "PYC");

        /// <inheritdoc />
        public PYCRule(ILogger<PYCRule> logger) : base(logger)
        {
        }

        protected override bool TryStructureInternal(BinaryReader reader, IResult result)
        {
            Version version = reader.ReadEnum<Version>();
            if (!Enum.IsDefined(typeof(Version), version))
                return false;

            ushort newline = reader.ReadUInt16();
            if (newline != 0x0A0Du)
                return false;

            // TODO some undefined ObjectTypes in here e.g 0x60, 0xAD and 0x8C. Figure out what is going on. Possibly flags?
            // {
            //     reader.SkipForwards(4); // timestamp
            //
            //     ObjectType type = reader.ReadEnum<ObjectType>();
            //     if (!Enum.IsDefined(typeof(ObjectType), type))
            //         return false;
            // }


            return true;
        }

        /// <summary>
        /// https://github.com/python/cpython/blob/master/Lib/importlib/_bootstrap_external.py#L199
        /// </summary>
        private enum Version : ushort
        {
            Python1_5 = 0x4E99, //
            Python1_5_1 = 0x4E99, //
            Python1_5_2 = 0x4E99, //
            Python1_6 = 0xC4FC, //
            Python2_0 = 0xC687, //
            Python2_0_1 = 0xC687, //
            Python2_1 = 0xEB2A, //
            Python2_1_1 = 0xEB2A, //
            Python2_1_2 = 0xEB2A, //
            Python2_2 = 0xED2D, //
            Python2_3a0a = 0xF23B, //
            Python2_3a0b = 0xF245, //
            Python2_3a0c = 0xF23B, //
            Python2_4a0b = 0xF259, //
            Python2_4a3 = 0xF263, //
            Python2_4b1 = 0xF26D, //
            Python2_5a0a = 0xF277, //
            Python2_5a0b = 0xF281, // (ast-branch)
            Python2_5a0c = 0xF28B, // (with)
            Python2_5a0d = 0xF28C, // (changed WITH_CLEANUP opcode)
            Python2_5b3a = 0xF295, // (fix wrong code:for x, in ...)
            Python2_5b3b = 0xF29F, // (fix wrong code:x += yield)
            Python2_5c1 = 0xF2A9, // (fix wrong lnotab with for loops and storing constants that should have been removed)
            Python2_5c2 = 0xF2B3, // (fix wrong code:for x, in ... in listcomp/genexp)
            Python2_6a0 = 0xF2C7, // (peephole optimizations and STORE_MAP opcode)
            Python2_6a1 = 0xF2D1, // (WITH_CLEANUP optimization)
            Python2_7a0a = 0xF2DB, // (optimize list comprehensions/change LIST_APPEND)
            Python2_7a0b = 0xF2E5, // (optimize conditional branches:introduce POP_JUMP_IF_FALSE and POP_JUMP_IF_TRUE)
            Python2_7a0c = 0xF2EF, // (introduce SETUP_WITH)
            Python2_7a0d = 0xF2F9, // (introduce BUILD_SET)
            Python2_7a0e = 0xF303, // (introduce MAP_ADD and SET_ADD)
            Python3000 = 0xBB8, //
            Python3010 = 0xBC2, // (removed UNARY_CONVERT)
            Python3020 = 0xBCC, // (added BUILD_SET)
            Python3030 = 0xBD6, // (added keyword-only parameters)
            Python3040 = 0xBE0, // (added signature annotations)
            Python3050 = 0xBEA, // (print becomes a function)
            Python3060 = 0xBF4, // (PEP 3115 metaclass syntax)
            Python3061 = 0xBF5, // (string literals become unicode)
            Python3071 = 0xBFF, // (PEP 3109 raise changes)
            Python3081 = 0xC09, // (PEP 3137 make __file__ and __name__ unicode)
            Python3091 = 0xC13, // (kill str8 interning)
            Python3101 = 0xC1D, // (merge from 2.6a0, see 62151)
            Python3103 = 0xC1F, // (__file__ points to source file)
            Python3_0a4 = 0xC27, // (WITH_CLEANUP optimization).
            Python3_0b1 = 0xC3B, // (lexical exception stacking, including POP_EXCEPT                    #3021)
            Python3_1a1a = 0xC45, // (optimize list, set and dict comprehensions:change LIST_APPEND and SET_ADD, add MAP_ADD #2183)
            Python3_1a1b = 0xC4F, // (optimize conditional branches:introduce POP_JUMP_IF_FALSE and POP_JUMP_IF_TRUE                    #4715)
            Python3_2a1c = 0xC58, // (add SETUP_WITH #6101)              tag:cpython-32
            Python3_2a2 = 0xC62, // (add DUP_TOP_TWO, remove DUP_TOPX and ROT_FOUR #9225)              tag:cpython-32
            Python3_2a3 = 0xC6C, // (add DELETE_DEREF #4617)
            Python3_3a1a = 0xC76, // (__class__ super closure changed)
            Python3_3a1b = 0xC80, // (PEP 3155 __qualname__ added #13448)
            Python3_3a1c = 0xC8A, // (added size modulo 2**32 to the pyc header #13645)
            Python3_3a2 = 0xC94, // (changed PEP 380 implementation #14230)
            Python3_3a4 = 0xC9E, // (revert changes to implicit __class__ closure #14857)
            Python3_4a1a = 0xCB2, // (evaluate positional default arguments before                   keyword-only defaults #16967)
            Python3_4a1b = 0xCBC, // (add LOAD_CLASSDEREF; allow locals of class to override                   free vars #17853)
            Python3_4a1c = 0xCC6, // (various tweaks to the __class__ closure #12370)
            Python3_4a1d = 0xCD0, // (remove implicit class argument)
            Python3_4a4a = 0xCDA, // (changes to __qualname__ computation #19301)
            Python3_4a4b = 0xCE4, // (more changes to __qualname__ computation #19301)
            Python3_4rc2 = 0xCEE, // (alter __qualname__ computation #20625)
            Python3_5a1a = 0xCF8, // (PEP 465:Matrix multiplication operator #21176)
            Python3_5b1b = 0xD02, // (PEP 448:Additional Unpacking Generalizations #2292)
            Python3_5b2 = 0xD0C, // (fix dictionary display evaluation order #11205)
            Python3_5b3 = 0xD16, // (add GET_YIELD_FROM_ITER opcode #24400)
            Python3_5_2 = 0xD17, // (fix BUILD_MAP_UNPACK_WITH_CALL opcode #27286)
            Python3_6a0 = 0xD20, // (add FORMAT_VALUE opcode #25483)
            Python3_6a1 = 0xD21, // (lineno delta of code.co_lnotab becomes signed #26107)
            Python3_6a2a = 0xD2A, // (16 bit wordcode #26647)
            Python3_6a2b = 0xD2B, // (add BUILD_CONST_KEY_MAP opcode #27140)
            Python3_6a2c = 0xD2C, // (MAKE_FUNCTION simplification, remove MAKE_CLOSURE                    #27095)
            Python3_6b1a = 0xD2D, // (add BUILD_STRING opcode #27078)
            Python3_6b1b = 0xD2F, // (add SETUP_ANNOTATIONS and STORE_ANNOTATION opcodes                    #27985)
            Python3_6b1c = 0xD30, // (simplify CALL_FUNCTIONs & BUILD_MAP_UNPACK_WITH_CALL                    #27213)
            Python3_6b1d = 0xD31, // (set __class__ cell from type.__new__ #23722)
            Python3_6b2 = 0xD32, // (add BUILD_TUPLE_UNPACK_WITH_CALL #28257)
            Python3_6rc1 = 0xD33, // (more thorough __class__ validation #23722)
            Python3_7a1 = 0xD3E, // (add LOAD_METHOD and CALL_METHOD opcodes #26110)
            Python3_7a2 = 0xD3F, // (update GET_AITER #31709)
            Python3_7a4 = 0xD40, // (PEP 552:Deterministic pycs #31650)
            Python3_7b1 = 0xD41, // (remove STORE_ANNOTATION opcode #32550)
            Python3_7b5 = 0xD42, // (restored docstring as the first stmt in the body;                    this might affected the first line number #32911)
            Python3_8a1a = 0xD48, // (move frame block handling to compiler #17611)
            Python3_8a1b = 0xD49, // (add END_ASYNC_FOR #33041)
            Python3_8a1c = 0xD52, // (PEP570 Python Positional-Only Parameters #36540)
            Python3_8b2a = 0xD53, // (Reverse evaluation order of key:value in dict                    comprehensions #35224)
            Python3_8b2b = 0xD54, // (Swap the position of positional args and positional                    only args in ast.arguments #37593)
            Python3_8b4 = 0xD55, // (Fix "break" and "continue" in "finally" #37830)
            Python3_9a0a = 0xD5C, // (add LOAD_ASSERTION_ERROR #34880)
            Python3_9a0b = 0xD5D, // (simplified bytecode for with blocks #32949)
            Python3_9a0c = 0xD5E, // (remove BEGIN_FINALLY, END_FINALLY, CALL_FINALLY, POP_FINALLY bytecodes #33387)
            Python3_9a2a = 0xD5F, // (add IS_OP, CONTAINS_OP and JUMP_IF_NOT_EXC_MATCH bytecodes #39156)
            Python3_9a2b = 0xD60, // (simplify bytecodes for *value unpacking)
            Python3_9a2c = 0xD61, // (simplify bytecodes for **value unpacking)
            Python3_10a1a = 0xD66, // (Make 'annotations' future by default)
            Python3_10a1b = 0xD67, // (New line number table format -- PEP 626)
            Python3_10a2a = 0xD68, // (Function annotation for MAKE_FUNCTION is changed from dict to tuple bpo-42202)
            Python3_10a2b = 0xD69, // (RERAISE restores f_lasti if oparg != 0)
            Python3_10a6 = 0xD6A, // (PEP 634:Structural Pattern Matching)
        }


        /// <summary>
        /// https://github.com/python/cpython/blob/master/Python/marshal.c#L41
        /// </summary>
        private enum ObjectType : byte
        {
            TYPE_TUPLE = 0x28,
            TYPE_SMALL_TUPLE = 0x29,
            TYPE_ELLIPSIS = 0x2E,
            TYPE_NULL = 0x30,
            TYPE_SET = 0x3C,
            TYPE_FROZENSET = 0x3E,
            TYPE_UNKNOWN = 0x3F,
            TYPE_ASCII_INTERNED = 0x41,
            TYPE_FALSE = 0x46,
            TYPE_INT64 = 0x49,
            TYPE_NONE = 0x4E,
            TYPE_STOPITER = 0x53,
            TYPE_TRUE = 0x54,
            TYPE_SHORT_ASCII_INTERNED = 0x5A,
            TYPE_LIST = 0x5B,
            TYPE_ASCII = 0x61,
            TYPE_CODE = 0x63,
            TYPE_FLOAT = 0x66,
            TYPE_BINARY_FLOAT = 0x67,
            TYPE_INT = 0x69,
            TYPE_LONG = 0x6C,
            TYPE_REF = 0x72,
            TYPE_STRING = 0x73,
            TYPE_INTERNED = 0x74,
            TYPE_UNICODE = 0x75,
            TYPE_COMPLEX = 0x78,
            TYPE_BINARY_COMPLEX = 0x79,
            TYPE_SHORT_ASCII = 0x7A,
            TYPE_DICT = 0x7B,
        }
    }
}