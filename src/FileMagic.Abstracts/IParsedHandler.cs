namespace ldy985.FileMagic.Abstracts
{
    public interface IParsedHandler<TParsed>
    {
        void Execute(TParsed parsedObject);
    }
}