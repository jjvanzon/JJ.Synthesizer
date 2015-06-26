using JJ.Framework.Reflection.Exceptions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JJ.Business.Synthesizer.Calculation.Operators.Entities
{
    internal class Resample_OperatorCalculator_RememberingT0 : OperatorCalculatorBase
    {
        private OperatorCalculatorBase _signalCalculator;
        private OperatorCalculatorBase _samplingRateCalculator;

        public Resample_OperatorCalculator_RememberingT0(OperatorCalculatorBase signalCalculator, OperatorCalculatorBase samplingRateCalculator)
        {
            if (signalCalculator == null) throw new NullException(() => signalCalculator);
            if (samplingRateCalculator == null) throw new NullException(() => samplingRateCalculator);
            if (samplingRateCalculator is Value_OperatorCalculator) throw new Exception("samplingRateCalculator cannot be a Value_OperatorCalculator.");

            _signalCalculator = signalCalculator;
            _samplingRateCalculator = samplingRateCalculator;
        }

        private double _t0;
        private double _x0;

        public override double Calculate(double t, int channelIndex)
        {
            double samplingRate = _samplingRateCalculator.Calculate(t, channelIndex);
            // TODO: Set fields if sampling rate is 0.
            if (samplingRate == 0) return 0;
            double samplePeriod = 1.0 / samplingRate;

            double t1 = _t0 + samplePeriod;
            if (t >= t1)
            {
                _t0 = t1;
                _x0 = _signalCalculator.Calculate(_t0, channelIndex);
                t1 = _t0 + samplePeriod;
            }

            double x1 = _signalCalculator.Calculate(t1, channelIndex);

            double dt = t1 - _t0;
            double dx = x1 - _x0;
            double a = dx / dt;

            double x = _x0 + a * (t - _t0);
            return x;
        }
    }
}
