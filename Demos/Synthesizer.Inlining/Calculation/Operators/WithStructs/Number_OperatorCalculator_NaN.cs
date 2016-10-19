using System;
using System.Diagnostics;
using System.Runtime.CompilerServices;
using JJ.Demos.Synthesizer.Inlining.Helpers;

namespace JJ.Demos.Synthesizer.Inlining.Calculation.Operators.WithStructs
{
    [DebuggerDisplay("{DebuggerDisplay}")]
    internal struct Number_OperatorCalculator_NaN : IOperatorCalculator
    {
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public double Calculate() => Double.NaN;

        private string DebuggerDisplay => DebugHelper.GetDebuggerDisplay(this);
    }
}
