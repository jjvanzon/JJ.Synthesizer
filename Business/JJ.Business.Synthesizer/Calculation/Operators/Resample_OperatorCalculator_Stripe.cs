using System;
using System.Collections.Generic;
using System.Linq;
using JJ.Business.Synthesizer.Enums;
using JJ.Framework.Reflection.Exceptions;

namespace JJ.Business.Synthesizer.Calculation.Operators
{
    internal class Resample_OperatorCalculator_Stripe : Resample_OperatorCalculator_Block
    {
        private readonly OperatorCalculatorBase _signalCalculator;
        private readonly OperatorCalculatorBase _samplingRateCalculator;
        private readonly DimensionStack _dimensionStack;
        private readonly int _currentDimensionStackIndex;
        private readonly int _previousDimensionStackIndex;

        public Resample_OperatorCalculator_Stripe(
            OperatorCalculatorBase signalCalculator,
            OperatorCalculatorBase samplingRateCalculator,
            DimensionStack dimensionStack)
            : base(signalCalculator, samplingRateCalculator, dimensionStack)
        {
            OperatorCalculatorHelper.AssertOperatorCalculatorBase(signalCalculator, () => signalCalculator);
            if (samplingRateCalculator == null) throw new NullException(() => samplingRateCalculator);
            // TODO: Resample with constant sampling rate does not have specialized calculators yet. Reactivate code line after those specialized calculators have been programmed.
            //if (samplingRateCalculator is Number_OperatorCalculator) throw new IsNotTypeException<Number_OperatorCalculator>(() => samplingRateCalculator);
            if (dimensionStack == null) throw new NullException(() => dimensionStack);

            _signalCalculator = signalCalculator;
            _samplingRateCalculator = samplingRateCalculator;
            _dimensionStack = dimensionStack;
            _currentDimensionStackIndex = dimensionStack.CurrentIndex;
            _previousDimensionStackIndex = dimensionStack.CurrentIndex - 1;
        }

        public override double Calculate()
        {
            double position = _dimensionStack.Get(_previousDimensionStackIndex);

            double samplingRate = _samplingRateCalculator.Calculate();
            if (samplingRate == 0.0)
            {
                // Weird number: Cannot divide by 0. Time stands still. Do not advance the signal.
                return _value0;
            }

            // Derived from the following:
            // sampleLength = 1.0 / samplingRate;
            // shift = sampleLength / 2.0;
            double earlierPositionShiftToGetFromBlockedToStriped = 0.5 / samplingRate;

            // IMPORTANT: To subtract time from the output, you have add time to the input.
            double transformedPosition = position + earlierPositionShiftToGetFromBlockedToStriped;

            _dimensionStack.Set(_currentDimensionStackIndex, transformedPosition);

            double value = Calculate(samplingRate);

            return value;
        }
    }
}