using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using JJ.Business.Synthesizer.Enums;
using JJ.Business.Synthesizer.Helpers;
using JJ.Framework.Collections;
using JJ.Framework.Reflection.Exceptions;

namespace JJ.Business.Synthesizer.Calculation.Operators
{
    /// <summary>
    /// Base class for Maximum_OperatorCalculator and Minimum_OperatorCalculator that have almost the same implementation.
    /// </summary>
    internal abstract class MaximumOrMinimum_OperatorCalculatorBase : OperatorCalculatorBase_WithChildCalculators
    {
        private readonly OperatorCalculatorBase _signalCalculator;
        private readonly OperatorCalculatorBase _timeSliceDurationCalculator;
        private readonly OperatorCalculatorBase _sampleCountCalculator;
        private readonly int _dimensionIndex;
        private readonly DimensionStack _dimensionStack;

        private double _sampleDuration;
        private double _sampleCountDouble;

        private Queue<double> _queue;

        /// <summary>
        /// Even though the RedBlackTree does not store duplicates,
        /// which is something you would want, this might not significantly affect the outcome.
        /// </summary>
        private RedBlackTree<double, double> _redBlackTree;

        private double _maximumOrMinimum;
        private double _previousPosition;
        private double _nextSamplePosition;
        private double _timeSliceDuration;

        public MaximumOrMinimum_OperatorCalculatorBase(
            OperatorCalculatorBase signalCalculator,
            OperatorCalculatorBase timeSliceDurationCalculator,
            OperatorCalculatorBase sampleCountCalculator,
            DimensionEnum dimensionEnum,
            DimensionStack dimensionStack)
            : base(new OperatorCalculatorBase[] 
            {
                signalCalculator,
                timeSliceDurationCalculator,
                sampleCountCalculator
            })
        {
            OperatorCalculatorHelper.AssertOperatorCalculatorBase(signalCalculator, () => signalCalculator);
            OperatorCalculatorHelper.AssertOperatorCalculatorBase_OnlyUsedUponResetState(timeSliceDurationCalculator, () => timeSliceDurationCalculator);
            OperatorCalculatorHelper.AssertOperatorCalculatorBase_OnlyUsedUponResetState(sampleCountCalculator, () => sampleCountCalculator);
            OperatorCalculatorHelper.AssertDimensionEnum(dimensionEnum);
            if (dimensionStack == null) throw new NullException(() => dimensionStack);

            _signalCalculator = signalCalculator;
            _timeSliceDurationCalculator = timeSliceDurationCalculator;
            _sampleCountCalculator = sampleCountCalculator;
            _dimensionIndex = (int)dimensionEnum;
            _dimensionStack = dimensionStack;

            Reset();
        }

        protected abstract double GetMaximumOrMinimum(RedBlackTree<double, double> redBlackTree);

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public override double Calculate()
        {
            double position = _dimensionStack.Get(_dimensionIndex);

            bool isForward = position >= _previousPosition;

            if (isForward)
            {
                bool mustUpdate = position > _nextSamplePosition;
                if (mustUpdate)
                {
                    // Fake last sample position if position difference too much.
                    // This prevents excessive sampling in case of a large jump in position.
                    // (Also takes care of the assumption that position would start at 0.)
                    double positionDifference = position - _nextSamplePosition;
                    double positionDifferenceTooMuch = positionDifference - _timeSliceDuration;
                    if (positionDifferenceTooMuch > 0.0)
                    {
                        _nextSamplePosition += positionDifferenceTooMuch;
                    }

                    do
                    {
                        _dimensionStack.Push(_dimensionIndex, _nextSamplePosition);
                        CalculateValueAndUpdateCollections(_dimensionStack);
                        _dimensionStack.Pop(_dimensionIndex);

                        _nextSamplePosition += _sampleDuration;
                    }
                    while (position > _nextSamplePosition);

                    _maximumOrMinimum = GetMaximumOrMinimum(_redBlackTree);
                }
            }
            else
            {
                // Is backwards
                bool mustUpdate = position < _nextSamplePosition;
                if (mustUpdate)
                {
                    // Fake last sample position if position difference too much.
                    // This prevents excessive sampling in case of a large jump in position.
                    // (Also takes care of the assumption that position would start at 0.)
                    double positionDifference = _nextSamplePosition - position;
                    double positionDifferenceTooMuch = positionDifference - _timeSliceDuration;
                    if (positionDifferenceTooMuch > 0.0)
                    {
                        _nextSamplePosition -= positionDifferenceTooMuch;
                    }

                    do
                    {
                        _dimensionStack.Push(_dimensionIndex, _nextSamplePosition);
                        CalculateValueAndUpdateCollections(_dimensionStack);
                        _dimensionStack.Pop(_dimensionIndex);

                        _nextSamplePosition -= _sampleDuration;
                    }
                    while (position < _nextSamplePosition);

                    _maximumOrMinimum = GetMaximumOrMinimum(_redBlackTree);
                }
            }

            // Check difference with brute force
            // (slight difference due to RedBlackTree not adding duplicates):
            //double treeMax = _maximum;
            //_maximum = _queue.Max();
            //if (treeMax != _maximum)
            //{
            //    int i = 0;
            //}

            _previousPosition = position;

            return _maximumOrMinimum;
        }

        private void CalculateValueAndUpdateCollections(DimensionStack dimensionStack)
        {
            double newValue = _signalCalculator.Calculate();

            double oldValue = _queue.Dequeue();
            _queue.Enqueue(newValue);

            _redBlackTree.Delete(oldValue);
            _redBlackTree.Insert(newValue, newValue);
        }

        public override void Reset()
        {
            double position = _dimensionStack.Get(_dimensionIndex);

            _previousPosition = position;

            _maximumOrMinimum = 0.0;
            _nextSamplePosition = 0.0;

            _timeSliceDuration = _timeSliceDurationCalculator.Calculate();
            _sampleCountDouble = _sampleCountCalculator.Calculate();

            if (ConversionHelper.CanCastToNonNegativeInt32(_sampleCountDouble))
            {
                _sampleCountDouble = (int)_sampleCountDouble;
            }
            else
            {
                _sampleCountDouble = 0.0;
            }

            _sampleDuration = _timeSliceDuration / _sampleCountDouble;

            _redBlackTree = new RedBlackTree<double, double>();
            _queue = CreateQueue();

            base.Reset();
        }

        private Queue<double> CreateQueue()
        {
            int sampleCountInt = 0;
            if (ConversionHelper.CanCastToNonNegativeInt32(_sampleCountDouble))
            {
                sampleCountInt = (int)(_sampleCountDouble);
            }

            var queue = new Queue<double>(sampleCountInt);
            for (int i = 0; i < sampleCountInt; i++)
            {
                queue.Enqueue(0.0);
            }

            return queue;
        }
    }
}
