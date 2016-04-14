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
        private readonly int _dimensionIndex;

        public Resample_OperatorCalculator_Stripe(
            OperatorCalculatorBase signalCalculator,
            OperatorCalculatorBase samplingRateCalculator,
            DimensionEnum dimensionEnum)
            : base(signalCalculator, samplingRateCalculator, dimensionEnum)
        {
            OperatorCalculatorHelper.AssertOperatorCalculatorBase(signalCalculator, () => signalCalculator);
            if (samplingRateCalculator == null) throw new NullException(() => samplingRateCalculator);
            // TODO: Resample with constant sampling rate does not have specialized calculators yet. Reactivate code line after those specialized calculators have been programmed.
            //if (samplingRateCalculator is Number_OperatorCalculator) throw new IsNotTypeException<Number_OperatorCalculator>(() => samplingRateCalculator);

            _signalCalculator = signalCalculator;
            _samplingRateCalculator = samplingRateCalculator;
            _dimensionIndex = (int)dimensionEnum;
        }

        public override double Calculate(DimensionStack dimensionStack)
        {
            double position = dimensionStack.Get(_dimensionIndex);

            double samplingRate = _samplingRateCalculator.Calculate(dimensionStack);
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

            dimensionStack.Push(_dimensionIndex, transformedPosition);
            double value = Calculate(dimensionStack, samplingRate);
            dimensionStack.Pop(_dimensionIndex);

            return value;
        }
    }
}