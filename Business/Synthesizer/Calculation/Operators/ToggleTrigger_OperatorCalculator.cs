using System.Runtime.CompilerServices;

namespace JJ.Business.Synthesizer.Calculation.Operators
{
    internal class ToggleTrigger_OperatorCalculator : OperatorCalculatorBase_WithChildCalculators
    {
        private readonly OperatorCalculatorBase _calculationCalculator;
        private readonly OperatorCalculatorBase _resetCalculator;

        /// <summary> Int32 because assigning 0 can be very fast in machine code. </summary>
        private int _previousZero;

        public ToggleTrigger_OperatorCalculator(
            OperatorCalculatorBase calculationCalculator,
            OperatorCalculatorBase resetCalculator)
            : base(new[] { calculationCalculator, resetCalculator })
        {
            _calculationCalculator = calculationCalculator;
            _resetCalculator = resetCalculator;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public override double Calculate()
        {
            double newTriggerValue = _resetCalculator.Calculate();

            // ReSharper disable once CompareOfFloatsByEqualityOperator
            bool newValueIsZero = newTriggerValue == 0.0;

            if (_previousZero == 0 && !newValueIsZero)
            {
                _calculationCalculator.Reset();

                // _previousZero = something non-zero, by flipping all bits.
                _previousZero = ~_previousZero;
            }
            else if (_previousZero != 0 && newValueIsZero)
            {
                _calculationCalculator.Reset();

                // _previousZero = 0, by XOR'ing it onto itself.
                _previousZero ^= _previousZero;
            }

            return _calculationCalculator.Calculate();
        }

        // Non-optimized version:
        //public override double Calculate()
        //{
        //	double newTriggerValue = _resetCalculator.Calculate();

        //	if (_previousTriggerValue == 0 && newTriggerValue != 0)
        //	{
        //		_calculationCalculator.ResetState();
        //	}
        //	else if (_previousTriggerValue != 0 && newTriggerValue == 0)
        //	{
        //		_calculationCalculator.ResetState();
        //	}

        //	_previousTriggerValue = newTriggerValue;

        //	return _calculationCalculator.Calculate();
        //}
    }
}