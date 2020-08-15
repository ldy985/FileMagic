namespace ldy985.FileMagic.Abstracts
{
    public interface IParsedHandler<TParsed> : IParsedHandler where TParsed : IParsed
    {
        void Execute(TParsed parsedObject);
    }

    public interface IParsedHandler
    {

    }
}