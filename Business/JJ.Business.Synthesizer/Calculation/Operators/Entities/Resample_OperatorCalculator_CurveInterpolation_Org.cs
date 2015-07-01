using JJ.Framework.Reflection.Exceptions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JJ.Business.Synthesizer.Calculation.Operators.Entities
{
    /// <summary>
    /// This is the currently used variation on the Resample_OperatorCalculator.
    /// It seems to work, except for the artifacts that linear interpolation gives us.
    /// A weakness though is, that the sampling rate is remembered until the next sample,
    /// which may work poorly when a very low sampling rate is provided.
    /// </summary>
    internal class Resample_OperatorCalculator_CurveInterpolation_Org : OperatorCalculatorBase
    {
        private double MINIMUM_SAMPLING_RATE = 1.0 / 8.0; // 8 Hz.

        private OperatorCalculatorBase _signalCalculator;
        private OperatorCalculatorBase _samplingRateCalculator;

        public Resample_OperatorCalculator_CurveInterpolation_Org(OperatorCalculatorBase signalCalculator, OperatorCalculatorBase samplingRateCalculator)
        {
            if (signalCalculator == null) throw new NullException(() => signalCalculator);
            if (samplingRateCalculator == null) throw new NullException(() => samplingRateCalculator);
            // TODO: Uncomment if the specialized calculator is up-to-date.
            //if (samplingRateCalculator is Value_OperatorCalculator) throw new Exception("samplingRateCalculator cannot be a Value_OperatorCalculator.");

            _signalCalculator = signalCalculator;
            _samplingRateCalculator = samplingRateCalculator;
        }

        // HACK: These defaults are hacks that are meaningless in practice.
        private double _xMinus1 = -0.2;
        private double _x0 = 0;
        private double _x1 = 0.2;
        private double _x2 = 0.4;

        private double _yMinus1 = 0;
        private double _y0 = 0;
        private double _y1 = 12000;
        private double _y2 = -24000;

        private double _dx0 = 0.2;
        private double _dx1 = 0.2;
        private double _a0;
        private double _a1;

        public override double Calculate(double time, int channelIndex)
        {
            double x = time;
            if (x > _x1)
            {
                _xMinus1 = _x0;
                _x0 = _x1;
                _x1 = _x2;

                _yMinus1 = _y0;
                _y0 = _y1;
                _y1 = _y2;

                _dx0 = _dx1;
                _a0 = _a1;

                double samplingRate1 = GetSamplingRate(_x1, channelIndex);
                // TODO: Handle SamplingRate 0.

                _dx1 = 1.0 / samplingRate1;
                _x2 += _dx1;
                _y2 = _signalCalculator.Calculate(_x2, channelIndex);

                _a1 = (_y2 - _y0) / (_x2 - _x0);

                if (Double.IsNaN(_a1))
                {
                    _a1 = 0;
                }
            }

            // TODO: What if _t1 exceeds _t2 already? What happens then?
            double dx = x - _x0;
            double t = 0;
            if (_dx0 != 0)
            {
                t = dx / _dx0;
            }

            // TODO: Figure out how to prevent tau from becoming out of range.
            if (t > 1.0)
            {
                return 0;
            }
            else if (t < 0.0)
            {
                return 0;
            }

            double y = (1.0 - t) * (_y0 + _a0 * (x - _x0)) + t * (_y1 + _a1 * (x - _x1));
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
