using System;
using System.Diagnostics;
using System.Runtime.CompilerServices;
using JJ.Business.SynthesizerPrototype.WithStructs.Helpers;

namespace JJ.Business.SynthesizerPrototype.WithStructs.Calculation
{
    [DebuggerDisplay("{DebuggerDisplay}")]
    public struct Number_OperatorCalculator_NaN : IOperatorCalculator
    {
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public double Calculate() => double.NaN;

        private string DebuggerDisplay => DebugHelper.GetDebuggerDisplay(this);
    }
}
