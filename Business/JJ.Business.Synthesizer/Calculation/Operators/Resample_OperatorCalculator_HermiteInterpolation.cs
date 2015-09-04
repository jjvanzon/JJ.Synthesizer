using JJ.Framework.Mathematics;
using JJ.Framework.Reflection.Exceptions;
using System;

namespace JJ.Business.Synthesizer.Calculation.Operators
{
    /// <summary>
    /// This is the currently used variation on the Resample_OperatorCalculator.
    /// </summary>
    internal class Resample_OperatorCalculator_HermiteInterpolation : OperatorCalculatorBase
    {
        private double MINIMUM_SAMPLING_RATE = 1.0 / 8.0; // 8 Hz.

        private OperatorCalculatorBase _signalCalculator;
        private OperatorCalculatorBase _samplingRateCalculator;

        public Resample_OperatorCalculator_HermiteInterpolation(OperatorCalculatorBase signalCalculator, OperatorCalculatorBase samplingRateCalculator)
        {
            if (signalCalculator == null) throw new NullException(() => signalCalculator);
            if (samplingRateCalculator == null) throw new NullException(() => samplingRateCalculator);
            // TODO: Uncomment if the specialized calculator is up-to-date.
            //if (samplingRateCalculator is Value_OperatorCalculator) throw new Exception("samplingRateCalculator cannot be a Value_OperatorCalculator.");

            _signalCalculator = signalCalculator;
            _samplingRateCalculator = samplingRateCalculator;
        }

        //// TODO: These are meaningless defaults.
        //private double _x0 = 0.0;
        //private double _x1 = 0.2;
        //private double _dx = 0.2; 
        //private double _yMinus1 = 0.0;
        //private double _y0 = 0.0;
        //private double _y1 = 12000.0;
        //private double _y2 = -24000.0;

        private double _x0;
        private double _x1;
        private double _dx = Double.Epsilon;
        private double _yMinus1;
        private double _y0;
        private double _y1;
        private double _y2;

        public override double Calculate(double time, int channelIndex)
        {
            // TODO: What if times goes in reverse?
            // TODO: What if _x0 or _x1 are way off? How will it correct itself?
            double x = time;
            if (x > _x1)
            {
                double samplingRate = GetSamplingRate(_x1, channelIndex);
                _dx = 1.0 / samplingRate;
                _x1 += _dx;

                // x'es must be equidistant for the interpolation we use.
                _x0 = _x1 - _dx;
                double xMinus1 = _x0 - _dx;
                double x2 = _x1 + _dx;

                _yMinus1 = _signalCalculator.Calculate(xMinus1, channelIndex);
                _y0 = _signalCalculator.Calculate(_x0, channelIndex);
                _y1 = _signalCalculator.Calculate(_x1, channelIndex);
                _y2 = _signalCalculator.Calculate(x2, channelIndex);
            }

            double t = (x - _x0) / _dx;

            double y = Interpolator.Interpolate_Hermite_4pt3oX(_yMinus1, _y0, _y1, _y2, t);
            return y;
        }

        /// <summary>
        /// Gets the sampling rate, converts it to an absolute number
        /// and ensures a minimum value.
        /// </summary>
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
