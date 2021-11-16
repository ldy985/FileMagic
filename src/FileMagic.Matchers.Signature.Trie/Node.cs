using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ldy985.FileMagic.Matchers.Signature.Trie
{
    public class Node<T>
    {
        public Node<T>? Parent { get; set; }

        public Dictionary<ushort, Node<T>>? Children { get; set; }

        /// <summary>
        ///     Only set in leafs
        /// </summary>
        public ICollection<T>? Values { get; private set; }

        public string MagicString
        {
            get
            {
                StringBuilder sb = new StringBuilder();

                foreach (ushort? data in GetMagic())
                    sb.Append(data.HasValue ? $"{data:X2}" : "??");

                return sb.ToString();
            }
        }

        public IEnumerable<ushort?> GetMagic()
        {
            if (Parent == null)
                return Enumerable.Empty<ushort?>();

            return GetMagicInternal(Parent, this).Select(data => data == ushort.MaxValue ? null : (ushort?)data).Reverse();
        }

        private IEnumerable<ushort> GetMagicInternal(Node<T> lastNode, Node<T> node2)
        {
            if (lastNode.Children == null)
                yield break;

            foreach ((ushort key, var value) in lastNode.Children)
            {
                if (value != node2)
                    continue;

                yield return key;
                break;
            }

            if (lastNode.Parent == null)
                yield break;

            foreach (ushort key in GetMagicInternal(lastNode.Parent, lastNode))
                yield return key;
        }

        public IEnumerable<T> GetAllLeafs()
        {
            return Values ?? ValuesDeep(this).Distinct();
        }

        private static IEnumerable<T> ValuesDeep(Node<T> node)
        {
            var valuesDeep = Enumerable.Empty<T>();

            if (node.Children != null)
                foreach (var pair in node.Children)
                    valuesDeep = valuesDeep.Concat(ValuesDeep(pair.Value));

            if (node.Values != null)
                valuesDeep = valuesDeep.Concat(node.Values);

            return valuesDeep;
        }

        public void AddValue(T type)
        {
            (Values ??= new List<T>()).Add(type);
        }

        public void AddChild(ushort key, Node<T> value)
        {
            (Children ??= new Dictionary<ushort, Node<T>>()).Add(key, value);
        }

        /// <inheritdoc />
        public override string ToString()
        {
            return MagicString;
        }
    }
}