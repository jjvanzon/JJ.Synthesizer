using System.Diagnostics;
using System.Runtime.CompilerServices;
using JJ.Business.SynthesizerPrototype.WithStructs.Helpers;

namespace JJ.Business.SynthesizerPrototype.WithStructs.Calculation
{
    [DebuggerDisplay("{DebuggerDisplay}")]
    public struct Number_OperatorCalculator_Zero : IOperatorCalculator
    {
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public double Calculate() => 0.0;

        private string DebuggerDisplay => DebugHelper.GetDebuggerDisplay(this);
    }
}
