using JJ.Framework.Mathematics;
using JJ.Framework.Reflection.Exceptions;
using System;

namespace JJ.Business.Synthesizer.Calculation.Operators
{
    /// <summary>
    /// A weakness though is, that the sampling rate is remembered until the next sample,
    /// which may work poorly when a very low sampling rate is provided.
    /// </summary>
    internal class Resample_OperatorCalculator_CubicEquidistant : OperatorCalculatorBase_WithChildCalculators
    {
        private const double MINIMUM_SAMPLING_RATE = 0.01666666666666667; // Once a minute

        private readonly OperatorCalculatorBase _signalCalculator;
        private readonly OperatorCalculatorBase _samplingRateCalculator;
        private readonly DimensionStack _dimensionStack;
        private readonly int _nextDimensionStackIndex;
        private readonly int _previousDimensionStackIndex;

        public Resample_OperatorCalculator_CubicEquidistant(
            OperatorCalculatorBase signalCalculator,
            OperatorCalculatorBase samplingRateCalculator,
            DimensionStack dimensionStack)
            : base(new OperatorCalculatorBase[]
            {
                signalCalculator,
                samplingRateCalculator
            })
        {
            OperatorCalculatorHelper.AssertOperatorCalculatorBase(signalCalculator, () => signalCalculator);
            if (samplingRateCalculator == null) throw new NullException(() => samplingRateCalculator);
            // TODO: Resample with constant sampling rate does not have specialized calculators yet. Reactivate code line after those specialized calculators have been programmed.
            //if (samplingRateCalculator is Number_OperatorCalculator) throw new IsNotTypeException<Number_OperatorCalculator>(() => samplingRateCalculator);
            OperatorCalculatorHelper.AssertDimensionStack_ForWriters(dimensionStack);

            _signalCalculator = signalCalculator;
            _samplingRateCalculator = samplingRateCalculator;
            _dimensionStack = dimensionStack;
            _previousDimensionStackIndex = dimensionStack.CurrentIndex;
            _nextDimensionStackIndex = dimensionStack.CurrentIndex + 1;
        }

        // TODO: These are meaningless defaults.
        private double _x0 = 0.0;
        private double _x1 = 0.2;
        private double _dx = 0.2;
        private double _yMinus1 = 0.0;
        private double _y0 = 0.0;
        private double _y1 = 12000.0;
        private double _y2 = -24000.0;

        public override double Calculate()
        {
#if !USE_INVAR_INDICES
            double position = _dimensionStack.Get();
#else
            double position = _dimensionStack.Get(_previousDimensionStackIndex);
#endif
#if ASSERT_INVAR_INDICES
            OperatorCalculatorHelper.AssertStackIndex(_dimensionStack, _previousDimensionStackIndex);
#endif

            double x = position;
            if (x > _x1)
            {
#if !USE_INVAR_INDICES
                _dimensionStack.Push(_x1);
#else
                _dimensionStack.Set(_nextDimensionStackIndex, _x1);
#endif
#if ASSERT_INVAR_INDICES
                OperatorCalculatorHelper.AssertStackIndex(_dimensionStack, _nextDimensionStackIndex);
#endif

                double samplingRate = GetSamplingRate();

                _dx = 1.0 / samplingRate;
                _x1 += _dx;

                // x'es must be equidistant for the interpolation we use.
                _x0 = _x1 - _dx;
                double xMinus1 = _x0 - _dx;
                double x2 = _x1 + _dx;

#if !USE_INVAR_INDICES
                _dimensionStack.Set(xMinus1);
#else
                _dimensionStack.Set(_nextDimensionStackIndex, xMinus1);
#endif
#if ASSERT_INVAR_INDICES
                OperatorCalculatorHelper.AssertStackIndex(_dimensionStack, _nextDimensionStackIndex);
#endif

                _yMinus1 = _signalCalculator.Calculate();

#if !USE_INVAR_INDICES
                _dimensionStack.Set(_x0);
#else
                _dimensionStack.Set(_nextDimensionStackIndex, _x0);
#endif
#if ASSERT_INVAR_INDICES
                OperatorCalculatorHelper.AssertStackIndex(_dimensionStack, _nextDimensionStackIndex);
#endif

                _y0 = _signalCalculator.Calculate();

#if !USE_INVAR_INDICES
                _dimensionStack.Set(_x1);
#else
                _dimensionStack.Set(_nextDimensionStackIndex, _x1);
#endif
#if ASSERT_INVAR_INDICES
                OperatorCalculatorHelper.AssertStackIndex(_dimensionStack, _nextDimensionStackIndex);
#endif

                _y1 = _signalCalculator.Calculate();

#if !USE_INVAR_INDICES
                _dimensionStack.Set(x2);
#else
                _dimensionStack.Set(_nextDimensionStackIndex, x2);
#endif
#if ASSERT_INVAR_INDICES
                OperatorCalculatorHelper.AssertStackIndex(_dimensionStack, _nextDimensionStackIndex);
#endif

                _y2 = _signalCalculator.Calculate();

#if !USE_INVAR_INDICES
                _dimensionStack.Pop();
#endif
            }

            double t = (x - _x0) / _dx;

            double y = Interpolator.Interpolate_Cubic_Equidistant(_yMinus1, _y0, _y1, _y2, t);
            return y;
        }

        /// <summary> Gets the sampling rate, converts it to an absolute number and ensures a minimum value. </summary>
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
    }
}
