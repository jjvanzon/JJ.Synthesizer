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
    internal class Resample_OperatorCalculator_LinearInterpolation_RememberingT1_Org : OperatorCalculatorBase
    {
        private OperatorCalculatorBase _signalCalculator;
        private OperatorCalculatorBase _samplingRateCalculator;

        public Resample_OperatorCalculator_LinearInterpolation_RememberingT1_Org(OperatorCalculatorBase signalCalculator, OperatorCalculatorBase samplingRateCalculator)
        {
            if (signalCalculator == null) throw new NullException(() => signalCalculator);
            if (samplingRateCalculator == null) throw new NullException(() => samplingRateCalculator);
            if (samplingRateCalculator is Value_OperatorCalculator) throw new Exception("samplingRateCalculator cannot be a Value_OperatorCalculator.");

            _signalCalculator = signalCalculator;
            _samplingRateCalculator = samplingRateCalculator;
        }

        private double _t0;
        private double _t1;
        private double _x0;
        private double _x1;
        private double _a;

        public override double Calculate(double t, int channelIndex)
        {
            if (t >= _t1)
            {
                _t0 = _t1;
                _x0 = _x1;

                double samplingRate = _samplingRateCalculator.Calculate(_t1, channelIndex);
                if (samplingRate == 0)
                {
                    _a = 0;
                }
                else
                {
                    double dt = 1.0 / samplingRate;

                    _t1 += dt;
                    _x1 = _signalCalculator.Calculate(_t1, channelIndex);
                    double dx = _x1 - _x0;

                    _a = dx / dt;
                }
            }

            double x = _x0 + _a * (t - _t0);
            return x;
        }
    }
}
