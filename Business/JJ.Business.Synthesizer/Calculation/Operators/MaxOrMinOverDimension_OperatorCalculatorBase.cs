using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;

namespace JJ.Business.Synthesizer.Calculation.Operators
{
    internal abstract class MaxOrMinOverDimension_OperatorCalculatorBase : OperatorCalculatorBase_WithChildCalculators
    {
        private readonly OperatorCalculatorBase _signalCalculator;
        private readonly OperatorCalculatorBase _fromCalculator;
        private readonly OperatorCalculatorBase _tillCalculator;
        private readonly OperatorCalculatorBase _stepCalculator;
        private readonly DimensionStack _dimensionStack;
        private readonly int _dimensionStackIndex;

        private double _aggregate;

        public MaxOrMinOverDimension_OperatorCalculatorBase(
            OperatorCalculatorBase signalCalculator,
            OperatorCalculatorBase fromCalculator,
            OperatorCalculatorBase tillCalculator,
            OperatorCalculatorBase stepCalculator,
            DimensionStack dimensionStack)
            : base(new OperatorCalculatorBase[] { signalCalculator, fromCalculator, tillCalculator, stepCalculator })
        {
            OperatorCalculatorHelper.AssertDimensionStack(dimensionStack);

            _signalCalculator = signalCalculator;
            _fromCalculator = fromCalculator;
            _tillCalculator = tillCalculator;
            _stepCalculator = stepCalculator;
            _dimensionStack = dimensionStack;
            _dimensionStackIndex = dimensionStack.CurrentIndex;

            ResetNonRecursive();
        }

        /// <summary> Just returns _aggregate. </summary>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public override double Calculate()
        {
            return _aggregate;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public sealed override void Reset()
        {
            base.Reset();

            ResetNonRecursive();
        }

        /// <summary> does nothing </summary>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        protected virtual void ResetNonRecursive()
        { }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        protected void RecalculateAggregate()
        {
            double from = _fromCalculator.Calculate();
            double till = _tillCalculator.Calculate();
            double step = _stepCalculator.Calculate();

            double length = till - from;
            bool isForward = length > 0.0;

            double position = from;

#if ASSERT_INVAR_INDICES
            OperatorCalculatorHelper.AssertStackIndex(_dimensionStack, _dimensionStackIndex);
#endif
#if !USE_INVAR_INDICES
            _dimensionStack.Set(position);
#else
            _dimensionStack.Set(_dimensionStackIndex, position);
#endif
            double currentValue = _signalCalculator.Calculate();
            position += step;

            // TODO: Prevent infinite loops.

            if (isForward)
            {
                while (position <= till)
                {
#if !USE_INVAR_INDICES
                    _dimensionStack.Set(position);
#else
                    _dimensionStack.Set(_dimensionStackIndex, position);
#endif
                    double newValue = _signalCalculator.Calculate();
                    bool mustOverwrite = MustOverwrite(currentValue, newValue);
                    if (mustOverwrite)
                    {
                        currentValue = newValue;
                    }
                    position += step;
                }
            }
            else
            {
                // Is backwards
                while (position >= till)
                {
#if !USE_INVAR_INDICES
                    _dimensionStack.Set(position);
#else
                    _dimensionStack.Set(_dimensionStackIndex, position);
#endif
                    double newValue = _signalCalculator.Calculate();
                    bool mustOverwrite = MustOverwrite(currentValue, newValue);
                    if (mustOverwrite)
                    {
                        currentValue = newValue;
                    }
                    position += step;
                }
            }

            _aggregate = currentValue;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        protected abstract bool MustOverwrite(double currentValue, double newValue);
    }
}
