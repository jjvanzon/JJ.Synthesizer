using System.Runtime.CompilerServices;

namespace JJ.Business.Synthesizer.Calculation.Operators
{
    internal class AverageOverDimension_OperatorCalculator_CollectionRecalculationUponReset
        : SumOverDimension_OperatorCalculator_CollectionRecalculationUponReset
    {
        public AverageOverDimension_OperatorCalculator_CollectionRecalculationUponReset(
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