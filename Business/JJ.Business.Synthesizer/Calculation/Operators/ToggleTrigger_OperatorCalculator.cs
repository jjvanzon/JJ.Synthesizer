﻿using System;
using System.Collections.Generic;
using System.Linq;
using JJ.Framework.Reflection.Exceptions;

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
            : base(new OperatorCalculatorBase[] { calculationCalculator, resetCalculator })
        {
            OperatorCalculatorHelper.AssertOperatorCalculatorBase(calculationCalculator, () => calculationCalculator);
            OperatorCalculatorHelper.AssertOperatorCalculatorBase(resetCalculator, () => resetCalculator);

            _calculationCalculator = calculationCalculator;
            _resetCalculator = resetCalculator;
        }

        public override double Calculate(DimensionStack dimensionStack)
        {
            double newTriggerValue = _resetCalculator.Calculate(dimensionStack);

            bool newValueIsZero = newTriggerValue == 0;

            if (_previousZero == 0 && !newValueIsZero)
            {
                _calculationCalculator.Reset(dimensionStack);

                // _previousZero = something non-zero, by flipping all bits.
                _previousZero = ~_previousZero;
            }
            else if (_previousZero != 0 && newValueIsZero)
            {
                _calculationCalculator.Reset(dimensionStack);

                // _previousZero = 0, by XOR'ing it onto itself.
                _previousZero ^= _previousZero;
            }

            return _calculationCalculator.Calculate(dimensionStack);
        }

        // Non-optimized version:
        //public override double Calculate(DimensionStack dimensionStack)
        //{
        //    double newTriggerValue = _resetCalculator.Calculate(dimensionStack);

        //    if (_previousTriggerValue == 0 && newTriggerValue != 0)
        //    {
        //        _calculationCalculator.ResetState();
        //    }
        //    else if (_previousTriggerValue != 0 && newTriggerValue == 0)
        //    {
        //        _calculationCalculator.ResetState();
        //    }

        //    _previousTriggerValue = newTriggerValue;

        //    return _calculationCalculator.Calculate(dimensionStack);
        //}
    }
}