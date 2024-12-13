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
        
        private string DebuggerDisplay => GetDebuggerDisplay(this);
    }
}
