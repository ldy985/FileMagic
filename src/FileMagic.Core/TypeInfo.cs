using ldy985.FileMagic.Abstracts;

namespace ldy985.FileMagic.Core
{
    public class TypeInfo : ITypeInfo
    {
        public string Description { get; }
        public string[] Extensions { get; }

        public TypeInfo(string description, params string[] extensions)
        {
            Description = description;
            Extensions = extensions;
        }
    }
}