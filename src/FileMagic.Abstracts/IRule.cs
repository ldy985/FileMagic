using System.Diagnostics.CodeAnalysis;
using System.IO;

namespace ldy985.FileMagic.Abstracts
{
    public interface IRule
    {
        string Name { get; }
        bool HasMagic { get; }
        IMagic? Magic { get; }
        ITypeInfo TypeInfo { get; }
        bool HasParser { get; }
        bool HasStructure { get; }
        bool TryParse([NotNull] BinaryReader reader, IResult result, [NotNullWhen(true)] out IParsed? parsed);

        bool TryStructure([NotNull] BinaryReader reader, IResult result);

        bool TryMagic([NotNull] BinaryReader stream);
    }
}