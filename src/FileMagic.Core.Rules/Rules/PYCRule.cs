using System;
using System.IO;
using ldy985.BinaryReaderExtensions;
using ldy985.FileMagic.Abstracts;
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
            if (newline != 0x0D0Au)
                return false;

            reader.SkipForwards(4); // timestamp

            ObjectType type = reader.ReadEnum<ObjectType>();
            if (!Enum.IsDefined(typeof(ObjectType), type))
                return false;

            return true;
        }

        /// <summary>
        /// https://github.com/python/cpython/blob/master/Lib/importlib/_bootstrap_external.py#L199
        /// </summary>
        private enum Version
        {
            Python1_5 = 20121, //
            Python1_5_1 = 20121, //
            Python1_5_2 = 20121, //
            Python1_6 = 50428, //
            Python2_0 = 50823, //
            Python2_0_1 = 50823, //
            Python2_1 = 60202, //
            Python2_1_1 = 60202, //
            Python2_1_2 = 60202, //
            Python2_2 = 60717, //
            Python2_3a0a = 62011, //
            Python2_3a0b = 62021, //
            Python2_3a0c = 62011, //
            Python2_4a0b = 62041, //
            Python2_4a3 = 62051, //
            Python2_4b1 = 62061, //
            Python2_5a0a = 62071, //
            Python2_5a0b = 62081, // (ast-branch)
            Python2_5a0c = 62091, // (with)
            Python2_5a0d = 62092, // (changed WITH_CLEANUP opcode)
            Python2_5b3a = 62101, // (fix wrong code:for x, in ...)
            Python2_5b3b = 62111, // (fix wrong code:x += yield)
            Python2_5c1 = 62121, // (fix wrong lnotab with for loops and storing constants that should have been removed)
            Python2_5c2 = 62131, // (fix wrong code:for x, in ... in listcomp/genexp)
            Python2_6a0 = 62151, // (peephole optimizations and STORE_MAP opcode)
            Python2_6a1 = 62161, // (WITH_CLEANUP optimization)
            Python2_7a0a = 62171, // (optimize list comprehensions/change LIST_APPEND)
            Python2_7a0b = 62181, // (optimize conditional branches:introduce POP_JUMP_IF_FALSE and POP_JUMP_IF_TRUE)
            Python2_7a0c = 62191, // (introduce SETUP_WITH)
            Python2_7a0d = 62201, // (introduce BUILD_SET)
            Python2_7a0e = 62211, // (introduce MAP_ADD and SET_ADD)
            Python3000 = 3000, //
            Python3010 = 3010, // (removed UNARY_CONVERT)
            Python3020 = 3020, // (added BUILD_SET)
            Python3030 = 3030, // (added keyword-only parameters)
            Python3040 = 3040, // (added signature annotations)
            Python3050 = 3050, // (print becomes a function)
            Python3060 = 3060, // (PEP 3115 metaclass syntax)
            Python3061 = 3061, // (string literals become unicode)
            Python3071 = 3071, // (PEP 3109 raise changes)
            Python3081 = 3081, // (PEP 3137 make __file__ and __name__ unicode)
            Python3091 = 3091, // (kill str8 interning)
            Python3101 = 3101, // (merge from 2.6a0, see 62151)
            Python3103 = 3103, // (__file__ points to source file)
            Python3_0a4 = 3111, // (WITH_CLEANUP optimization).
            Python3_0b1 = 3131, // (lexical exception stacking, including POP_EXCEPT                    #3021)
            Python3_1a1a = 3141, // (optimize list, set and dict comprehensions:change LIST_APPEND and SET_ADD, add MAP_ADD #2183)
            Python3_1a1b = 3151, // (optimize conditional branches:introduce POP_JUMP_IF_FALSE and POP_JUMP_IF_TRUE                    #4715)
            Python3_2a1c = 3160, // (add SETUP_WITH #6101)              tag:cpython-32
            Python3_2a2 = 3170, // (add DUP_TOP_TWO, remove DUP_TOPX and ROT_FOUR #9225)              tag:cpython-32
            Python3_2a3 = 3180, // (add DELETE_DEREF #4617)
            Python3_3a1a = 3190, // (__class__ super closure changed)
            Python3_3a1b = 3200, // (PEP 3155 __qualname__ added #13448)
            Python3_3a1c = 3210, // (added size modulo 2**32 to the pyc header #13645)
            Python3_3a2 = 3220, // (changed PEP 380 implementation #14230)
            Python3_3a4 = 3230, // (revert changes to implicit __class__ closure #14857)
            Python3_4a1a = 3250, // (evaluate positional default arguments before                   keyword-only defaults #16967)
            Python3_4a1b = 3260, // (add LOAD_CLASSDEREF; allow locals of class to override                   free vars #17853)
            Python3_4a1c = 3270, // (various tweaks to the __class__ closure #12370)
            Python3_4a1d = 3280, // (remove implicit class argument)
            Python3_4a4a = 3290, // (changes to __qualname__ computation #19301)
            Python3_4a4b = 3300, // (more changes to __qualname__ computation #19301)
            Python3_4rc2 = 3310, // (alter __qualname__ computation #20625)
            Python3_5a1a = 3320, // (PEP 465:Matrix multiplication operator #21176)
            Python3_5b1b = 3330, // (PEP 448:Additional Unpacking Generalizations #2292)
            Python3_5b2 = 3340, // (fix dictionary display evaluation order #11205)
            Python3_5b3 = 3350, // (add GET_YIELD_FROM_ITER opcode #24400)
            Python3_5_2 = 3351, // (fix BUILD_MAP_UNPACK_WITH_CALL opcode #27286)
            Python3_6a0 = 3360, // (add FORMAT_VALUE opcode #25483)
            Python3_6a1 = 3361, // (lineno delta of code.co_lnotab becomes signed #26107)
            Python3_6a2a = 3370, // (16 bit wordcode #26647)
            Python3_6a2b = 3371, // (add BUILD_CONST_KEY_MAP opcode #27140)
            Python3_6a2c = 3372, // (MAKE_FUNCTION simplification, remove MAKE_CLOSURE                    #27095)
            Python3_6b1a = 3373, // (add BUILD_STRING opcode #27078)
            Python3_6b1b = 3375, // (add SETUP_ANNOTATIONS and STORE_ANNOTATION opcodes                    #27985)
            Python3_6b1c = 3376, // (simplify CALL_FUNCTIONs & BUILD_MAP_UNPACK_WITH_CALL                    #27213)
            Python3_6b1d = 3377, // (set __class__ cell from type.__new__ #23722)
            Python3_6b2 = 3378, // (add BUILD_TUPLE_UNPACK_WITH_CALL #28257)
            Python3_6rc1 = 3379, // (more thorough __class__ validation #23722)
            Python3_7a1 = 3390, // (add LOAD_METHOD and CALL_METHOD opcodes #26110)
            Python3_7a2 = 3391, // (update GET_AITER #31709)
            Python3_7a4 = 3392, // (PEP 552:Deterministic pycs #31650)
            Python3_7b1 = 3393, // (remove STORE_ANNOTATION opcode #32550)
            Python3_7b5 = 3394, // (restored docstring as the first stmt in the body;                    this might affected the first line number #32911)
            Python3_8a1a = 3400, // (move frame block handling to compiler #17611)
            Python3_8a1b = 3401, // (add END_ASYNC_FOR #33041)
            Python3_8a1c = 3410, // (PEP570 Python Positional-Only Parameters #36540)
            Python3_8b2a = 3411, // (Reverse evaluation order of key:value in dict                    comprehensions #35224)
            Python3_8b2b = 3412, // (Swap the position of positional args and positional                    only args in ast.arguments #37593)
            Python3_8b4 = 3413, // (Fix "break" and "continue" in "finally" #37830)
            Python3_9a0a = 3420, // (add LOAD_ASSERTION_ERROR #34880)
            Python3_9a0b = 3421, // (simplified bytecode for with blocks #32949)
            Python3_9a0c = 3422, // (remove BEGIN_FINALLY, END_FINALLY, CALL_FINALLY, POP_FINALLY bytecodes #33387)
            Python3_9a2a = 3423, // (add IS_OP, CONTAINS_OP and JUMP_IF_NOT_EXC_MATCH bytecodes #39156)
            Python3_9a2b = 3424, // (simplify bytecodes for *value unpacking)
            Python3_9a2c = 3425, // (simplify bytecodes for **value unpacking)
            Python3_10a1a = 3430, // (Make 'annotations' future by default)
            Python3_10a1b = 3431, // (New line number table format -- PEP 626)
            Python3_10a2a = 3432, // (Function annotation for MAKE_FUNCTION is changed from dict to tuple bpo-42202)
            Python3_10a2b = 3433, // (RERAISE restores f_lasti if oparg != 0)
            Python3_10a6 = 3434, // (PEP 634:Structural Pattern Matching)
        }


        private enum ObjectType
        {
            Tuple = 0x28,
            PyFalse = 0x46,
            None = 0x4E,
            StringRef = 0x52,
            PyTrue = 0x54,
            CodeObject = 0x63,
            Int = 0x69,
            String = 0x73,
            Interned = 0x74,
            UnicodeString = 0x75,
        }
    }
}