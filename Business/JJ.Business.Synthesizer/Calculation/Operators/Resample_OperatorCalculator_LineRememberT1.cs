using JJ.Framework.Reflection.Exceptions;
using System;

namespace JJ.Business.Synthesizer.Calculation.Operators
{
    /// <summary>
    /// It seems to work, except for the artifacts that linear interpolation gives us.
    /// A weakness though is, that the sampling rate is remembered until the next sample,
    /// which may work poorly when a very low sampling rate is provided.
    /// </summary>
    internal class Resample_OperatorCalculator_LineRememberT1 : OperatorCalculatorBase_WithChildCalculators
    {
        private const double MINIMUM_SAMPLING_RATE = 0.01666666666666667; // Once a minute

        private OperatorCalculatorBase _signalCalculator;
        private OperatorCalculatorBase _samplingRateCalculator;
        private readonly DimensionStack _dimensionStack;
        private readonly int _nextDimensionStackIndex;
        private readonly int _previousDimensionStackIndex;

        private double _x0;
        private double _x1;
        private double _y0;
        private double _y1;
        private double _a;

        public Resample_OperatorCalculator_LineRememberT1(
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
            // TODO: Resample with constant sampling rate does not have specialized calculators yet. Reactivate code line after those specialized calculators have been programmed.
            //if (samplingRateCalculator is Number_OperatorCalculator) throw new IsNotTypeException<Number_OperatorCalculator>(() => samplingRateCalculator);
            OperatorCalculatorHelper.AssertDimensionStack(dimensionStack);

            _signalCalculator = signalCalculator;
            _samplingRateCalculator = samplingRateCalculator;
            _dimensionStack = dimensionStack;
            _previousDimensionStackIndex = dimensionStack.CurrentIndex;
            _nextDimensionStackIndex = dimensionStack.CurrentIndex + 1;

            ResetNonRecursive();
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
            if (x > _x1)
            {
                _x0 = _x1;
                _y0 = _y1;

#if !USE_INVAR_INDICES
                _dimensionStack.Push(_x1);
#else
                _dimensionStack.Set(_nextDimensionStackIndex, _x1);
#endif
#if ASSERT_INVAR_INDICES
                OperatorCalculatorHelper.AssertStackIndex(_dimensionStack, _nextDimensionStackIndex);
#endif

                double samplingRate = GetSamplingRate();
#if !USE_INVAR_INDICES
                _dimensionStack.Pop();
#endif
                if (samplingRate == 0) // Minimum samplingRate value might become variable in the near future, so could be 0.
                {
                    _a = 0;
                }
                else
                {
                    double dx = 1.0 / samplingRate;

                    _x1 += dx;

#if !USE_INVAR_INDICES
                    _dimensionStack.Push(_x1);
#else
                    _dimensionStack.Set(_nextDimensionStackIndex, _x1);
#endif
#if ASSERT_INVAR_INDICES
                    OperatorCalculatorHelper.AssertStackIndex(_dimensionStack, _nextDimensionStackIndex);
#endif

                    _y1 = _signalCalculator.Calculate();
#if !USE_INVAR_INDICES
                    _dimensionStack.Pop();
#endif
                    double dy = _y1 - _y0;
                    _a = dy / dx;
                }
            }
            else if (x < _x0)
            {
                // Going in reverse, take sample in reverse position.
                _x1 = _x0;
                _y1 = _y0;

#if !USE_INVAR_INDICES
                _dimensionStack.Push(_x0);
#else
                _dimensionStack.Set(_nextDimensionStackIndex, _x0);
#endif
#if ASSERT_INVAR_INDICES
                OperatorCalculatorHelper.AssertStackIndex(_dimensionStack, _nextDimensionStackIndex);
#endif

                double samplingRate = GetSamplingRate();
#if !USE_INVAR_INDICES
                _dimensionStack.Pop();
#endif
                if (samplingRate == 0)
                {
                    _a = 0;
                }
                else
                {
                    double dx = 1.0 / samplingRate;

                    _x0 -= dx;

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
                    double dy = _y1 - _y0;
                    _a = dy / dx;
                }
            }

            double y = _y0 + _a * (x - _x0);
            return y;
        }

        /// <summary>
        /// Gets the sampling rate, converts it to an absolute number
        /// and ensures a minimum value.
        /// </summary>
        private double GetSamplingRate()
        {
            // _x1 was recently (2015-08-22) corrected to x which might make going in reverse work better.
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
            ResetNonRecursive();

            base.Reset();
        }

        private void ResetNonRecursive()
        {
            _x0 = CalculationHelper.VERY_HIGH_VALUE;
            _x1 = CalculationHelper.VERY_LOW_VALUE;
            _y0 = 0.0;
            _y1 = 0.0;
            _a = 0.0;
        }
    }
}
