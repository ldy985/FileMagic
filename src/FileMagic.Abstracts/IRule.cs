#if NETSTANDARD2_1
using System.Diagnostics.CodeAnalysis;
#endif
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
#if NETSTANDARD2_1
        bool TryParse(BinaryReader reader, IResult result, [NotNullWhen(true)]out IParsed? parsed);
#else
        bool TryParse(BinaryReader reader, IResult result, out IParsed? parsed);
#endif

        bool TryStructure(BinaryReader reader, IResult result);

        bool TryMagic(BinaryReader stream);
    }
}