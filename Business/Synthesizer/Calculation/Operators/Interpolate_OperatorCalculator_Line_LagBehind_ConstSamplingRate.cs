using System.Runtime.CompilerServices;
using JJ.Business.Synthesizer.Enums;
using JJ.Framework.Reflection.Exceptions;

namespace JJ.Business.Synthesizer.Calculation.Operators
{
    /// <summary> Not used. </summary>
    internal class Interpolate_OperatorCalculator_Line_LagBehind_ConstSamplingRate : OperatorCalculatorBase_WithChildCalculators
    {
        private readonly OperatorCalculatorBase _signalCalculator;
        private readonly DimensionStack _dimensionStack;
        private readonly int _nextDimensionStackIndex;
        private readonly int _previousDimensionStackIndex;
        private readonly double _dx;

        private double _x0;
        private double _x1;
        private double _y0;
        private double _y1;
        private double _a;

        public Interpolate_OperatorCalculator_Line_LagBehind_ConstSamplingRate(
            OperatorCalculatorBase signalCalculator,
            double samplingRate,
            DimensionStack dimensionStack)
            : base(new OperatorCalculatorBase[]
            {
                signalCalculator
            })
        {
            OperatorCalculatorHelper.AssertChildOperatorCalculator(signalCalculator, () => signalCalculator);
            if (samplingRate <= 0) throw new LessThanOrEqualException(() => samplingRate, 0);
            OperatorCalculatorHelper.AssertDimensionStack(dimensionStack);

            _signalCalculator = signalCalculator;
            _dimensionStack = dimensionStack;
            _previousDimensionStackIndex = dimensionStack.CurrentIndex;
            _nextDimensionStackIndex = dimensionStack.CurrentIndex + 1;
            _dx = 1.0 / samplingRate;

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
            if (x > _x1)
            {
                _x0 = _x1;
                _y0 = _y1;
                _x1 += _dx;

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
                _a = dy / _dx;
            }
            else if (x < _x0)
            {
                // Going in reverse, take sample in reverse position.
                _x1 = _x0;
                _y1 = _y0;
                _x0 -= _dx;

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
                _a = dy / _dx;
            }

            double y = _y0 + _a * (x - _x0);

            return y;
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

            _x0 = x - _dx;
            _x1 = x;

            // Y's are just set at a slightly more practical default than 0.
            _y0 = y;
            _y1 = y;

            _a = 0.0;
        }
    }
}
