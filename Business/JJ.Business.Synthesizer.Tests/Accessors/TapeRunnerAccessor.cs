namespace JJ.Business.Synthesizer.Tests.Accessors;

internal class TapeRunnerAccessor(object obj) : AccessorCore(obj)
{
    public void RunAllTapes() => Call();
}
