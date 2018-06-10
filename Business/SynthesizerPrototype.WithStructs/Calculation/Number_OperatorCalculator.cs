using System.Diagnostics;
using System.Runtime.CompilerServices;
using JJ.Business.SynthesizerPrototype.WithStructs.Helpers;

namespace JJ.Business.SynthesizerPrototype.WithStructs.Calculation
{
    [DebuggerDisplay("{" + nameof(DebuggerDisplay) + "}")]
    public struct Number_OperatorCalculator : IOperatorCalculator
    {
        public double Number { get; set; }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public double Calculate() => Number;

        private string DebuggerDisplay => DebugHelper.GetDebuggerDisplay(this);
    }
}