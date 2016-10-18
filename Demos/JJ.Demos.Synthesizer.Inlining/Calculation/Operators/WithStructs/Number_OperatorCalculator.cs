using System.Diagnostics;
using System.Runtime.CompilerServices;
using JJ.Demos.Synthesizer.Inlining.Helpers;

namespace JJ.Demos.Synthesizer.Inlining.Calculation.Operators.WithStructs
{
    [DebuggerDisplay("{DebuggerDisplay}")]
    internal struct Number_OperatorCalculator : IOperatorCalculator
    {
        private double _number;
        public double Number
        {
            get { return _number; }
            set { _number = value; }
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public double Calculate()
        {
            return _number;
        }

        private string DebuggerDisplay => DebugHelper.GetDebuggerDisplay(this);
    }
}
