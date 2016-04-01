using System;
using System.Collections.Generic;
using System.Linq;
using JJ.Framework.Reflection.Exceptions;

namespace JJ.Business.Synthesizer.Calculation.Operators
{
    internal class PulseTrigger_OperatorCalculator : OperatorCalculatorBase_WithChildCalculators
    {
        private readonly OperatorCalculatorBase _calculationCalculator;
        private readonly OperatorCalculatorBase _resetCalculator;

        private double _previousTriggerValue;

        public PulseTrigger_OperatorCalculator(
            OperatorCalculatorBase calculationCalculator,
            OperatorCalculatorBase resetCalculator)
            : base (new OperatorCalculatorBase[] { calculationCalculator, resetCalculator })
        {
            OperatorCalculatorHelper.AssertOperatorCalculatorBase(calculationCalculator, () => calculationCalculator);
            OperatorCalculatorHelper.AssertOperatorCalculatorBase(resetCalculator, () => resetCalculator);

            _calculationCalculator = calculationCalculator;
            _resetCalculator = resetCalculator;
        }

        //public override double Calculate(double time, int channelIndex)
        //{
        //    if (_previousTriggerValue == 0)
        //    {
        //        double newTriggerValue = _resetCalculator.Calculate(time, channelIndex);

        //        if (newTriggerValue != 0)
        //        {
        //            _calculationCalculator.ResetState();

        //            _previousTriggerValue = 1;
        //        }
        //        else
        //        {
        //            _previousTriggerValue = 0;
        //        }
        //    }
        //    else
        //    {
        //        _previousTriggerValue = 0;
        //    }

        //    return _calculationCalculator.Calculate(time, channelIndex);
        //}

        //public override double Calculate(double time, int channelIndex)
        //{
        //    if (_previousTriggerValue == 0)
        //    {
        //        double newTriggerValue = _resetCalculator.Calculate(time, channelIndex);

        //        if (newTriggerValue != 0)
        //        {
        //            _calculationCalculator.ResetState();

        //            _previousTriggerValue = 1;
        //        }
        //    }
        //    else
        //    {
        //        _previousTriggerValue = 1;
        //    }

        //    return _calculationCalculator.Calculate(time, channelIndex);
        //}

        public override double Calculate(double time, int channelIndex)
        {
            double newTriggerValue = _resetCalculator.Calculate(time, channelIndex);

            if (_previousTriggerValue == 0.0 && newTriggerValue != 0.0)
            {
                _calculationCalculator.ResetState();
            }

            _previousTriggerValue = newTriggerValue;

            return _calculationCalculator.Calculate(time, channelIndex);
        }

        //public override double Calculate(double time, int channelIndex)
        //{
        //    if (_previousResetValue == 0.0)
        //    {
        //        double newResetValue = _resetCalculator.Calculate(time, channelIndex);

        //        if (newResetValue != 0.0)
        //        {
        //            _calculationCalculator.ResetState();

        //            _previousResetValue = newResetValue;
        //        }
        //    }
        //    else
        //    {
        //        _previousResetValue = 0.0;
        //    }

        //    throw new NotImplementedException();
        //}
    }
}
