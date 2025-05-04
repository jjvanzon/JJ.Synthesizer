namespace JJ.Business.Synthesizer.Tests.Accessors;

internal class FMTestsAccessor(FMTests obj) : AccessorCore(obj)
{
    /// <inheritdoc cref="_captureindexer" />
    public SynthWishes _ => Get<SynthWishes>();
           
    public void Run(Action action) => Call(action);

    /// <inheritdoc cref="_flute1" />
    public FlowNode Flute1(FlowNode freq = null, FlowNode duration = null)
        => (FlowNode)Call(freq, duration);

    /// <inheritdoc cref="_flute2" />
    public FlowNode Flute2(FlowNode freq = null, FlowNode duration = null)
        => (FlowNode)Call(freq, duration);

    /// <inheritdoc cref="_flute3" />
    public FlowNode Flute3(FlowNode freq = null, FlowNode duration = null)
        => (FlowNode)Call(freq, duration);

    /// <inheritdoc cref="_flute4" />
    public FlowNode Flute4(FlowNode freq = null, FlowNode duration = null)
        => (FlowNode)Call(freq, duration);

    /// <inheritdoc cref="_default" />
    public FlowNode Pad(FlowNode freq = null, FlowNode duration = null)
        => (FlowNode)Call(freq, duration);
    
    public FlowNode Organ(FlowNode freq = null, FlowNode duration = null)
        => (FlowNode)Call(freq, duration);

    /// <inheritdoc cref="_horn" />
    public FlowNode Horn(FlowNode freq = null, FlowNode duration = null)
        => (FlowNode)Call(freq, duration);
        
    /// <inheritdoc cref="_trombone" />
    public FlowNode Trombone(FlowNode freq = null, FlowNode durationFactor = null)        
        => (FlowNode)Call(freq, durationFactor);
    
    /// <inheritdoc cref="_default" />
    public FlowNode ElectricNote(FlowNode freq = null, FlowNode duration = null)
        => (FlowNode)Call(freq, duration);
    
    /// <inheritdoc cref="_ripplebass" />
    public FlowNode RippleBass(FlowNode freq = null, FlowNode duration = null)
        => (FlowNode)Call(freq, duration);
    
    /// <inheritdoc cref="_ripplebass" />
    public FlowNode RippleNote_SharpMetallic(FlowNode freq = null, FlowNode duration = null)
        => (FlowNode)Call(freq, duration);
    
    /// <inheritdoc cref="_ripplebass" />
    public FlowNode RippleSound_Clean(FlowNode freq = null, FlowNode duration = null)
        => (FlowNode)Call(freq, duration);
    
    /// <inheritdoc cref="_ripplebass" />
    public FlowNode RippleSound_FantasyEffect(FlowNode freq = null, FlowNode duration = null)
        => (FlowNode)Call(freq, duration);
    
    /// <inheritdoc cref="_ripplebass" />
    public FlowNode RippleSound_CoolDouble(FlowNode freq = null, FlowNode duration = null)
        => (FlowNode)Call(freq, duration);
    
    /// <inheritdoc cref="_ripplebass" />
    public FlowNode Create_FM_Noise_Beating(FlowNode freq = null, FlowNode duration = null)
        => (FlowNode)Call(freq, duration);

    public FlowNode FluteMelody1 => (FlowNode)Get();
    public FlowNode FluteMelody2 => (FlowNode)Get();
    public FlowNode OrganChords => (FlowNode)Get();
    public FlowNode PadChords() => (FlowNode)Call();
    public FlowNode PadChords2() => (FlowNode)Call();
    public FlowNode HornMelody1 => (FlowNode)Get();
    public FlowNode HornMelody2 => (FlowNode)Get();
    public FlowNode TromboneMelody1 => (FlowNode)Get();
    public FlowNode TromboneMelody2 => (FlowNode)Get();
    public FlowNode RippleBassMelody2 => (FlowNode)Get();
    public FlowNode Jingle() => (FlowNode)Call();
}
