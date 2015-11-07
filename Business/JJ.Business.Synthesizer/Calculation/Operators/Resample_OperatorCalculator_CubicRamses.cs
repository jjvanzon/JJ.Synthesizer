using JJ.Framework.Mathematics;
using JJ.Framework.Reflection.Exceptions;
using System;

namespace JJ.Business.Synthesizer.Calculation.Operators
{
    /// <summary> This is the currently used variation on the Resample_OperatorCalculator. </summary>
    internal class Resample_OperatorCalculator_CubicRamses : OperatorCalculatorBase
    {
        // TODO: This division does not seem to be right....
        private double MINIMUM_SAMPLING_RATE = 1.0 / 8.0; // 8 Hz.

        private OperatorCalculatorBase _signalCalculator;
        private OperatorCalculatorBase _samplingRateCalculator;

        public Resample_OperatorCalculator_CubicRamses(OperatorCalculatorBase signalCalculator, OperatorCalculatorBase samplingRateCalculator)
        {
            if (signalCalculator == null) throw new NullException(() => signalCalculator);
            if (samplingRateCalculator == null) throw new NullException(() => samplingRateCalculator);

            _signalCalculator = signalCalculator;
            _samplingRateCalculator = samplingRateCalculator;
        }

        private double _xMinus1 = Double.MinValue;
        private double _x0 = -Double.Epsilon;
        private double _x1 = 0;
        private double _x2 = Double.Epsilon;
        private double _dx1 = Double.Epsilon;

        // Assume values begin at 0
        private double _yMinus1 = 0;
        private double _y0 = 0;
        private double _y1 = 0;
        private double _y2 = 0;

        public override double Calculate(double time, int channelIndex)
        {
            // TODO: What if times goes in reverse?
            // TODO: What if _x0 or _x1 are way off? How will it correct itself?
            double x = time;

            // When x goes past _x1 you must shift things.
            if (x >= _x1)
            {
                // Shift the samples to the left.
                _xMinus1 = _x0;
                _x0 = _x1;
                _x1 = _x2;
                _yMinus1 = _y0;
                _y0 = _y1;
                _y1 = _y2;

                // Determine next sample
                double samplingRate = GetSamplingRate(_x1, channelIndex);
                _dx1 = 1.0 / samplingRate;
                _x2 = _x1 + _dx1;
                _y2 = _signalCalculator.Calculate(_x2, channelIndex);
            }

            double y = Interpolator.Interpolate_Cubic_Ramses(
                _xMinus1, _x0, _x1, _x2,
                _yMinus1, _y0, _y1, _y2,
                x);

            return y;
        }

        /// <summary> Gets the sampling rate, converts it to an absolute number and ensures a minimum value. </summary>
        private double GetSamplingRate(double x, int channelIndex)
        {
            double samplingRate = _samplingRateCalculator.Calculate(x, channelIndex);

            samplingRate = Math.Abs(samplingRate);

            if (samplingRate < MINIMUM_SAMPLING_RATE)
            {
                samplingRate = MINIMUM_SAMPLING_RATE;
            }

            return samplingRate;
        }
    }
}
