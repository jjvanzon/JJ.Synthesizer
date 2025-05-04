namespace JJ.Business.Synthesizer.Tests.Accessors;

internal class SynthWishesAccessor(SynthWishes obj) : AccessorCore(obj)
{
    /// <inheritdoc cref="_captureindexer" />
    public SynthWishes _ => (SynthWishes)Get();

    public void Run(Action action) => Call(action);

    public TapeRunnerAccessor _tapeRunner => new(Get());
    public TapeCollectionAccessor _tapes  => new(Get());
    public ConfigResolverAccessor _config => new(Get());

    public IList<FlowNode> FlattenTerms(FlowNode sumOrAdd) 
        => (IList<FlowNode>)Call(sumOrAdd);

    public IList<FlowNode> FlattenFactors(IList<FlowNode> operands) 
        => (IList<FlowNode>)Call(operands);

    public IList<FlowNode> FlattenFactors(FlowNode multiply) 
        => (IList<FlowNode>)Call(multiply);

    // EchoAdditive

    public FlowNode EchoAdditive(FlowNode sound, int count, FlowNode magnitude = null, FlowNode delay = null, [CallerMemberName] string name = "")
        => (FlowNode)Call([ sound, count, magnitude, delay, name ]);

    public FlowNode EchoAdditive(FlowNode sound, int count, double magnitude, FlowNode delay = null, [CallerMemberName] string name = "")
        => (FlowNode)Call([ sound, count, magnitude, delay, name ]);

    public FlowNode EchoAdditive(FlowNode sound, int count, FlowNode magnitude, double delay, [CallerMemberName] string name = "")
        => (FlowNode)Call([ sound, count, magnitude, delay, name ]);

    public FlowNode EchoAdditive(FlowNode sound, int count, double magnitude, double delay, [CallerMemberName] string name = "") 
        => (FlowNode)Call([ sound, count, magnitude, delay, name ]);

    // EchoFeedBack
    
    public FlowNode EchoFeedBack(FlowNode sound, int count, FlowNode magnitude = null, FlowNode delay = null, [CallerMemberName] string name = "")
        => (FlowNode)Call([ sound, count, magnitude, delay, name ]);

    public FlowNode EchoFeedBack(FlowNode sound, int count, double magnitude, FlowNode delay = null, [CallerMemberName] string name = "")
        => (FlowNode)Call([ sound, count, magnitude, delay, name ]);

    public FlowNode EchoFeedBack(FlowNode sound, int count, FlowNode magnitude, double delay, [CallerMemberName] string name = "")
        => (FlowNode)Call([ sound, count, magnitude, delay, name ]);
    
    public FlowNode EchoFeedBack(FlowNode sound, int count, double magnitude, double delay, [CallerMemberName] string name = "")
        => (FlowNode)Call([ sound, count, magnitude, delay, name ]);
}
