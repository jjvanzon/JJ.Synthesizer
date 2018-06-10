using System.Diagnostics;
using System.Runtime.CompilerServices;
using JJ.Business.SynthesizerPrototype.WithStructs.CopiedCode.From_JJ_Business_SynthesizerPrototype;
using JJ.Business.SynthesizerPrototype.WithStructs.Helpers;

namespace JJ.Business.SynthesizerPrototype.WithStructs.Calculation
{
    [DebuggerDisplay("{" + nameof(DebuggerDisplay) + "}")]
    public struct Sine_OperatorCalculator_ConstFrequency_NoOriginShifting : IOperatorCalculator
    {
        public double Frequency { get; set; }

        public DimensionStack DimensionStack { get; set; }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public double Calculate()
        {
            double position = DimensionStack.Get();
            double value = SineCalculator.Sin(position * Frequency);

            return value;
        }

        private string DebuggerDisplay => DebugHelper.GetDebuggerDisplay(this);
    }
}