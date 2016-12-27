using System;
using System.Diagnostics;
using System.Runtime.CompilerServices;
using JJ.Business.SynthesizerPrototype.Tests.Helpers;

namespace JJ.Business.SynthesizerPrototype.Tests.Calculation.WithStructs
{
    [DebuggerDisplay("{DebuggerDisplay}")]
    internal struct Number_OperatorCalculator_NaN : IOperatorCalculator
    {
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public double Calculate() => Double.NaN;

        private string DebuggerDisplay => DebugHelper.GetDebuggerDisplay(this);
    }
}
