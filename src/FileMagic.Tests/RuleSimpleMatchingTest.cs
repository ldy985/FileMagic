using System;
using System.IO;
using ldy985.FileMagic.Abstracts;
using ldy985.FileMagic.Core;
using ldy985.FileMagic.Matchers.Signature.Trie;
using ldy985.FileMagic.Tests.TestRules;
using Microsoft.Extensions.Logging.Abstractions;
using Xunit;

namespace ldy985.FileMagic.Tests
{
    public class RuleSimpleMatchingTest : IDisposable
    {
        public RuleSimpleMatchingTest()
        {
            IRule[] rules =
            {
                new TestRule(NullLogger<BaseRule>.Instance),
                new TestRule2(NullLogger<BaseRule>.Instance),
                new TestRule3(NullLogger<BaseRule>.Instance)
            };
            FileMagicConfig fileMagicConfig = new FileMagicConfig();
            fileMagicConfig.ParserCheck = false;
            fileMagicConfig.StructureCheck = false;
            fileMagicConfig.ParserHandle = false;
            fileMagicConfig.PatternCheck = true;
            RuleProvider ruleProvider = new RuleProvider(rules);
            _simpleSignatureMatcher = new FileMagic( Microsoft.Extensions.Options.Options.Create(fileMagicConfig),NullLogger<FileMagic>.Instance, ruleProvider, new TrieSignatureMatcher(NullLogger<TrieSignatureMatcher>.Instance, ruleProvider));
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        private readonly FileMagic _simpleSignatureMatcher;

        protected virtual void Dispose(bool disposing)
        {
            if (disposing)
                _simpleSignatureMatcher.Dispose();
        }

        ~RuleSimpleMatchingTest()
        {
            Dispose(false);
        }

        [Fact]
        public void LongRuleMatchCase()
        {
            using (MemoryStream ms = new MemoryStream(new byte[] { 0x0, 0x0, 0x0, 0x0, 0x0, 0x1, 0x2 }))
                Assert.True(_simpleSignatureMatcher.StreamMatches<TestRule2>(ms, out _));
        }

        [Fact]
        public void LongRuleNoMatchCase()
        {
            using (MemoryStream ms = new MemoryStream(new byte[] { 0x0 }))
                Assert.False(_simpleSignatureMatcher.StreamMatches<TestRule2>(ms, out _));
        }

        [Fact]
        public void LongSteamCase()
        {
            using (MemoryStream ms = new MemoryStream(new byte[] { 0x0, 0x0, 0x0, 0x0, 0x0 }))
                Assert.False(_simpleSignatureMatcher.StreamMatches<TestRule>(ms, out _));
        }

        [Fact]
        public void MatchCase()
        {
            using (MemoryStream ms = new MemoryStream(new byte[] { 0x0, 0x1, 0x2 }))
                Assert.True(_simpleSignatureMatcher.StreamMatches<TestRule>(ms, out _));
        }

        [Fact]
        public void NoMatchCase()
        {
            using (MemoryStream ms = new MemoryStream(new byte[] { 0x0, 0x1, 0x3 }))
                Assert.False(_simpleSignatureMatcher.StreamMatches<TestRule>(ms, out _));
        }

        [Fact]
        public void ShortSteamCase()
        {
            using (MemoryStream ms = new MemoryStream(new byte[] { 0x0 }))
                Assert.False(_simpleSignatureMatcher.StreamMatches<TestRule>(ms, out _));
        }

        [Fact]
        public void WildcardRuleCase()
        {
            using (MemoryStream ms = new MemoryStream(new byte[] { 0x0, 1, 2, 7 }))
                Assert.True(_simpleSignatureMatcher.StreamMatches<TestRule3>(ms, out _));
        }
    }
}