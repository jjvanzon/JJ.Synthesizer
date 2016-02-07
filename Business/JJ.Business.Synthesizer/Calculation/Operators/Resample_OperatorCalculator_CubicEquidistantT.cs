using JJ.Framework.Mathematics;
using JJ.Framework.Reflection.Exceptions;
using System;

namespace JJ.Business.Synthesizer.Calculation.Operators
{
    /// <summary>
    /// A weakness though is, that the sampling rate is remembered until the next sample,
    /// which may work poorly when a very low sampling rate is provided.
    /// </summary>
    internal class Resample_OperatorCalculator_CubicEquidistantT : OperatorCalculatorBase
    {
        private double MINIMUM_SAMPLING_RATE = 16.0; // 8 Hz.

        private OperatorCalculatorBase _signalCalculator;
        private OperatorCalculatorBase _samplingRateCalculator;

        public Resample_OperatorCalculator_CubicEquidistantT(OperatorCalculatorBase signalCalculator, OperatorCalculatorBase samplingRateCalculator)
        {
            if (signalCalculator == null) throw new NullException(() => signalCalculator);
            if (signalCalculator is Number_OperatorCalculator) throw new IsNotTypeException<Number_OperatorCalculator>(() => signalCalculator);
            if (samplingRateCalculator == null) throw new NullException(() => samplingRateCalculator);
            // TODO: Resample with constant sampling rate does not have specialized calculators yet. Reactivate code line after those specialized calculators have been programmed.
            //if (samplingRateCalculator is Number_OperatorCalculator) throw new IsNotTypeException<Number_OperatorCalculator>(() => samplingRateCalculator);

            _signalCalculator = signalCalculator;
            _samplingRateCalculator = samplingRateCalculator;
        }

        // TODO: These are meaningless defaults.
        private double _x0 = 0.0;
        private double _x1 = 0.2;
        private double _dx = 0.2; 
        private double _yMinus1 = 0.0;
        private double _y0 = 0.0;
        private double _y1 = 12000.0;
        private double _y2 = -24000.0;

        public override double Calculate(double time, int channelIndex)
        {
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

            double y = Interpolator.Interpolate_Cubic_Equidistant_SlightlyBetterThanLinear(_yMinus1, _y0, _y1, _y2, t);
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
