using Microsoft.Extensions.DependencyInjection;

namespace ldy985.FileMagic.Abstracts
{
    public interface IFileMagicBuilder
    {
        IServiceCollection Services { get; }
    }
}