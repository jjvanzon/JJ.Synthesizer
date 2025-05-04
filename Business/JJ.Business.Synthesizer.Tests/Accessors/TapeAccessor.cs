// ReSharper disable RedundantTypeArgumentsOfMethod
namespace JJ.Business.Synthesizer.Tests.Accessors;

internal class TapeAccessor(object obj) : AccessorCore(obj)
{
    public FlowNode Signal 
    { 
        get => Get<FlowNode>(); 
        set => Set<FlowNode>(value); }
}
