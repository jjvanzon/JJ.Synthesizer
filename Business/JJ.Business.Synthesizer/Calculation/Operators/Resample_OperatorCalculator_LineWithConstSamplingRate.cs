using JJ.Business.Synthesizer.Enums;
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

        public override double Calculate(DimensionStack dimensionStack)
        {
            double time = dimensionStack.Get(DimensionEnum.Time);

            double remainder = time % _samplePeriod;

            double t0 = time - remainder;
            double t1 = t0 + _samplePeriod;
            double dt = t1 - t0;

            dimensionStack.Push(DimensionEnum.Time, t0);
            double x0 = _signalCalculator.Calculate(dimensionStack);
            dimensionStack.Pop(DimensionEnum.Time);

            dimensionStack.Push(DimensionEnum.Time, t1);
            double x1 = _signalCalculator.Calculate(dimensionStack);
            dimensionStack.Pop(DimensionEnum.Time);

            double dx = x1 - x0;

            double a = dx / dt;

            double x = x0 + a * (time - t0);

            return x;
        }
    }
}
