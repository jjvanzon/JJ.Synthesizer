using JJ.Persistence.Synthesizer;
using System;
using System.Diagnostics;
using static JJ.Business.Synthesizer.Wishes.Helpers.DebuggerDisplayFormatter;

namespace JJ.Business.Synthesizer.Wishes
{
    [DebuggerDisplay("{DebuggerDisplay}")]
    public partial class FlowNode
    {
        internal FlowNode(SynthWishes synthWishes, Outlet firstOperand)
        {
            _synthWishes = synthWishes ?? throw new ArgumentNullException(nameof(synthWishes));
            _underlyingOutlet = firstOperand ?? throw new ArgumentNullException(nameof(firstOperand));
            Operands = new FluentOperandList(this);
        }
 
        private readonly SynthWishes _synthWishes;
        public Outlet UnderlyingOutlet => _underlyingOutlet;

        private readonly Outlet _underlyingOutlet;
        public SynthWishes SynthWishes => _synthWishes;
        
        public FluentOperandList Operands { get; }

        /// <inheritdoc cref="docs._captureindexer" />
        public CaptureIndexer _ => _synthWishes._;

        // Conversion Operators
        
        public static implicit operator Outlet(FlowNode flowNode) => flowNode?._underlyingOutlet;
    
        public static explicit operator double(FlowNode flowNode) => flowNode.Value;

        // Command Indexers
        
        // No Parameters
        
        public FlowNode this[Func<FlowNode> func] 
            => _synthWishes[func];
        
        // 1 Parameter
        
        public FlowNode this[
            Func<FlowNode, FlowNode> func] 
            => _synthWishes[func, this];
        
        // 2 Parameters
        
        public FlowNode this[
            Func<FlowNode, FlowNode, FlowNode> func, 
            FlowNode arg2 = null] 
            => _synthWishes[func, this, arg2];
        
        public FlowNode this[
            Func<FlowNode, FlowNode, FlowNode> func, 
            double arg2] 
            => _synthWishes[func, this, arg2];
        
        // 3 Parameters
        
        public FlowNode this[
            Func<FlowNode, FlowNode, FlowNode, FlowNode> func, 
            FlowNode arg2 = null, FlowNode arg3 = null] 
            => _synthWishes[func, this, arg2, arg3];
            
        public FlowNode this[
            Func<FlowNode, FlowNode, FlowNode, FlowNode> func, 
            double arg2, FlowNode arg3 = null] 
            => _synthWishes[func, this, arg2, arg3];
               
        public FlowNode this[
            Func<FlowNode, FlowNode, FlowNode, FlowNode> func, 
            FlowNode arg2, double arg3] 
            => _synthWishes[func, this, arg2, arg3];

        public FlowNode this[
            Func<FlowNode, FlowNode, FlowNode, FlowNode> func, 
            double arg2, double arg3] 
            => _synthWishes[func, this, arg2, arg3];

        // 4 Parameters
        
        public FlowNode this[
            Func<FlowNode, FlowNode, FlowNode, FlowNode, FlowNode> func, 
            FlowNode arg2 = null, FlowNode arg3 = null, FlowNode arg4 = null] 
            => _synthWishes[func, this, arg2, arg3, arg4];

        // 5 Parameters
        
        public FlowNode this[
            Func<FlowNode, FlowNode, FlowNode, FlowNode, FlowNode, FlowNode> func, 
            FlowNode arg2 = null, FlowNode arg3 = null, FlowNode arg4 = null, FlowNode arg5 = null] 
            => _synthWishes[func, this, arg2, arg3, arg4, arg5];

        private string DebuggerDisplay => GetDebuggerDisplay(this);
    }
}
