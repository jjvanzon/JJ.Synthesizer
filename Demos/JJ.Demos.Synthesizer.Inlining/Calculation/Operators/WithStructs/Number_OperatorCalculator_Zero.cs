using System.Diagnostics;
using System.Runtime.CompilerServices;
using JJ.Demos.Synthesizer.Inlining.Helpers;

namespace JJ.Demos.Synthesizer.Inlining.Calculation.Operators.WithStructs
{
    [DebuggerDisplay("{DebuggerDisplay}")]
    internal struct Number_OperatorCalculator_Zero : IOperatorCalculator
    {
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public double Calculate()
        {
            return 0.0;
        }

        private string DebuggerDisplay => DebugHelper.GetDebuggerDisplay(this);
    }
}
