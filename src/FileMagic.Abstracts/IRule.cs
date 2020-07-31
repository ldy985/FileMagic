using System.IO;

namespace ldy985.FileMagic.Abstracts
{
    public interface IRule
    {
        string Name { get; }
        bool HasMagic { get; }
        IMagic Magic { get; }
        ITypeInfo TypeInfo { get; }
        bool HasParser { get; }
        bool HasStructure { get; }
        bool TryParse(BinaryReader reader, IResult result);

        bool TryStructure(BinaryReader reader, IResult result);

        bool TryMagic(BinaryReader stream);
    }
}