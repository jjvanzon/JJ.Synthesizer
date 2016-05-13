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
        private readonly DimensionStack _dimensionStack;

        public Resample_OperatorCalculator_LineWithConstSamplingRate(
            OperatorCalculatorBase signalCalculator, 
            double samplingRate,
            DimensionEnum dimensionEnum,
            DimensionStack dimensionStack)
            : base(new OperatorCalculatorBase[]
            {
                signalCalculator
            })
        {
            if (signalCalculator == null) throw new NullException(() => signalCalculator);
            if (samplingRate <= 0) throw new LessThanOrEqualException(() => samplingRate, 0);
            OperatorCalculatorHelper.AssertDimensionEnum(dimensionEnum);
            if (dimensionStack == null) throw new NullException(() => dimensionStack);

            _signalCalculator = signalCalculator;
            _dimensionIndex = (int)dimensionEnum;
            _dimensionStack = dimensionStack;

            _sampleLength = 1.0 / samplingRate;
        }

        public override double Calculate()
        {
            double x = _dimensionStack.Get(_dimensionIndex);

            double remainder = x % _sampleLength;

            double x0 = x - remainder;
            double x1 = x0 + _sampleLength;
            double dx = x1 - x0;

            _dimensionStack.Push(_dimensionIndex, x0);

            double y0 = _signalCalculator.Calculate();

            _dimensionStack.Set(_dimensionIndex, x1);

            double y1 = _signalCalculator.Calculate();

            _dimensionStack.Pop(_dimensionIndex);

            double dy = y1 - y0;

            double a = dy / dx;

            double y = y0 + a * (x - x0);

            return y;
        }
    }
}
