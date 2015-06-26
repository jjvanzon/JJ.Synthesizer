using JJ.Framework.Reflection.Exceptions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JJ.Business.Synthesizer.Calculation.Operators.Entities
{
    internal class Resample_WithConstSamplingRate_OperatorCalculator : OperatorCalculatorBase
    {
        // TODO: Low priority: Maybe control the offset with an inlet in the future? Or is it not useful enough?
        private const double TIME_OFFSET = 0;

        private OperatorCalculatorBase _signalCalculator;
        private double _samplePeriod;

        public Resample_WithConstSamplingRate_OperatorCalculator(OperatorCalculatorBase signalCalculator, double samplingRate)
        {
            if (signalCalculator == null) throw new NullException(() => signalCalculator);
            if (samplingRate <= 0) throw new LessThanOrEqualException(() => samplingRate, 0);

            _signalCalculator = signalCalculator;

            _samplePeriod = 1.0 / samplingRate;
        }

        public override double Calculate(double time, int channelIndex)
        {
            double remainder = time % _samplePeriod; // TODO: Wow, the compiler allows doubles in a modulo. But what does it do?

            double t0 = time - remainder;
            double t1 = t0 + _samplePeriod;
            double dt = t1 - t0;

            double x0 = _signalCalculator.Calculate(t0, channelIndex);
            double x1 = _signalCalculator.Calculate(t1, channelIndex);
            double dx = x1 - x0;

            double a = dx / dt;

            double x = x0 + a * (time - t0);

            return x;
        }
    }
}
