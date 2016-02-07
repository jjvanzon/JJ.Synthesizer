using JJ.Framework.Reflection.Exceptions;

namespace JJ.Business.Synthesizer.Calculation.Operators
{
    /// <summary> Not used. </summary>
    internal class Resample_OperatorCalculator_LineWithConstSamplingRate : OperatorCalculatorBase
    {
        private OperatorCalculatorBase _signalCalculator;
        private double _samplePeriod;

        public Resample_OperatorCalculator_LineWithConstSamplingRate(OperatorCalculatorBase signalCalculator, double samplingRate)
        {
            if (signalCalculator == null) throw new NullException(() => signalCalculator);
            if (samplingRate <= 0) throw new LessThanOrEqualException(() => samplingRate, 0);

            _signalCalculator = signalCalculator;

            _samplePeriod = 1.0 / samplingRate;
        }

        public override double Calculate(double time, int channelIndex)
        {
            double remainder = time % _samplePeriod;

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
