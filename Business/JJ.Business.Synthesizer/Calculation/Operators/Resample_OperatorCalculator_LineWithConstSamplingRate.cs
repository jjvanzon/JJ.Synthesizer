using JJ.Business.Synthesizer.Enums;
using JJ.Framework.Reflection.Exceptions;

namespace JJ.Business.Synthesizer.Calculation.Operators
{
    /// <summary> Not used. </summary>
    internal class Resample_OperatorCalculator_LineWithConstSamplingRate : OperatorCalculatorBase_WithChildCalculators
    {
        private readonly OperatorCalculatorBase _signalCalculator;
        private readonly double _sampleLength;
        private readonly int _dimensionIndex;

        public Resample_OperatorCalculator_LineWithConstSamplingRate(
            OperatorCalculatorBase signalCalculator, 
            double samplingRate,
            DimensionEnum dimensionEnum)
            : base(new OperatorCalculatorBase[]
            {
                signalCalculator
            })
        {
            if (signalCalculator == null) throw new NullException(() => signalCalculator);
            if (samplingRate <= 0) throw new LessThanOrEqualException(() => samplingRate, 0);

            _signalCalculator = signalCalculator;
            _dimensionIndex = (int)dimensionEnum;

            _sampleLength = 1.0 / samplingRate;
        }

        public override double Calculate(DimensionStack dimensionStack)
        {
            double x = dimensionStack.Get(_dimensionIndex);

            double remainder = x % _sampleLength;

            double x0 = x - remainder;
            double x1 = x0 + _sampleLength;
            double dx = x1 - x0;

            dimensionStack.Push(_dimensionIndex, x0);
            double y0 = _signalCalculator.Calculate(dimensionStack);
            dimensionStack.Pop(_dimensionIndex);

            dimensionStack.Push(_dimensionIndex, x1);
            double y1 = _signalCalculator.Calculate(dimensionStack);
            dimensionStack.Pop(_dimensionIndex);

            double dy = y1 - y0;

            double a = dy / dx;

            double y = y0 + a * (x - x0);

            return y;
        }
    }
}
