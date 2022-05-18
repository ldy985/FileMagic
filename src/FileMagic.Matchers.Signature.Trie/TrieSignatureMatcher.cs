using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.IO;
using ldy985.BinaryReaderExtensions;
using ldy985.FileMagic.Abstracts;
using ldy985.FileMagic.Matchers.Signature.Trie.Logging;
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
                TrieSignatureMatcherLogger.LogRuleRegistration(logger, rule.GetType().Name);
                RegisterPattern(rule);
            }
        }

        private Node<IRule> RootNode { get; }

        /// <inheritdoc />
        /// <exception cref="T:System.IO.IOException">An I/O error occurs.</exception>
        /// <exception cref="T:System.ObjectDisposedException">Condition.</exception>
        public bool TryFind(BinaryReader br, in IMetaData metaData, [NotNullWhen(true)] out IEnumerable<IRule>? matchedRules)
        {
            return TryFind(br, out matchedRules);
        }

        /// <inheritdoc />
        /// <exception cref="T:System.ObjectDisposedException">Condition.</exception>
        /// <exception cref="T:System.IO.IOException">Condition.</exception>
        public bool TryFind(BinaryReader br, [NotNullWhen(true)] out IEnumerable<IRule>? matchedRules)
        {
            long streamPosition = br.GetPosition();
            bool tryFindInternal = TryFindInternal(RootNode, br, streamPosition, out Node<IRule>? data);
            br.SetPosition(streamPosition);

            if (!tryFindInternal)
            {
                TrieSignatureMatcherLogger.LogNoLeafs(_logger);
                matchedRules = null;
                return false;
            }

            TrieSignatureMatcherLogger.LogFoundLeaf(_logger);

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
        private static bool TryFindInternal(Node<IRule> node, BinaryReader br, long pos, [NotNullWhen(true)] out Node<IRule>? result)
        {
            result = null;

            if (pos >= br.GetLength())
                return false;

            bool tryFind1 = false;
            bool tryFind2 = false;
            ushort readByte;

            try
            {
                readByte = br.ReadByte();
            }
            catch (EndOfStreamException)
            {
                // End of stream is fine, we just early exit. This is hit in rare cases when the stream is created from a device or similar.
                return false;
            }

            pos++;

            if (node.Children != null)
            {
                if (node.Children.TryGetValue(readByte, out Node<IRule>? tempNode))
                {
                    tryFind1 = TryFindInternal(tempNode, br, pos, out Node<IRule>? temp1);
                    if (tryFind1)
                        result = temp1;
                }

                br.SetPosition(pos);

                if (node.Children.TryGetValue(ushort.MaxValue, out Node<IRule>? tempNode2))
                {
                    tryFind2 = TryFindInternal(tempNode2, br, pos, out Node<IRule>? temp2);
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
            Node<IRule> node = RootNode;

            for (int index = 0; index < path.Count; index++)
            {
                byte? b = path[index];
                ushort key = b.HasValue ? (ushort)b : ushort.MaxValue;

                if (node.Children != null && node.Children.TryGetValue(key, out Node<IRule>? tempNode))
                {
                    if (index == path.Count - 1)
                        tempNode.AddValue(leafData);

                    node = tempNode;
                    continue;
                }

                Node<IRule> value = new Node<IRule>();

                if (index == path.Count - 1)
                    value.AddValue(leafData);

                value.Parent = node;
                node.AddChild(key, value);
                node = value;
            }
        }
    }
}