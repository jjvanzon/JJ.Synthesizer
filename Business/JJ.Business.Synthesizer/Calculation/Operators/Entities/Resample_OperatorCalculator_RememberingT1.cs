using JJ.Framework.Reflection.Exceptions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JJ.Business.Synthesizer.Calculation.Operators.Entities
{
    internal class Resample_OperatorCalculator_RememberingT1 : OperatorCalculatorBase
    {
        private OperatorCalculatorBase _signalCalculator;
        private OperatorCalculatorBase _samplingRateCalculator;

        public Resample_OperatorCalculator_RememberingT1(OperatorCalculatorBase signalCalculator, OperatorCalculatorBase samplingRateCalculator)
        {
            if (signalCalculator == null) throw new NullException(() => signalCalculator);
            if (samplingRateCalculator == null) throw new NullException(() => samplingRateCalculator);
            if (samplingRateCalculator is Value_OperatorCalculator) throw new Exception("samplingRateCalculator cannot be a Value_OperatorCalculator.");

            _signalCalculator = signalCalculator;
            _samplingRateCalculator = samplingRateCalculator;
        }

        private double _t0;
        private double _x0;
        private double _t1;
        private double _a;

        public override double Calculate(double t, int channelIndex)
        {
            if (t >= _t1)
            {
                double samplingRate = _samplingRateCalculator.Calculate(_t1, channelIndex);
                if (samplingRate == 0)
                {
                    _t0 = t;
                    _x0 = 0;
                    _t1 = t;
                    _a = 0;
                }
                double dt = 1.0 / samplingRate;

                _t0 = _t1;
                _t1 += dt;

                _x0 = _signalCalculator.Calculate(_t0, channelIndex);
                double x1 = _signalCalculator.Calculate(_t1, channelIndex);
                double dx = x1 - _x0;

                _a = dx / dt;
            }

            double x = _x0 + _a * (t - _t0);
            return x;
        }
    }
}
