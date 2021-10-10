using System;
using System.IO;
using ldy985.FileMagic.Abstracts;

namespace ldy985.FileMagic.Core.Extensions
{
    public static class RuleExtensions
    {
        public static bool TryMagic(this IRule rule, string filePath)
        {
            if (!rule.HasMagic)
                throw new ArgumentException("Rule must have a magic");

            using (FileStream? stream = File.OpenRead(filePath))
                return rule.TryMagic(stream);
        }
    }
}