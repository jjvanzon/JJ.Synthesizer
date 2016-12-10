using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;

namespace JJ.Business.Synthesizer.Calculation.Operators
{
    internal class ChangeTrigger_OperatorCalculator_VarPassThrough_VarReset : OperatorCalculatorBase_WithChildCalculators
    {
        private readonly OperatorCalculatorBase _calculationCalculator;
        private readonly OperatorCalculatorBase _resetCalculator;

        private double _previousTriggerValue;

        public ChangeTrigger_OperatorCalculator_VarPassThrough_VarReset(
            OperatorCalculatorBase calculationCalculator,
            OperatorCalculatorBase resetCalculator)
            : base(new OperatorCalculatorBase[] { calculationCalculator, resetCalculator })
        {
            OperatorCalculatorHelper.AssertChildOperatorCalculator(calculationCalculator, () => calculationCalculator);
            OperatorCalculatorHelper.AssertChildOperatorCalculator(resetCalculator, () => resetCalculator);

            _calculationCalculator = calculationCalculator;
            _resetCalculator = resetCalculator;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public override double Calculate()
        {
            double newTriggerValue = _resetCalculator.Calculate();

            if (_previousTriggerValue != newTriggerValue)
            {
                _calculationCalculator.Reset();

                _previousTriggerValue = newTriggerValue;
            }

            return _calculationCalculator.Calculate();
        }
    }
}