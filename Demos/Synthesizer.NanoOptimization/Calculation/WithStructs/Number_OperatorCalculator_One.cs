using System.Diagnostics;
using System.Runtime.CompilerServices;
using JJ.Demos.Synthesizer.NanoOptimization.Helpers;

namespace JJ.Demos.Synthesizer.NanoOptimization.Calculation.WithStructs
{
    [DebuggerDisplay("{DebuggerDisplay}")]
    internal struct Number_OperatorCalculator_One : IOperatorCalculator
    {
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public double Calculate() => 1.0;

        private string DebuggerDisplay => DebugHelper.GetDebuggerDisplay(this);
    }
}
