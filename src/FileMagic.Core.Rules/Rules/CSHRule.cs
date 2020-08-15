using System;
using System.Collections.Generic;
using System.Text;
using ldy985.FileMagic.Abstracts;
using Microsoft.Extensions.Logging;

namespace ldy985.FileMagic.Core.Rules.Rules
{
    public class CSHRule : BaseRule
    {
        public CSHRule(ILogger<BaseRule> logger) : base(logger) { }

        public override IMagic Magic { get; }

        public override ITypeInfo TypeInfo { get; }
    }
}