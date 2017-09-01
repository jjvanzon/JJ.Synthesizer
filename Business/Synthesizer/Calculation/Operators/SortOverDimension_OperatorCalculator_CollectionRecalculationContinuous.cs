using System.Runtime.CompilerServices;
using JJ.Business.Synthesizer.Helpers;

namespace JJ.Business.Synthesizer.Calculation.Operators
{
    internal class SortOverDimension_OperatorCalculator_CollectionRecalculationContinuous
        : SortOverDimension_OperatorCalculator_Base
    {
        public SortOverDimension_OperatorCalculator_CollectionRecalculationContinuous(
            OperatorCalculatorBase signalCalculator,
            OperatorCalculatorBase fromCalculator,
            OperatorCalculatorBase tillCalculator,
            OperatorCalculatorBase stepCalculator,
            OperatorCalculatorBase positionInputCalculator,
            VariableInput_OperatorCalculator positionOutputCalculator)
            : base(signalCalculator, fromCalculator, tillCalculator, stepCalculator, positionInputCalculator, positionOutputCalculator)
        { }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public override double Calculate()
        {
            RecalculateCollection();

            double position = _positionInputCalculator.Calculate();

            // Stripe interpolation
            position += 0.5;

            if (!ConversionHelper.CanCastToNonNegativeInt32WithMax(position, _count))
            {
                return 0.0;
            }

            int i = (int)position;

            double value = _samples[i];
            return value;
        }
    }
}