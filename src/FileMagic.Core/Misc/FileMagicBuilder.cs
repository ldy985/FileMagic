using ldy985.FileMagic.Abstracts;
using Microsoft.Extensions.DependencyInjection;

namespace ldy985.FileMagic.Core.Misc
{
    public class FileMagicBuilder : IFileMagicBuilder
    {
        public FileMagicBuilder(IServiceCollection services)
        {
            Services = services;
        }

        public IServiceCollection Services { get; }
    }
}