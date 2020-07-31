using ldy985.FileMagic.Abstracts;
using ldy985.FileMagic.Abstracts.Enums;

namespace ldy985.FileMagic.Core
{
    public class Result : IResult
    {
        public object ParsedObject { get; set; }

        public string[] Extensions { get; set; }
        public string Description { get; set; }
        public string MatchedRuleName { get; set; }
        public MatchType MatchedRuleType { get; set; }
    }
}