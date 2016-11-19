using System.Diagnostics;
using System.Runtime.CompilerServices;
using JJ.Business.Synthesizer.Tests.NanoOptimization.Helpers;

namespace JJ.Business.Synthesizer.Tests.NanoOptimization.Calculation.WithStructs
{
    [DebuggerDisplay("{DebuggerDisplay}")]
    internal struct Number_OperatorCalculator_Zero : IOperatorCalculator
    {
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public double Calculate() => 0.0;

        private string DebuggerDisplay => DebugHelper.GetDebuggerDisplay(this);
    }
}
