using JJ.Persistence.Synthesizer;
using System;
using System.Diagnostics;
using JJ.Business.Synthesizer.Wishes.Logging;
using JJ.Business.Synthesizer.Wishes.OperandWishes;
using static JJ.Business.Synthesizer.Wishes.Helpers.DebuggerDisplayFormatter;
// ReSharper disable AssignmentInsteadOfDiscard

namespace JJ.Business.Synthesizer.Wishes
{
    [DebuggerDisplay("{DebuggerDisplay}")]
    public partial class FlowNode
    {
        private string DebuggerDisplay => GetDebuggerDisplay(this);
        public override string ToString() => _underlyingOutlet.Stringify(true, true);

        internal FlowNode(SynthWishes synthWishes, Outlet firstOperand)
        {
            _synthWishes = synthWishes ?? throw new ArgumentNullException(nameof(synthWishes));
            _underlyingOutlet = firstOperand ?? throw new ArgumentNullException(nameof(firstOperand));
            Operands = new FluentOperandList(this);
        }
 

        /// <summary> Always filled in. </summary>
        private readonly SynthWishes _synthWishes;
        /// <summary> Always filled in. </summary>
        public SynthWishes SynthWishes => _synthWishes;
        /// <summary> Always filled in. </summary>
        public SynthWishes _ => _synthWishes;

        
        /// <summary> Always filled in. </summary>
        public Outlet UnderlyingOutlet => _underlyingOutlet;
        /// <summary> Always filled in. </summary>
        private readonly Outlet _underlyingOutlet;
        
        public FluentOperandList Operands { get; }


        // Conversion Operators
        
        public static implicit operator Outlet(FlowNode flowNode) => flowNode?._underlyingOutlet;
    
        public static explicit operator double(FlowNode flowNode) => flowNode.Value;
    }
}
