using System.Runtime.CompilerServices;
using JJ.Business.SynthesizerPrototype.WithInheritance.CopiedCode.From_JJ_Business_SynthesizerPrototype;
using JJ.Framework.Exceptions.Basic;

namespace JJ.Business.SynthesizerPrototype.WithInheritance.Calculation
{
    internal class Sine_OperatorCalculator_ConstFrequency_NoOriginShifting : OperatorCalculatorBase
    {
        private readonly double _frequency;
        private readonly DimensionStack _dimensionStack;

        public Sine_OperatorCalculator_ConstFrequency_NoOriginShifting(
            double frequency,
            DimensionStack dimensionStack)
        {
            _frequency = frequency;
            _dimensionStack = dimensionStack ?? throw new NullException(() => dimensionStack);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public override double Calculate()
        {
            double position = _dimensionStack.Get();
            double value = SineCalculator.Sin(position * _frequency);

            return value;
        }
    }
}
