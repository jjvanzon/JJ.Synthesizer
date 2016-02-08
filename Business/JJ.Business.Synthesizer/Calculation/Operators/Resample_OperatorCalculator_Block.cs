using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using JJ.Framework.Reflection.Exceptions;

namespace JJ.Business.Synthesizer.Calculation.Operators
{
    internal class Resample_OperatorCalculator_Block : OperatorCalculatorBase_WithChildCalculators
    {
        private readonly OperatorCalculatorBase _signalCalculator;
        private readonly OperatorCalculatorBase _samplingRateCalculator;

        private double _t0;
        private double _x0;

        public Resample_OperatorCalculator_Block(
            OperatorCalculatorBase signalCalculator,
            OperatorCalculatorBase samplingRateCalculator)
            : base(new OperatorCalculatorBase[] { signalCalculator, samplingRateCalculator })
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
                // Weird number: sampling rate 0 = time stands still.
                return _x0;
            }

            double sampleDuration = 1.0 / samplingRate;

            double tOffset = time - _t0;

            bool isForwardInTime = tOffset >= 0.0;
            if (isForwardInTime)
            {
                bool exceedsSampleDuration = tOffset > sampleDuration;
                if (exceedsSampleDuration)
                {
                    bool exceedsMoreThanTwoSampleDurations = tOffset > sampleDuration + sampleDuration;
                    if (!exceedsMoreThanTwoSampleDurations)
                    {
                        _t0 += sampleDuration;
                    }
                    else
                    {
                        double sampleCount = Math.Floor(tOffset * samplingRate);
                        _t0 += sampleCount * sampleDuration;
                    }

                    _x0 = _signalCalculator.Calculate(_t0, channelIndex);
                }
            }
            else
            {
                // Time in reverse
                bool exceedsSampleDuration = tOffset < -sampleDuration;
                if (exceedsSampleDuration)
                {
                    bool exceedsMoreThanTwoSampleDurations = tOffset < -sampleDuration - sampleDuration;
                    if (!exceedsMoreThanTwoSampleDurations)
                    {
                        _t0 -= sampleDuration;
                    }
                    else
                    {
                        double sampleCount = Math.Ceiling(tOffset * samplingRate); // tOffset is negative.
                        _t0 += sampleCount * sampleDuration;
                    }

                    _x0 = _signalCalculator.Calculate(_t0, channelIndex);
                }
            }

            return _x0;
        }
    }
}