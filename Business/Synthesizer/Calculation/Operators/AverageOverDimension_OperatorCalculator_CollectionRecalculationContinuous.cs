using System.Runtime.CompilerServices;

namespace JJ.Business.Synthesizer.Calculation.Operators
{
    internal class AverageOverDimension_OperatorCalculator_CollectionRecalculationContinuous
        : SumOverDimension_OperatorCalculator_CollectionRecalculationContinuous
    {
        public AverageOverDimension_OperatorCalculator_CollectionRecalculationContinuous(
            OperatorCalculatorBase signalCalculator,
            OperatorCalculatorBase fromCalculator,
            OperatorCalculatorBase tillCalculator,
            OperatorCalculatorBase stepCalculator,
            OperatorCalculatorBase positionInputCalculator,
            VariableInput_OperatorCalculator positionOutputCalculator)
            : base(signalCalculator, fromCalculator, tillCalculator, stepCalculator, positionInputCalculator, positionOutputCalculator)
        { }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        protected override void RecalculateCollection()
        {
            base.RecalculateCollection();

            _aggregate *= _step;
        }
    }
}