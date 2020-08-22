namespace ldy985.FileMagic.Core
{
    public class FileMagicConfig
    {
        public bool PatternCheck { get; set; } = true;
        public bool StructureCheck { get; set; } = true;
        public bool ParserCheck { get; set; } = true;
        public bool ParserHandle { get; set; } = true;
    }
}