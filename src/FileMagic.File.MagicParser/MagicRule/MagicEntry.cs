using System.Collections.Generic;
using System.Text;
using Chronos.Libraries.FileClassifier.MagicRule;

namespace Chronos.Libraries.FileClassifier.entries
{
    /// <summary>
    /// One magic rule.
    /// </summary>
    public class MagicEntry
    {
        public List<MagicEntry> Children { get; set; }

        public MagicEntry()
        {
            Offset = new Offset();
            DataType = new DataType();
            Test = new Test();
            PatternInfo = new PatternInfo();
            Modifiers = new Modifiers();
        }

        public bool ClearFormat { get; set; }

        public DataType DataType { get; set; }

        public bool FormatSpacePrefix { get; set; } = true;

        public MagicFormatter Formatter { get; set; }

        public int Level { get; set; }

        public string MimeType { get; set; }

        public Modifiers Modifiers { get; set; }

        public string Name { get; set; }

        public Offset Offset { get; set; }

        public PatternInfo PatternInfo { get; set; }

        public Flags StrFlags { get; set; }

        public Test Test { get; set; }

        internal bool IsOptional { get; set; }

        public FileRuleParser FileRuleParserPtr { get; set; }

        public void AddChild(MagicEntry child)
        {
            if (Children == null)
            {
                Children = new List<MagicEntry>();
            }

            Children.Add(child);
        }

        /// Gets the Magic string format of the rule.
        /// </summary>
        /// <returns>The <see cref="string"/>.</returns>
        public override string ToString()
        {
            var sb = new StringBuilder();

            for (var i = 0; i < Level; i++)
            {
                sb.Append('>');
            }

            if (Offset.Realative)
            {
                sb.Append('&');
            }

            if (Offset.Indirection != null && Offset.Indirection.Type != 0)
            {
                sb.Append('(');
            }

            if (Offset.Indirection != null && Offset.Indirection.Relative)
            {
                sb.Append('&');
            }

            if (Offset.Indirection != null && Offset.Indirection.Type != 0)
            {
                sb.Append(Offset.Value);
                if (Offset.Indirection.Unsigned)
                {
                    sb.Append('.');
                }
                else
                {
                    sb.Append(',');
                }

                sb.Append(Offset.Indirection.Type);

                if (Offset.Indirection.Op != 0)
                {
                    sb.Append((char)Offset.Indirection.Op);
                    if (Offset.Indirection.ModifierValue < 0)
                    {
                        sb.Append('(');
                        sb.Append(Offset.Indirection.ModifierValue);
                        sb.Append(')');
                    }
                    else
                    {
                        sb.Append(Offset.Indirection.ModifierValue);
                    }
                }

                sb.Append(')');
            }
            else
            {
                sb.Append(Offset.Value);
            }

            sb.Append(' ');
            if (DataType.Unsigned)
            {
                sb.Append('u');
            }

            sb.Append(DataType.Type.ToString().ToLower());
            if (DataType.Op != 0)
            {
                sb.Append((char)DataType.Op);
            }

            if (DataType.ModifierValue != null)
            {
                sb.Append(DataType.ModifierValue);
            }

            sb.Append('\t');

            if (Test != null && Test.Op != 0)
            {
                sb.Append((char)Test.Op);
            }

            if (Test != null)
            {
                sb.Append(Test.Value);
            }
            else
            {
                sb.Append('x');
            }

            sb.Append('\t');

            if (Formatter != null)
            {
                sb.Append(Formatter);
            }

            //if (this.Matcher is NumberType numberType)
            //{
            //    sb.Append('\t');

            //    sb.Append(numberType.endianConverter.GetType().Name);
            //}

            return sb.ToString();
        }
    }
}