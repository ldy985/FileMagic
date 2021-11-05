using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.IO;
using ldy985.BinaryReaderExtensions;
using ldy985.FileMagic.Abstracts;
using Microsoft.Extensions.Logging;

namespace ldy985.FileMagic.Matchers.Signature.Trie
{
    /// <summary>
    ///     An implementation of <see cref="IParallelMagicMatcher" /> using a trie as lookup structure.
    ///     It allows to match multiple rules quickly by only going though the stream once.
    /// </summary>
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

        private Node<IRule> RootNode { get; }

        /// <inheritdoc />
        public bool TryFind(BinaryReader br, in IMetaData metaData, [NotNullWhen(true)] out IEnumerable<IRule>? matchedRules)
        {
            return TryFind(br, out matchedRules);
        }

        /// <inheritdoc />
        public bool TryFind(BinaryReader br, [NotNullWhen(true)] out IEnumerable<IRule>? matchedRules)
        {
            long streamPosition = br.GetPosition();
            bool tryFindInternal = TryFindInternal(RootNode, br, streamPosition, out var data);
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
        }

        private void RegisterPattern(IRule rule)
        {
            if (rule.Magic == null)
                throw new ArgumentException("Rule must have a magic");

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

        /// <summary>
        ///     TryFindInternal
        /// </summary>
        /// <param name="node"></param>
        /// <param name="br"></param>
        /// <param name="pos"></param>
        /// <param name="result"></param>
        /// <returns></returns>
        /// <exception cref="IOException"></exception>
        /// <exception cref="ObjectDisposedException"></exception>
        private static bool TryFindInternal(Node<IRule> node, BinaryReader br, long pos, [NotNullWhen(true)] out Node<IRule>? result)
        {
            result = null;

            if (pos >= br.GetLength())
                return false;

            bool tryFind1 = false;
            bool tryFind2 = false;

            ushort readByte = br.ReadByte();
            pos++;

            if (node.Children != null)
            {
                if (node.Children.TryGetValue(readByte, out var tempNode))
                {
                    tryFind1 = TryFindInternal(tempNode, br, pos, out var temp1);
                    if (tryFind1)
                        result = temp1;
                }

                br.SetPosition(pos);

                if (node.Children.TryGetValue(ushort.MaxValue, out var tempNode2))
                {
                    tryFind2 = TryFindInternal(tempNode2, br, pos, out var temp2);
                    if (tryFind2)
                        result = temp2;
                }
            }

            if (tryFind1 || tryFind2)
#pragma warning disable 8762
                return true;
#pragma warning restore 8762

            if (node.Values == null)
                return false;

            result = node;
            return true;
        }

        private void AddRule(IReadOnlyList<byte?> path, IRule leafData)
        {
            var node = RootNode;

            for (int index = 0; index < path.Count; index++)
            {
                byte? b = path[index];
                ushort key = b.HasValue ? (ushort)b : ushort.MaxValue;

                if (node.Children != null && node.Children.TryGetValue(key, out var tempNode))
                {
                    if (index == path.Count - 1)
                        tempNode.AddValue(leafData);

                    node = tempNode;
                    continue;
                }

                var value = new Node<IRule>();

                if (index == path.Count - 1)
                    value.AddValue(leafData);

                value.Parent = node;
                node.AddChild(key, value);
                node = value;
            }
        }
    }
}