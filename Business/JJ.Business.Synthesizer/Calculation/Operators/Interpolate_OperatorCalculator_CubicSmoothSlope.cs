using JJ.Framework.Reflection.Exceptions;
using System;
using JJ.Business.Synthesizer.CopiedCode.FromFramework;
using System.Runtime.CompilerServices;

namespace JJ.Business.Synthesizer.Calculation.Operators
{
    internal class Interpolate_OperatorCalculator_CubicSmoothSlope : OperatorCalculatorBase_WithChildCalculators
    {
        private const double MINIMUM_SAMPLING_RATE = 1.0 / 60.0; // Once a minute

        private readonly OperatorCalculatorBase _signalCalculator;
        private readonly OperatorCalculatorBase _samplingRateCalculator;
        private readonly DimensionStack _dimensionStack;
        private readonly int _nextDimensionStackIndex;
        private readonly int _previousDimensionStackIndex;

        private double _xMinus1;
        private double _x0;
        private double _x1;
        private double _x2;
        private double _dx1;
        private double _yMinus1;
        private double _y0;
        private double _y1;
        private double _y2;

        public Interpolate_OperatorCalculator_CubicSmoothSlope(
            OperatorCalculatorBase signalCalculator,
            OperatorCalculatorBase samplingRateCalculator,
            DimensionStack dimensionStack)
            : base(new OperatorCalculatorBase[] { signalCalculator, samplingRateCalculator })
        {
            OperatorCalculatorHelper.AssertChildOperatorCalculator(signalCalculator, () => signalCalculator);
            if (samplingRateCalculator == null) throw new NullException(() => samplingRateCalculator);
            // TODO: Interpolate with constant sampling rate does not have specialized calculators yet. Reactivate code line after those specialized calculators have been programmed.
            //if (samplingRateCalculator is Number_OperatorCalculator) throw new IsNotTypeException<Number_OperatorCalculator>(() => samplingRateCalculator);
            OperatorCalculatorHelper.AssertDimensionStack(dimensionStack);

            _signalCalculator = signalCalculator;
            _samplingRateCalculator = samplingRateCalculator;
            _dimensionStack = dimensionStack;
            _previousDimensionStackIndex = dimensionStack.CurrentIndex;
            _nextDimensionStackIndex = dimensionStack.CurrentIndex + 1;

            ResetNonRecursive();
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
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
            // TODO: What if position goes in reverse?
            // TODO: What if _x0 or _x1 are way off? How will it correct itself?
            // When x goes past _x1 you must shift things.
            if (x >= _x1)
            {
                // Shift the samples to the left.
                _xMinus1 = _x0;
                _x0 = _x1;
                _x1 = _x2;
                _yMinus1 = _y0;
                _y0 = _y1;
                _y1 = _y2;

                // Determine next sample
#if !USE_INVAR_INDICES
                _dimensionStack.Push(_x1);
#else
                _dimensionStack.Set(_nextDimensionStackIndex, _x1);
#endif
#if ASSERT_INVAR_INDICES
                OperatorCalculatorHelper.AssertStackIndex(_dimensionStack, _nextDimensionStackIndex);
#endif
                double samplingRate1 = GetSamplingRate();

                _dx1 = 1.0 / samplingRate1;
                _x2 = _x1 + _dx1;

#if !USE_INVAR_INDICES
                _dimensionStack.Set(_x2);
#else
                _dimensionStack.Set(_nextDimensionStackIndex, _x2);
#endif
#if ASSERT_INVAR_INDICES
                OperatorCalculatorHelper.AssertStackIndex(_dimensionStack, _nextDimensionStackIndex);
#endif
                _y2 = _signalCalculator.Calculate();

#if !USE_INVAR_INDICES
                _dimensionStack.Pop();
#endif
            }

            double y = Interpolator.Interpolate_Cubic_SmoothSlope(
                _xMinus1, _x0, _x1, _x2,
                _yMinus1, _y0, _y1, _y2,
                x);

            return y;
        }

        /// <summary> Gets the sampling rate, converts it to an absolute number and ensures a minimum value. </summary>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        private double GetSamplingRate()
        {
            double samplingRate = _samplingRateCalculator.Calculate();

            samplingRate = Math.Abs(samplingRate);

            if (samplingRate < MINIMUM_SAMPLING_RATE)
            {
                samplingRate = MINIMUM_SAMPLING_RATE;
            }

            return samplingRate;
        }

        public override void Reset()
        {
            base.Reset();

            ResetNonRecursive();
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        private void ResetNonRecursive()
        {
#if !USE_INVAR_INDICES
            double x = _dimensionStack.Get();
#else
            double x = _dimensionStack.Get(_previousDimensionStackIndex);
#endif
#if ASSERT_INVAR_INDICES
            OperatorCalculatorHelper.AssertStackIndex(_dimensionStack, _previousDimensionStackIndex);
#endif
            double y = _signalCalculator.Calculate();

            _xMinus1 = CalculationHelper.VERY_LOW_VALUE;
            _x0 = x - CalculationHelper.VERY_SMALL_POSITIVE_VALUE;
            _x1 = x;
            _x2 = x + CalculationHelper.VERY_SMALL_POSITIVE_VALUE;
            _dx1 = CalculationHelper.VERY_SMALL_POSITIVE_VALUE;

            _yMinus1 = y;
            _y0 = y;
            _y1 = y;
            _y2 = y;
        }
    }
}
