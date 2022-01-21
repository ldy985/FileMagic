using System;

namespace ldy985.FileMagic.Abstracts
{
    public interface IParsedHandlerProvider
    {
        void ExecuteHandlers(IRule type, IParsed parsed);
        IParsedHandlerProvider AddParsedHandler<TRule, TParsed>(Action<TParsed> action1) where TRule : IRule where TParsed : IParsed;
    }
}