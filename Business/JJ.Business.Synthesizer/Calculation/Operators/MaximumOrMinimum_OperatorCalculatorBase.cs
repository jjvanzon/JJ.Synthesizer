using System;
using System.Collections.Generic;
using System.Linq;
using JJ.Framework.Collections;
using JJ.Framework.Reflection.Exceptions;

namespace JJ.Business.Synthesizer.Calculation.Operators
{
    /// <summary>
    /// Base class for Maximum_OperatorCalculator and Minimum_OperatorCalculator that have almost the same implementation.
    /// </summary>
    internal abstract class MaximumOrMinimum_OperatorCalculatorBase : OperatorCalculatorBase_WithChildCalculators
    {
        private const double DEFAULT_TIME = 0.0;
        private const int DEFAULT_CHANNEL_INDEX = 0;

        private readonly OperatorCalculatorBase _signalCalculator;
        private readonly OperatorCalculatorBase _timeSliceDurationCalculator;
        private readonly OperatorCalculatorBase _sampleCountCalculator;

        private double _sampleDuration;
        private double _sampleCountDouble;

        private Queue<double> _queue;

        /// <summary>
        /// Even though the RedBlackTree does not store duplicates,
        /// which is something you would want, this might not significantly affect the outcome.
        /// </summary>
        private RedBlackTree<double, double> _redBlackTree;

        private double _maximumOrMinimum;
        private double _previousTime;
        private double _nextSampleTime;
        private double _timeSliceDuration;

        public MaximumOrMinimum_OperatorCalculatorBase(
            OperatorCalculatorBase signalCalculator,
            OperatorCalculatorBase timeSliceDurationCalculator,
            OperatorCalculatorBase sampleCountCalculator)
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

            _signalCalculator = signalCalculator;
            _timeSliceDurationCalculator = timeSliceDurationCalculator;
            _sampleCountCalculator = sampleCountCalculator;

            Reset(DEFAULT_TIME, DEFAULT_CHANNEL_INDEX);
        }

        protected abstract double GetMaximumOrMinimum(RedBlackTree<double, double> redBlackTree);

        public override double Calculate(double time, int channelIndex)
        {
            bool isForwardInTime = time >= _previousTime;

            if (isForwardInTime)
            {
                bool mustUpdate = time > _nextSampleTime;
                if (mustUpdate)
                {
                    // Fake last sample time if time difference too much.
                    // This prevents excessive sampling in case of a large jump in time.
                    // (Also takes care of the assumption that time would start at 0.)
                    double timeDifference = time - _nextSampleTime;
                    double timeDifferenceTooMuch = timeDifference - _timeSliceDuration;
                    if (timeDifferenceTooMuch > 0.0)
                    {
                        _nextSampleTime += timeDifferenceTooMuch;
                    }

                    do
                    {
                        CalculateValueAndUpdateCollections(_nextSampleTime, channelIndex);

                        _nextSampleTime += _sampleDuration;
                    }
                    while (time > _nextSampleTime);

                    _maximumOrMinimum = GetMaximumOrMinimum(_redBlackTree);
                }
            }
            else
            {
                // Is backwards in time
                bool mustUpdate = time < _nextSampleTime;
                if (mustUpdate)
                {
                    // Fake last sample time if time difference too much.
                    // This prevents excessive sampling in case of a large jump in time.
                    // (Also takes care of the assumption that time would start at 0.)
                    double timeDifference = _nextSampleTime - time;
                    double timeDifferenceTooMuch = timeDifference - _timeSliceDuration;
                    if (timeDifferenceTooMuch > 0.0)
                    {
                        _nextSampleTime -= timeDifferenceTooMuch;
                    }

                    do
                    {
                        CalculateValueAndUpdateCollections(_nextSampleTime, channelIndex);

                        _nextSampleTime -= _sampleDuration;
                    }
                    while (time < _nextSampleTime);

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

            _previousTime = time;

            return _maximumOrMinimum;
        }

        private void CalculateValueAndUpdateCollections(double time, int channelIndex)
        {
            double newValue = _signalCalculator.Calculate(time, channelIndex);

            double oldValue = _queue.Dequeue();
            _queue.Enqueue(newValue);

            _redBlackTree.Delete(oldValue);
            _redBlackTree.Insert(newValue, newValue);
        }

        public override void Reset(double time, int channelIndex)
        {
            _previousTime = time;

            _maximumOrMinimum = 0.0;
            _nextSampleTime = 0.0;

            _timeSliceDuration = _timeSliceDurationCalculator.Calculate(time, channelIndex);
            _sampleCountDouble = _sampleCountCalculator.Calculate(time, channelIndex);

            if (CalculationHelper.CanCastToNonNegativeInt32(_sampleCountDouble))
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

            base.Reset(time, channelIndex);
        }

        private Queue<double> CreateQueue()
        {
            int sampleCountInt = 0;
            if (CalculationHelper.CanCastToNonNegativeInt32(_sampleCountDouble))
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
