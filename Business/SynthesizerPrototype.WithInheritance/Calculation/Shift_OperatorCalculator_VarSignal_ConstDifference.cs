using System.Runtime.CompilerServices;
using JJ.Business.SynthesizerPrototype.WithInheritance.CopiedCode.From_JJ_Business_SynthesizerPrototype;
using JJ.Framework.Exceptions.Basic;

namespace JJ.Business.SynthesizerPrototype.WithInheritance.Calculation
{
    internal class Shift_OperatorCalculator_VarSignal_ConstDifference : OperatorCalculatorBase
    {
        private readonly OperatorCalculatorBase _signalCalculator;
        private readonly double _distance;
        private readonly DimensionStack _dimensionStack;

        public Shift_OperatorCalculator_VarSignal_ConstDifference(
            OperatorCalculatorBase signalCalculator,
            double distance,
            DimensionStack dimensionStack)
        {
            _signalCalculator = signalCalculator ?? throw new NullException(() => signalCalculator);
            _distance = distance;
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

            // IMPORTANT: To shift to the right in the output, you have shift to the left in the input.
            double transformedPosition = position - _distance;

            return transformedPosition;
        }
    }
}
