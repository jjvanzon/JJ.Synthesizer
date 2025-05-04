namespace JJ.Business.Synthesizer.Tests.Accessors
{
    internal class ModulationTestsAccessor(ModulationTests obj) : AccessorCore(obj)
    {
        readonly ModulationTests _obj = obj ?? throw new ArgumentNullException(nameof(obj));
        
        /// <inheritdoc cref="_captureindexer" />
        public SynthWishes _ => (SynthWishes)Get(); 

        public void WithLeft() => _obj.WithLeft();
        public void WithRight() => _obj.WithRight();
        
        /// <inheritdoc cref="_detunica" />
        public FlowNode Detunica1(FlowNode freq, FlowNode duration = null, FlowNode detuneDepth = null, FlowNode chorusRate = null)
            => (FlowNode)Call(freq, duration, detuneDepth, chorusRate);

        /// <inheritdoc cref="_detunica" />
        public FlowNode Detunica2(FlowNode freq, FlowNode duration = null) => (FlowNode)Call(freq, duration);
        /// <inheritdoc cref="_detunica" />
        
        public FlowNode Detunica3(FlowNode freq, FlowNode duration = null) => (FlowNode)Call(freq, duration);
        
        /// <inheritdoc cref="_detunica" />
        public FlowNode Detunica4(FlowNode freq, FlowNode duration = null) => (FlowNode)Call(freq, duration);
        
        /// <inheritdoc cref="_detunica" />
        public FlowNode Detunica5(FlowNode freq, FlowNode duration = null) => (FlowNode)Call(freq, duration);
        
        /// <inheritdoc cref="_detunica" />
        public FlowNode DetunicaBass(FlowNode freq, FlowNode duration = null) => (FlowNode)Call(freq, duration);

        /// <inheritdoc cref="_detunica" />
        public FlowNode DetunicaJingle => (FlowNode)Get();

        public FlowNode Vibraphase(FlowNode freq, FlowNode duration = null, FlowNode depthAdjust1 = null, FlowNode depthAdjust2 = null)
            => (FlowNode)Call(freq, duration, depthAdjust1, depthAdjust2);
        
        public FlowNode VibraphaseChord => (FlowNode)Get();
    }
}
