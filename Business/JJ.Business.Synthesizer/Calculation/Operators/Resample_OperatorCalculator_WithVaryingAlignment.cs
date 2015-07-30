using JJ.Framework.Reflection.Exceptions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JJ.Business.Synthesizer.Calculation.Operators
{
    /// <summary>
    /// This variation on the Resample_OperatorCalculator
    /// does not work when the sampling rate gradually changes,
    /// because the alignment of sampling changes with the gradual change.
    /// </summary>
    internal class Resample_OperatorCalculator_WithVaryingAlignment : OperatorCalculatorBase
    {
        private OperatorCalculatorBase _signalCalculator;
        private OperatorCalculatorBase _samplingRateCalculator;

        public Resample_OperatorCalculator_WithVaryingAlignment(OperatorCalculatorBase signalCalculator, OperatorCalculatorBase samplingRateCalculator)
        {
            if (signalCalculator == null) throw new NullException(() => signalCalculator);
            if (samplingRateCalculator == null) throw new NullException(() => samplingRateCalculator);
            if (samplingRateCalculator is Value_OperatorCalculator) throw new Exception("samplingRateCalculator cannot be a Value_OperatorCalculator.");

            _signalCalculator = signalCalculator;
            _samplingRateCalculator = samplingRateCalculator;
        }

        public override double Calculate(double time, int channelIndex)
        {
            double samplingRate = _samplingRateCalculator.Calculate(time, channelIndex);

            double samplePeriod = 1.0 / samplingRate;

            double remainder = time % samplePeriod;

            double t0 = time - remainder;
            double t1 = t0 + samplePeriod;
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
