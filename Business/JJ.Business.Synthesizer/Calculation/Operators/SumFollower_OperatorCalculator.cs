using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using JJ.Business.Synthesizer.Helpers;

namespace JJ.Business.Synthesizer.Calculation.Operators
{
    internal class SumFollower_OperatorCalculator : OperatorCalculatorBase_WithChildCalculators
    {
        private readonly OperatorCalculatorBase _signalCalculator;
        private readonly OperatorCalculatorBase _sliceLengthCalculator;
        private readonly OperatorCalculatorBase _sampleCountCalculator;
        private readonly DimensionStack _dimensionStack;
        private readonly int _dimensionStackIndex;

        private double _sampleDistance;
        protected double _sampleCountDouble;

        private Queue<double> _queue;

        protected double _sum;
        private double _aggregate;
        private double _previousPosition;
        private double _passedSamplingLength;

        public SumFollower_OperatorCalculator(
            OperatorCalculatorBase signalCalculator,
            OperatorCalculatorBase sliceLengthCalculator,
            OperatorCalculatorBase sampleCountCalculator,
            DimensionStack dimensionStack)
            : base(new OperatorCalculatorBase[]
            {
                signalCalculator,
                sliceLengthCalculator,
                sampleCountCalculator
            })
        {
            OperatorCalculatorHelper.AssertChildOperatorCalculator(signalCalculator, () => signalCalculator);
            OperatorCalculatorHelper.AssertChildOperatorCalculator_OnlyUsedUponResetState(sliceLengthCalculator, () => sliceLengthCalculator);
            OperatorCalculatorHelper.AssertChildOperatorCalculator_OnlyUsedUponResetState(sampleCountCalculator, () => sampleCountCalculator);
            OperatorCalculatorHelper.AssertDimensionStack(dimensionStack);
            
            _signalCalculator = signalCalculator;
            _sliceLengthCalculator = sliceLengthCalculator;
            _sampleCountCalculator = sampleCountCalculator;
            _dimensionStack = dimensionStack;
            _dimensionStackIndex = dimensionStack.CurrentIndex;

            ResetNonRecursive();
        }

        public override double Calculate()
        {
#if !USE_INVAR_INDICES
            double position = _dimensionStack.Get();
#else
            double position = _dimensionStack.Get(_dimensionStackIndex);
#endif

#if ASSERT_INVAR_INDICES
            OperatorCalculatorHelper.AssertStackIndex(_dimensionStack, _dimensionStackIndex);
#endif
            // Update _passedSamplingLength
            double positionChange = position - _previousPosition;
            if (positionChange >= 0)
            {
                _passedSamplingLength += positionChange;
            }
            else
            {
                // Substitute for Math.Abs().
                // This makes it work for changes that go in reverse and even position changes that quickly goes back and forth.
                _passedSamplingLength -= positionChange;
            }

            if (_passedSamplingLength >= _sampleDistance)
            {
                // Use a queueing trick to update the average without traversing a whole list.
                // This also makes the average update more continually.
                double oldValue = _queue.Dequeue();
                double newValue = _signalCalculator.Calculate();
                _queue.Enqueue(newValue);

                _sum -= oldValue;
                _sum += newValue;

                // It may not be arithmetically perfect, that we ignore the fact that
                // _passedSampleLength may be significantly greater than _sampleDuration,
                // but in practice for this application it might not matter very much.
                _passedSamplingLength = 0.0;

                _aggregate = PostProcessAggregate();
            }

            _previousPosition = position;

            return _aggregate;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        protected virtual double PostProcessAggregate()
        {
            return _sum;
        }

        public override void Reset()
        {
            base.Reset();

            ResetNonRecursive();
        }

        private void ResetNonRecursive()
        {
#if !USE_INVAR_INDICES
            double position = _dimensionStack.Get();
#else
            double position = _dimensionStack.Get(_dimensionStackIndex);
#endif

#if ASSERT_INVAR_INDICES
            OperatorCalculatorHelper.AssertStackIndex(_dimensionStack, _dimensionStackIndex);
#endif

            _previousPosition = position;

            _sum = 0.0;
            _aggregate = 0.0;
            _passedSamplingLength = 0.0;

            double sliceLength = _sliceLengthCalculator.Calculate();
            _sampleCountDouble = _sampleCountCalculator.Calculate();

            if (ConversionHelper.CanCastToNonNegativeInt32(_sampleCountDouble))
            {
                _sampleCountDouble = (int)_sampleCountDouble;
            }
            else
            {
                _sampleCountDouble = 0.0;
            }

            _sampleDistance = sliceLength / _sampleCountDouble;

            _queue = CreateQueue(_sampleCountDouble);
        }

        private Queue<double> CreateQueue(double sampleCountDouble)
        {
            int sampleCountInt = 0;
            if (ConversionHelper.CanCastToNonNegativeInt32(sampleCountDouble))
            {
                sampleCountInt = (int)(_sampleCountDouble);
            }

            Queue<double> queue = new Queue<double>(sampleCountInt);
            for (int i = 0; i < sampleCountInt; i++)
            {
                queue.Enqueue(0.0);
            }

            return queue;
        }
   }
}