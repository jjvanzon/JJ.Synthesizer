namespace JJ.Business.Synthesizer.Tests.Accessors;

internal class TapeCollectionAccessor(object obj) : AccessorCore(obj)
{
    public IList<TapeAccessor> GetAll()
    {
        var tapes = (IList<object>)Call();
        return tapes.Select(x => new TapeAccessor(x)).ToArray();
    }
}
