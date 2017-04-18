using System.Runtime.CompilerServices;
using JJ.Business.SynthesizerPrototype.WithInheritance.CopiedCode.From_JJ_Business_SynthesizerPrototype;
using JJ.Framework.Exceptions;

namespace JJ.Business.SynthesizerPrototype.WithInheritance.Calculation
{
    internal class Shift_OperatorCalculator_VarSignal_VarDifference : OperatorCalculatorBase
    {
        private readonly OperatorCalculatorBase _signalCalculator;
        private readonly OperatorCalculatorBase _distanceCalculator;
        private readonly DimensionStack _dimensionStack;

        public Shift_OperatorCalculator_VarSignal_VarDifference(
            OperatorCalculatorBase signalCalculator,
            OperatorCalculatorBase distanceCalculator,
            DimensionStack dimensionStack)
        {
            _signalCalculator = signalCalculator ?? throw new NullException(() => signalCalculator);
            _distanceCalculator = distanceCalculator ?? throw new NullException(() => distanceCalculator);
            _dimensionStack = dimensionStack ?? throw new NullException(() => dimensionStack);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public override double Calculate()
        {
            double transformedPosition = GetTransformedPosition();

            _dimensionStack.Push(transformedPosition);

            double result = _signalCalculator.Calculate();

            _dimensionStack.Pop();

            return result;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        private double GetTransformedPosition()
        {
            double position = _dimensionStack.Get();

            double distance = _distanceCalculator.Calculate();

            // IMPORTANT: To shift to the right in the output, you have shift to the left in the input.
            double transformedPosition = position - distance;

            return transformedPosition;
        }
    }
}
