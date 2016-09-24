using JJ.Business.Synthesizer.Enums;
using JJ.Framework.Reflection.Exceptions;

namespace JJ.Business.Synthesizer.Calculation.Operators
{
    /// <summary> Not used. </summary>
    internal class Interpolate_OperatorCalculator_Line_ConstSamplingRate : OperatorCalculatorBase_WithChildCalculators
    {
        private readonly OperatorCalculatorBase _signalCalculator;
        private readonly double _sampleLength;
        private readonly DimensionStack _dimensionStack;
        private readonly int _nextDimensionStackIndex;
        private readonly int _previousDimensionStackIndex;

        public Interpolate_OperatorCalculator_Line_ConstSamplingRate(
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
            _previousDimensionStackIndex = dimensionStack.CurrentIndex;
            _nextDimensionStackIndex = dimensionStack.CurrentIndex + 1;
            _sampleLength = 1.0 / samplingRate;
        }

        public override double Calculate()
        {
#if !USE_INVAR_INDICES
            double x = _dimensionStack.Get();
#else
            double x = _dimensionStack.Get(_previousDimensionStackIndex);
#endif
#if ASSERT_INVAR_INDICES
            OperatorCalculatorHelper.AssertStackIndex(_dimensionStack, _previousDimensionStackIndex);
#endif
            double remainder = x % _sampleLength;

            double x0 = x - remainder;
            double x1 = x0 + _sampleLength;
            double dx = x1 - x0;

#if !USE_INVAR_INDICES
            _dimensionStack.Push(x0);
#else
            _dimensionStack.Set(_nextDimensionStackIndex, x0);
#endif
#if ASSERT_INVAR_INDICES
            OperatorCalculatorHelper.AssertStackIndex(_dimensionStack, _nextDimensionStackIndex);
#endif
            double y0 = _signalCalculator.Calculate();

#if !USE_INVAR_INDICES
            _dimensionStack.Set(x1);
#else
            _dimensionStack.Set(_nextDimensionStackIndex, x1);
#endif
#if ASSERT_INVAR_INDICES
            OperatorCalculatorHelper.AssertStackIndex(_dimensionStack, _nextDimensionStackIndex);
#endif
            double y1 = _signalCalculator.Calculate();

#if !USE_INVAR_INDICES
            _dimensionStack.Pop();
#endif
            double dy = y1 - y0;

            double a = dy / dx;

            double y = y0 + a * (x - x0);

            return y;
        }
    }
}
