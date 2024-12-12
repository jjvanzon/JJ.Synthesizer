using System;

namespace JJ.Business.Synthesizer.Wishes
{
    /// <inheritdoc cref="docs._captureindexer" />
    public partial class CaptureIndexer
    {
        private readonly SynthWishes _synthWishes;
        
        /// <inheritdoc cref="docs._captureindexer" />
        internal CaptureIndexer(SynthWishes synthWishes) 
            => _synthWishes = synthWishes;
        
        // ReSharper disable once UnusedParameter.Global
        /// <summary>
        /// Crazy conversion operator, reintroducing the discard notation _
        /// sort of, for FlowNodes.
        /// </summary>
        /// <param name="captureIndexer"></param>
        public static implicit operator FlowNode(CaptureIndexer captureIndexer) => null;
        
        // Command Indexers
        
        // 1 Parameter
        
        public FlowNode this[
            Func<FlowNode, FlowNode> func, 
            FlowNode arg1 = null] 
            => _synthWishes[func, arg1];
        
        // 2 Parameters
        
        public FlowNode this[
            Func<FlowNode, FlowNode, FlowNode> func, 
            FlowNode arg1 = null, FlowNode arg2 = null] 
            => _synthWishes[func, arg1, arg2];
        
        // 3 Parameters
        
        public FlowNode this[
            Func<FlowNode, FlowNode, FlowNode, FlowNode> func, 
            FlowNode arg1 = null, FlowNode arg2 = null, FlowNode arg3 = null] 
            => _synthWishes[func, arg1, arg2, arg3];
        
        // 4 Parameters
        
        public FlowNode this[
            Func<FlowNode, FlowNode, FlowNode, FlowNode, FlowNode> func, 
            FlowNode arg1 = null, FlowNode arg2 = null, FlowNode arg3 = null, FlowNode arg4 = null] 
            => _synthWishes[func, arg1, arg2, arg3, arg4];
    }
}
