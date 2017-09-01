using System.Runtime.CompilerServices;

namespace JJ.Business.Synthesizer.Calculation.Operators
{
    internal class ClosestOverDimensionExp_OperatorCalculator_CollectionRecalculationContinuous
        : ClosestOverDimensionExp_OperatorCalculator_Base
    {
        public ClosestOverDimensionExp_OperatorCalculator_CollectionRecalculationContinuous(
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