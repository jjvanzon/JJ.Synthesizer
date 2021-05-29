using System.Runtime.CompilerServices;
using JJ.Business.Synthesizer.Calculation.Arrays;

namespace JJ.Business.Synthesizer.Calculation.Operators
{
    internal class SortOverDimension_OperatorCalculator_CollectionRecalculationUponReset
        : SortOverDimension_OperatorCalculator_Base
    {
        private ArrayCalculator_MinPositionZero_Stripe_NoRate _arrayCalculator;

        public SortOverDimension_OperatorCalculator_CollectionRecalculationUponReset(
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
            double position = _positionInputCalculator.Calculate();

            double value = _arrayCalculator.Calculate(position);
            return value;
        }

        /// <summary> does nothing </summary>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        protected override void ResetNonRecursive()
        {
            RecalculateCollection();

            _arrayCalculator = new ArrayCalculator_MinPositionZero_Stripe_NoRate(_samples, valueBefore: 0, valueAfter: 0);
        }
    }
}