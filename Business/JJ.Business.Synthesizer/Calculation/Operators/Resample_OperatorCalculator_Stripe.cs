using System;
using System.Collections.Generic;
using System.Linq;
using JJ.Framework.Reflection.Exceptions;

namespace JJ.Business.Synthesizer.Calculation.Operators
{
    internal class Resample_OperatorCalculator_Stripe : Resample_OperatorCalculator_Block
    {
        private readonly OperatorCalculatorBase _signalCalculator;
        private readonly OperatorCalculatorBase _samplingRateCalculator;

        public Resample_OperatorCalculator_Stripe(
            OperatorCalculatorBase signalCalculator,
            OperatorCalculatorBase samplingRateCalculator)
            : base(signalCalculator, samplingRateCalculator)
        {
            if (signalCalculator == null) throw new NullException(() => signalCalculator);
            if (signalCalculator is Number_OperatorCalculator) throw new IsNotTypeException<Number_OperatorCalculator>(() => signalCalculator);
            if (samplingRateCalculator == null) throw new NullException(() => samplingRateCalculator);
            // TODO: Resample with constant sampling rate does not have specialized calculators yet. Reactivate code line after those specialized calculators have been programmed.
            //if (samplingRateCalculator is Number_OperatorCalculator) throw new IsNotTypeException<Number_OperatorCalculator>(() => samplingRateCalculator);

            _signalCalculator = signalCalculator;
            _samplingRateCalculator = samplingRateCalculator;
        }

        public override double Calculate(double time, int channelIndex)
        {
            double samplingRate = _samplingRateCalculator.Calculate(time, channelIndex);
            if (samplingRate == 0.0)
            {
                // Weird number: Cannot divide by 0. Time stands still. Do not advance the signal.
                return _x0;
            }

            // Derived from the following:
            // sampleDuration = 1.0 / samplingRate;
            // timeShift = sampleDuration / 2.0;
            double earlierTimeShiftToGetFromBlockedToStriped = 0.5 / samplingRate;

            // IMPORTANT: To subtract time from the output, you have add time to the input.
            double transformedTime = time + earlierTimeShiftToGetFromBlockedToStriped;

            return Calculate(transformedTime, channelIndex, samplingRate);
        }
    }
}