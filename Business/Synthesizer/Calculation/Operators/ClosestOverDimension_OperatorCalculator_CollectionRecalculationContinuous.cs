using System.Runtime.CompilerServices;

namespace JJ.Business.Synthesizer.Calculation.Operators
{
    internal class ClosestOverDimension_OperatorCalculator_CollectionRecalculationContinuous : ClosestOverDimension_OperatorCalculator_Base
    {
        public ClosestOverDimension_OperatorCalculator_CollectionRecalculationContinuous(
            OperatorCalculatorBase inputCalculator,
            OperatorCalculatorBase collectionCalculator,
            OperatorCalculatorBase fromCalculator,
            OperatorCalculatorBase tillCalculator,
            OperatorCalculatorBase stepCalculator,
            OperatorCalculatorBase positionInputCalculator,
            VariableInput_OperatorCalculator positionOutputCalculator)
            : base(
                inputCalculator,
                collectionCalculator,
                fromCalculator,
                tillCalculator,
                stepCalculator,
                positionInputCalculator,
                positionOutputCalculator)
        { }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public override double Calculate()
        {
            RecalculateCollection();

            return base.Calculate();
        }
    }
}
