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
        
        // 0 Parameters
        
        public FlowNode this[
            Func<FlowNode> func] 
            => _synthWishes[func];
        
        // 1 Parameter
        
        public FlowNode this[
            Func<FlowNode, FlowNode> func, 
            FlowNode arg1 = null] 
            => _synthWishes[func, arg1];
        
        public FlowNode this[
            Func<FlowNode, FlowNode> func, 
            double arg1] 
            => _synthWishes[func, arg1];
        
        // 2 Parameters
        
        public FlowNode this[
            Func<FlowNode, FlowNode, FlowNode> func, 
            FlowNode arg1 = null, FlowNode arg2 = null] 
            => _synthWishes[func, arg1, arg2];
                
        public FlowNode this[
            Func<FlowNode, FlowNode, FlowNode> func, 
            double arg1, FlowNode arg2 = null] 
            => _synthWishes[func, arg1, arg2];
        
        public FlowNode this[
            Func<FlowNode, FlowNode, FlowNode> func, 
            FlowNode arg1, double arg2] 
            => _synthWishes[func, arg1, arg2];
        
        public FlowNode this[
            Func<FlowNode, FlowNode, FlowNode> func, 
            double arg1, double arg2] 
            => _synthWishes[func, arg1, arg2];

        // 3 Parameters
        
        public FlowNode this[
            Func<FlowNode, FlowNode, FlowNode, FlowNode> func, 
            FlowNode arg1 = null, FlowNode arg2 = null, FlowNode arg3 = null] 
            => _synthWishes[func, arg1, arg2, arg3];
        
        public FlowNode this[
            Func<FlowNode, FlowNode, FlowNode, FlowNode> func, 
            double arg1, FlowNode arg2 = null, FlowNode arg3 = null] 
            => _synthWishes[func, arg1, arg2, arg3];
        
        public FlowNode this[
            Func<FlowNode, FlowNode, FlowNode, FlowNode> func, 
            FlowNode arg1, double arg2, FlowNode arg3 = null] 
            => _synthWishes[func, arg1, arg2, arg3];
        
        public FlowNode this[
            Func<FlowNode, FlowNode, FlowNode, FlowNode> func, 
            FlowNode arg1, FlowNode arg2, double arg3] 
            => _synthWishes[func, arg1, arg2, arg3];
               
        public FlowNode this[
            Func<FlowNode, FlowNode, FlowNode, FlowNode> func, 
            double arg1, double arg2, FlowNode arg3] 
            => _synthWishes[func, arg1, arg2, arg3];
               
        public FlowNode this[
            Func<FlowNode, FlowNode, FlowNode, FlowNode> func, 
            double arg1, FlowNode arg2, double arg3] 
            => _synthWishes[func, arg1, arg2, arg3];

        public FlowNode this[
            Func<FlowNode, FlowNode, FlowNode, FlowNode> func, 
            FlowNode arg1, double arg2, double arg3] 
            => _synthWishes[func, arg1, arg2, arg3];
               
        public FlowNode this[
            Func<FlowNode, FlowNode, FlowNode, FlowNode> func, 
            double arg1, double arg2, double arg3] 
            => _synthWishes[func, arg1, arg2, arg3];
        
        // 4 Parameters
        
        public FlowNode this[
            Func<FlowNode, FlowNode, FlowNode, FlowNode, FlowNode> func, 
            FlowNode arg1 = null, FlowNode arg2 = null, FlowNode arg3 = null, FlowNode arg4 = null] 
            => _synthWishes[func, arg1, arg2, arg3, arg4];
        
        // 5 Parameters
        
        public FlowNode this[
            Func<FlowNode, FlowNode, FlowNode, FlowNode, FlowNode, FlowNode> func, 
            FlowNode arg1 = null, FlowNode arg2 = null, FlowNode arg3 = null, FlowNode arg4 = null, FlowNode arg5 = null] 
            => _synthWishes[func, arg1, arg2, arg3, arg4, arg5];
    
        // TODO: With delegate for variadic parameters
    }
}
