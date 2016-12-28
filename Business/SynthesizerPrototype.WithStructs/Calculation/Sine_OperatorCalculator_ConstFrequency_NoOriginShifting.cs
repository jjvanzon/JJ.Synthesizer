using System.Diagnostics;
using System.Runtime.CompilerServices;
using JJ.Business.SynthesizerPrototype.WithStructs.CopiedCode.From_JJ_Business_SynthesizerPrototype;
using JJ.Business.SynthesizerPrototype.WithStructs.Helpers;

namespace JJ.Business.SynthesizerPrototype.WithStructs.Calculation
{
    [DebuggerDisplay("{DebuggerDisplay}")]
    public struct Sine_OperatorCalculator_ConstFrequency_NoOriginShifting : IOperatorCalculator
    {
        private double _frequency;
        public double Frequency
        {
            get { return _frequency; }
            set { _frequency = value; }
        }

        private DimensionStack _dimensionStack;
        public DimensionStack DimensionStack
        {
            get { return _dimensionStack; }
            set { _dimensionStack = value; }
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public double Calculate()
        {
            double position = _dimensionStack.Get();
            double value = SineCalculator.Sin(position * _frequency);

            return value;
        }

        private string DebuggerDisplay => DebugHelper.GetDebuggerDisplay(this);

    }
}
