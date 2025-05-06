namespace JJ.Business.Synthesizer.Tests.Accessors;

internal class AdditiveTestsAccessor(AdditiveTests obj)
{
    private readonly AccessorCore _accessor = new(obj);
    
    
    /// <inheritdoc cref="_captureindexer" />
    public SynthWishes _ => _accessor.Get<SynthWishes>();
    
    /// <inheritdoc cref="_metallophone"/>
    public FlowNode MetallophoneJingle()
        => (FlowNode)_accessor.Call();
    
    /// <inheritdoc cref="_metallophone"/>
    public FlowNode MetallophoneChord
        => (FlowNode)_accessor.Get();
    
    /// <inheritdoc cref="_metallophone"/>
    public FlowNode Metallophone(FlowNode freq, FlowNode duration = null)
        => (FlowNode)_accessor.Call(freq, duration);
}