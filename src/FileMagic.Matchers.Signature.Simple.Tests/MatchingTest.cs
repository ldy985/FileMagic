using System;
using System.IO;
using ldy985.FileMagic.Abstracts;
using Shared;
using Shared.Interfaces;
using Xunit;

namespace ldy985.FileMagic.Matchers.Signature.Simple.Tests
{
    public class MatchingTest
    {
        private readonly SimpleSignatureMatcher _simpleSignatureMatcher;

        public MatchingTest()
        {
            _simpleSignatureMatcher = new SimpleSignatureMatcher();
        }

        [Fact]
        public void MatchCase()
        {
            using (MemoryStream ms = new MemoryStream(new byte[] { 0x0, 0x1, 0x2 }))
            using (BinaryReader br = new BinaryReader(ms))
                Assert.True(_simpleSignatureMatcher.TestRule(br, new TestRule()));
        }

        [Fact]
        public void NoMatchCase()
        {
            using (MemoryStream ms = new MemoryStream(new byte[] { 0x0, 0x1, 0x3 }))
            using (BinaryReader br = new BinaryReader(ms))
                Assert.False(_simpleSignatureMatcher.TestRule(br, new TestRule()));
        }

        [Fact]
        public void ShortSteamCase()
        {
            using (MemoryStream ms = new MemoryStream(new byte[] { 0x0 }))
            using (BinaryReader br = new BinaryReader(ms))
                Assert.False(_simpleSignatureMatcher.TestRule(br, new TestRule()));
        }

        [Fact]
        public void LongSteamCase()
        {
            using (MemoryStream ms = new MemoryStream(new byte[] { 0x0, 0x0, 0x0, 0x0, 0x0 }))
            using (BinaryReader br = new BinaryReader(ms))
                Assert.False(_simpleSignatureMatcher.TestRule(br, new TestRule()));
        }

        [Fact]
        public void LongRuleNoMatchCase()
        {
            using (MemoryStream ms = new MemoryStream(new byte[] { 0x0 }))
            using (BinaryReader br = new BinaryReader(ms))
                Assert.False(_simpleSignatureMatcher.TestRule(br, new TestRule2()));
        }

        [Fact]
        public void LongRuleMatchCase()
        {
            using (MemoryStream ms = new MemoryStream(new byte[] { 0x0, 0x0, 0x0, 0x0, 0x0, 0x1, 0x2 }))
            using (BinaryReader br = new BinaryReader(ms))
                Assert.True(_simpleSignatureMatcher.TestRule(br, new TestRule2()));
        }

        [Fact]
        public void WildcardRuleCase()
        {
            using (MemoryStream ms = new MemoryStream(new byte[] { 0x0, 1, 2, 7 }))
            using (BinaryReader br = new BinaryReader(ms))
                Assert.True(_simpleSignatureMatcher.TestRule(br, new TestRule3()));
        }
    }

    [System.Diagnostics.CodeAnalysis.SuppressMessage("General", "RCS1079:Throwing of new NotImplementedException.", Justification = "<Pending>")]
    internal class TestRule : IRule
    {
        public bool HasMagic { get; }
        public IMagic Magic { get; } = new Magic("000102");
        public ITypeInfo TypeInfo { get; }
        public bool HasParser { get; }

        public bool TryParse(BinaryReader reader, IResult result)
        {
            throw new NotImplementedException();
        }

        public bool HasStructure { get; }

        public bool TryStructure(BinaryReader reader, IResult result)
        {
            throw new NotImplementedException();
        }

        /// <inheritdoc />
        public bool TryMagic(BinaryReader stream)
        {
            throw new NotImplementedException();
        }
    }

    [System.Diagnostics.CodeAnalysis.SuppressMessage("General", "RCS1079:Throwing of new NotImplementedException.", Justification = "<Pending>")]
    internal class TestRule2 : IRule
    {
        public bool HasMagic { get; }
        public IMagic Magic { get; } = new Magic("000102", 4);
        public ITypeInfo TypeInfo { get; }
        public bool HasParser { get; }

        public bool TryParse(BinaryReader reader, IResult result)
        {
            throw new NotImplementedException();
        }

        public bool HasStructure { get; }

        public bool TryStructure(BinaryReader reader, IResult result)
        {
            throw new NotImplementedException();
        }

        /// <inheritdoc />
        public bool TryMagic(BinaryReader stream)
        {
            throw new NotImplementedException();
        }
    }

    [System.Diagnostics.CodeAnalysis.SuppressMessage("General", "RCS1079:Throwing of new NotImplementedException.", Justification = "<Pending>")]
    internal class TestRule3 : IRule
    {
        public bool HasMagic { get; }
        public IMagic Magic { get; } = new Magic("000102??");
        public ITypeInfo TypeInfo { get; }
        public bool HasParser { get; }

        public bool TryParse(BinaryReader reader, IResult result)
        {
            throw new NotImplementedException();
        }

        public bool HasStructure { get; }

        public bool TryStructure(BinaryReader reader, IResult result)
        {
            throw new NotImplementedException();
        }

        /// <inheritdoc />
        public bool TryMagic(BinaryReader stream)
        {
            throw new NotImplementedException();
        }
    }
}