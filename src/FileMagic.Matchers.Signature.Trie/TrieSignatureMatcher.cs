using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.IO;
using ldy985.BinaryReaderExtensions;
using ldy985.FileMagic.Abstracts;
using Microsoft.Extensions.Logging;

namespace ldy985.FileMagic.Matchers.Signature.Trie
{
    public class TrieSignatureMatcher : IParallelMagicMatcher
    {
        private readonly ILogger<TrieSignatureMatcher> _logger;

        public TrieSignatureMatcher(ILogger<TrieSignatureMatcher> logger, IRuleProvider ruleProvider)
        {
            _logger = logger;

            RootNode = new Node<IRule>();
            RootNode.Children = new Dictionary<ushort, Node<IRule>>();

            foreach (IRule rule in ruleProvider.PatternRules)
            {
                logger.LogTrace("Registered: {RuleName}", rule.GetType().Name);
                RegisterPattern(rule);
            }
        }

        private void RegisterPattern(IRule rule)
        {
            ulong offset = rule.Magic.Offset;
            string bytes = rule.Magic.Pattern;

            byte?[] data = new byte?[offset + (ulong)bytes.Length / 2];
            for (int i = 0; i < bytes.Length; i += 2)
            {
                string substring = bytes.Substring(i, 2);
                if (substring == "??")
                    data[offset] = null;
                else
                    data[offset] = Convert.ToByte(substring, 16);

                offset++;
            }

            AddRule(data, rule);
        }

        private Node<IRule> RootNode { get; }

        /// <inheritdoc />
#if NETSTANDARD2_1
        public bool TryFind(BinaryReader br, in IMetaData metaData, [NotNullWhen(true)]out IEnumerable<IRule>? matchedRules)
#else
        public bool TryFind(BinaryReader br, in IMetaData metaData, out IEnumerable<IRule>? matchedRules)
#endif
        {
            return TryFind(br, out matchedRules);
        }

#if NETSTANDARD2_1
        public bool TryFind(BinaryReader br, [NotNullWhen(true)]out IEnumerable<IRule>? matchedRules)
#else
        public bool TryFind(BinaryReader br, out IEnumerable<IRule>? matchedRules)
#endif
        {
            long streamPosition = br.GetPosition();
            bool tryFindInternal = TryFindInternal(RootNode, br, out Node<IRule>? data);
            br.SetPosition(streamPosition);
            if (!tryFindInternal)
            {
                _logger.LogTrace("Trie found no leafs");
                matchedRules = null;
                return false;
            }

            _logger.LogTrace("Trie found at least 1 leaf");

            matchedRules = data!.GetAllLeafs();
            return true;

            //foreach (IRule matchedRule in data.GetAllLeafs())
            //{
            //    //string name = matchedRule.GetType().Name;
            //    //_logger.LogTrace("Matched " + name + " using pattern");
            //    //result.MatchedRuleType = MatchType.Binary;

            //    //if (matchedRule.HasStructure || matchedRule.HasParser)
            //    //{
            //    //    if (RuleHelper.TryRule(matchedRule, br, in result, out MatchType matchType))
            //    //    {
            //    //        if ((matchType & MatchType.Parser) != 0 && (matchType & MatchType.Structure) != 0)
            //    //            _logger.LogTrace("Matched " + name + " using parser and structure");
            //    //        else if ((matchType & MatchType.Parser) != 0)
            //    //            _logger.LogTrace("Matched " + name + " using parser");
            //    //        else if ((matchType & MatchType.Structure) != 0)
            //    //            _logger.LogTrace("Matched " + name + " using structure");

            //    //result.MatchedRuleType |= matchType;

            //    RuleHelper.AddData(result, matchedRule);

            //    //        return true;
            //    //    }
            //    //}
            //    //else
            //    //{
            //    //    RuleHelper.AddData(result, matchedRule);

            //    return true;

            //    //}
            //}

            //return false;
        }

        /// <summary>
        /// TryFindInternal
        /// </summary>
        /// <param name="node"></param>
        /// <param name="br"></param>
        /// <param name="result"></param>
        /// <returns></returns>
        /// <exception cref="IOException"></exception>
        /// <exception cref="ObjectDisposedException"></exception>
#if NETSTANDARD2_1
        private bool TryFindInternal(Node<IRule> node, BinaryReader br, [NotNullWhen(true)]out Node<IRule>? result)
#else
        private bool TryFindInternal(Node<IRule> node, BinaryReader br, out Node<IRule>? result)
#endif

        {
            result = null;

            if (br.GetPosition() >= br.GetLength())
                return false;

            bool tryFind1 = false;
            bool tryFind2 = false;

            ushort readByte = br.ReadByte();
            _logger.LogTrace("Read {Byte} from stream", readByte);
            long streamPosition = br.GetPosition();

            if (node.Children != null)
            {
                if (node.Children.TryGetValue(readByte, out Node<IRule> tempNode))
                {
                    tryFind1 = TryFindInternal(tempNode, br, out Node<IRule>? temp1);
                    if (tryFind1)
                        result = temp1;
                }

                br.SetPosition(streamPosition);

                if (node.Children.TryGetValue(ushort.MaxValue, out Node<IRule>? tempNode2))
                {
                    tryFind2 = TryFindInternal(tempNode2, br, out Node<IRule>? temp2);
                    if (tryFind2)
                        result = temp2;
                }
            }

            if (tryFind1 || tryFind2)
                return true;

            if (node.Values == null)
                return false;

            result = node;
            return true;
        }

        private void AddRule(byte?[] path, IRule leafData)
        {
            Node<IRule> node = RootNode;

            for (int index = 0; index < path.Length; index++)
            {
                byte? b = path[index];
                ushort key = b.HasValue ? (ushort)b : ushort.MaxValue;

                if (node.Children != null && node.Children.TryGetValue(key, out Node<IRule> tempNode))
                {
                    if (index == path.Length - 1)
                        tempNode.AddValue(leafData);

                    node = tempNode;
                    continue;
                }

                Node<IRule> value = new Node<IRule>();

                if (index == path.Length - 1)
                    value.AddValue(leafData);

                value.Parent = node;
                node.AddChild(key, value);
                node = value;
            }
        }
    }
}