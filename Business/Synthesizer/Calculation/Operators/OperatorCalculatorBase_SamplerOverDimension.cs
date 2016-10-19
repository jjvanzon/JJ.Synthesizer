using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;

namespace JJ.Business.Synthesizer.Calculation.Operators
{
    internal abstract class OperatorCalculatorBase_SamplerOverDimension
        : OperatorCalculatorBase_WithChildCalculators
    {
        protected readonly OperatorCalculatorBase _collectionCalculator;
        protected readonly OperatorCalculatorBase _fromCalculator;
        protected readonly OperatorCalculatorBase _tillCalculator;
        protected readonly OperatorCalculatorBase _stepCalculator;
        protected readonly DimensionStack _dimensionStack;
        protected readonly int _dimensionStackIndex;

        protected double _step;
        protected double _length;

        public OperatorCalculatorBase_SamplerOverDimension(
            OperatorCalculatorBase collectionCalculator,
            OperatorCalculatorBase fromCalculator,
            OperatorCalculatorBase tillCalculator,
            OperatorCalculatorBase stepCalculator,
            DimensionStack dimensionStack)
            : base(new OperatorCalculatorBase[]
            {
                collectionCalculator,
                fromCalculator,
                tillCalculator,
                stepCalculator
            })
        {
            OperatorCalculatorHelper.AssertDimensionStack(dimensionStack);

            _collectionCalculator = collectionCalculator;
            _fromCalculator = fromCalculator;
            _tillCalculator = tillCalculator;
            _stepCalculator = stepCalculator;
            _dimensionStack = dimensionStack;
            _dimensionStackIndex = dimensionStack.CurrentIndex;

            ResetNonRecursive();
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public sealed override void Reset()
        {
            base.Reset();

            ResetNonRecursive();
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        protected virtual void ProcessFirstSample(double sample)
        { }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        protected virtual void ProcessNextSample(double sample)
        { }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        protected virtual void FinalizeSampling()
        { }

        /// <summary> does nothing </summary>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        protected virtual void ResetNonRecursive()
        { }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        protected virtual void RecalculateCollection()
        {
#if !USE_INVAR_INDICES
            double originalPosition = _dimensionStack.Get();
#else
            double originalPosition = _dimensionStack.Get(_dimensionStackIndex);
#endif
#if ASSERT_INVAR_INDICES
            OperatorCalculatorHelper.AssertStackIndex(_dimensionStack, _dimensionStackIndex);
#endif
            double from = _fromCalculator.Calculate();
            double till = _tillCalculator.Calculate();
            _step = _stepCalculator.Calculate();

            _length = till - from;
            bool isForward = _length >= 0.0;

            double position = from;

#if ASSERT_INVAR_INDICES
            OperatorCalculatorHelper.AssertStackIndex(_dimensionStack, _dimensionStackIndex);
#endif
#if !USE_INVAR_INDICES
            _dimensionStack.Set(from);
#else
            _dimensionStack.Set(_dimensionStackIndex, position);
#endif
            double sample = _collectionCalculator.Calculate();

            ProcessFirstSample(sample);

            // Prevent infinite loop.
            if (_step == 0.0)
            {
                return;
            }

            position += _step;

            if (isForward)
            {
                while (position <= till)
                {
#if ASSERT_INVAR_INDICES
                    OperatorCalculatorHelper.AssertStackIndex(_dimensionStack, _dimensionStackIndex);
#endif
#if !USE_INVAR_INDICES
                    _dimensionStack.Set(position);
#else
                    _dimensionStack.Set(_dimensionStackIndex, position);
#endif
                    sample = _collectionCalculator.Calculate();

                    ProcessNextSample(sample);

                    position += _step;
                }
            }
            else
            {
                // Is backwards
                while (position >= till)
                {
#if ASSERT_INVAR_INDICES
                    OperatorCalculatorHelper.AssertStackIndex(_dimensionStack, _dimensionStackIndex);
#endif
#if !USE_INVAR_INDICES
                    _dimensionStack.Set(position);
#else
                    _dimensionStack.Set(_dimensionStackIndex, position);
#endif
                    sample = _collectionCalculator.Calculate();

                    ProcessNextSample(sample);

                    position += _step;
                }
            }
#if ASSERT_INVAR_INDICES
            OperatorCalculatorHelper.AssertStackIndex(_dimensionStack, _dimensionStackIndex);
#endif
#if !USE_INVAR_INDICES
            _dimensionStack.Set(originalPosition);
#else
            _dimensionStack.Set(_dimensionStackIndex, originalPosition);
#endif
            FinalizeSampling();
        }
    }
}
