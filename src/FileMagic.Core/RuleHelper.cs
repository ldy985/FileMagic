using ldy985.FileMagic.Abstracts;

namespace ldy985.FileMagic.Core
{
    public static class RuleHelper
    {
        public static void AddData(in IResult result, IRule rule)
        {
            result.MatchedRule = rule;
            result.Extensions ??= rule.TypeInfo.Extensions;
            result.Description ??= rule.TypeInfo.Description;
        }
    }
}