using JJ.Business.Synthesizer.Enums;
using JJ.Framework.Reflection.Exceptions;

namespace JJ.Business.Synthesizer.Calculation.Operators
{
    /// <summary> Not used. </summary>
    internal class Resample_OperatorCalculator_LineWithConstSamplingRate : OperatorCalculatorBase_WithChildCalculators
    {
        private readonly OperatorCalculatorBase _signalCalculator;
        private readonly double _sampleLength;
        private readonly DimensionStack _dimensionStack;
        private readonly int _currentDimensionStackIndex;
        private readonly int _previousDimensionStackIndex;

        public Resample_OperatorCalculator_LineWithConstSamplingRate(
            OperatorCalculatorBase signalCalculator, 
            double samplingRate,
            DimensionStack dimensionStack)
            : base(new OperatorCalculatorBase[]
            {
                signalCalculator
            })
        {
            if (signalCalculator == null) throw new NullException(() => signalCalculator);
            if (samplingRate <= 0) throw new LessThanOrEqualException(() => samplingRate, 0);
            if (dimensionStack == null) throw new NullException(() => dimensionStack);

            _signalCalculator = signalCalculator;
            _dimensionStack = dimensionStack;
            _currentDimensionStackIndex = dimensionStack.CurrentIndex;
            _previousDimensionStackIndex = dimensionStack.CurrentIndex - 1;
            _sampleLength = 1.0 / samplingRate;
        }

        public override double Calculate()
        {
            double x = _dimensionStack.Get(_previousDimensionStackIndex);

            double remainder = x % _sampleLength;

            double x0 = x - remainder;
            double x1 = x0 + _sampleLength;
            double dx = x1 - x0;

            _dimensionStack.Set(_currentDimensionStackIndex, x0);

            double y0 = _signalCalculator.Calculate();

            _dimensionStack.Set(_currentDimensionStackIndex, x1);

            double y1 = _signalCalculator.Calculate();

            double dy = y1 - y0;

            double a = dy / dx;

            double y = y0 + a * (x - x0);

            return y;
        }
    }
}
