using JJ.Framework.Reflection.Exceptions;
using System;

namespace JJ.Business.Synthesizer.Calculation.Operators
{
    /// <summary>
    /// This variation on the Interpolate_OperatorCalculator
    /// does give some sense of a filter, but when looking at the wave output,
    /// I see peaks, that I cannot explain, but my hunch it that it has to do
    /// with t catching up with t1 too quickly.
    /// </summary>
    internal class Interpolate_OperatorCalculator_LineRememberT0 : OperatorCalculatorBase_WithChildCalculators
    {
        private readonly OperatorCalculatorBase _signalCalculator;
        private readonly OperatorCalculatorBase _samplingRateCalculator;
        private readonly DimensionStack _dimensionStack;
        private readonly int _nextDimensionStackIndex;
        private readonly int _previousDimensionStackIndex;

        private double _x0;
        private double _y0;

        public Interpolate_OperatorCalculator_LineRememberT0(
            OperatorCalculatorBase signalCalculator,
            OperatorCalculatorBase samplingRateCalculator,
            DimensionStack dimensionStack)
            : base(new OperatorCalculatorBase[]
            {
                signalCalculator,
                samplingRateCalculator
            })
        {
            if (signalCalculator == null) throw new NullException(() => signalCalculator);
            if (signalCalculator is Number_OperatorCalculator) throw new InvalidTypeException<Number_OperatorCalculator>(() => signalCalculator);
            if (samplingRateCalculator == null) throw new NullException(() => samplingRateCalculator);
            // TODO: Interpolate with constant sampling rate does not have specialized calculators yet. Reactivate code line after those specialized calculators have been programmed.
            //if (samplingRateCalculator is Number_OperatorCalculator) throw new IsNotTypeException<Number_OperatorCalculator>(() => samplingRateCalculator);
            OperatorCalculatorHelper.AssertDimensionStack(dimensionStack);

            _signalCalculator = signalCalculator;
            _samplingRateCalculator = samplingRateCalculator;
            _dimensionStack = dimensionStack;
            _previousDimensionStackIndex = dimensionStack.CurrentIndex;
            _nextDimensionStackIndex = dimensionStack.CurrentIndex + 1;
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

            double samplingRate = _samplingRateCalculator.Calculate();
            if (samplingRate == 0)
            {
                // TODO: Set fields if sampling rate is 0?
                return 0;
            }
            double dx = 1.0 / samplingRate;

            double x1 = _x0 + dx;
            if (x >= x1)
            {
                _x0 = x1;

#if !USE_INVAR_INDICES
                _dimensionStack.Push(_x0);
#else
                _dimensionStack.Set(_nextDimensionStackIndex, _x0);
#endif
#if ASSERT_INVAR_INDICES
                OperatorCalculatorHelper.AssertStackIndex(_dimensionStack, _nextDimensionStackIndex);
#endif

                _y0 = _signalCalculator.Calculate();
#if !USE_INVAR_INDICES
                _dimensionStack.Pop();
#endif
                x1 = _x0 + dx;
            }
#if !USE_INVAR_INDICES
            _dimensionStack.Push(x1);
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
            double dy = y1 - _y0;
            double a = dy / dx;

            double y = _y0 + a * (x - _x0);
            return y;
        }
    }
}
