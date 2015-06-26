using JJ.Framework.Reflection.Exceptions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JJ.Business.Synthesizer.Calculation.Operators.Entities
{
    internal class Resample_OperatorCalculator : OperatorCalculatorBase
    {
        // TODO: Low priority: Maybe control the offset with an inlet in the future? Or is it not useful enough?
        private const double TIME_OFFSET = 0;

        private OperatorCalculatorBase _signalCalculator;
        private OperatorCalculatorBase _samplingRateCalculator;

        public Resample_OperatorCalculator(OperatorCalculatorBase signalCalculator, OperatorCalculatorBase samplingRateCalculator)
        {
            if (signalCalculator == null) throw new NullException(() => signalCalculator);
            if (samplingRateCalculator == null) throw new NullException(() => samplingRateCalculator);

            _signalCalculator = signalCalculator;
            _samplingRateCalculator = samplingRateCalculator;
        }

        public override double Calculate(double time, int channelIndex)
        {
            double samplingRate = _samplingRateCalculator.Calculate(time, channelIndex);
            double samplePeriod = 1.0 / samplingRate;

            double remainder = time % samplePeriod; // TODO: Wow, the compiler allows doubles in a modulo. But what does it do?

            double t0 = time - remainder;
            double t1 = t0 + samplePeriod;

            double x0 = _signalCalculator.Calculate(t0, channelIndex);
            double x1 = _signalCalculator.Calculate(t1, channelIndex);

            double x = x0 + (x1 - x0) * (time - t0);
            return x;
        }
    }
}
