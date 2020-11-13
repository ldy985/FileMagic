using ldy985.FileMagic.Abstracts.Enums;

namespace ldy985.FileMagic.Abstracts
{
    public interface IResult
    {
        object ParsedObject { get; set; }
        string[] Extensions { get; set; }
        string Description { get; set; }
        string MatchedRuleName { get; set; }
        MatchTypes MatchedRuleTypes { get; set; }
    }
}