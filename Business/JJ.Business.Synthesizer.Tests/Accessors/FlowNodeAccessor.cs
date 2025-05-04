namespace JJ.Business.Synthesizer.Tests.Accessors;

internal class FlowNodeAccessor(FlowNode obj) : AccessorCore(obj)
{
    public string DebuggerDisplay => Get<string>();
}
