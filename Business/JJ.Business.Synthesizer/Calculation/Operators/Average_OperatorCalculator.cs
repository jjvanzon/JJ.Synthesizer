using System;
using System.Collections.Generic;
using System.Linq;
using JJ.Business.Synthesizer.Enums;
using JJ.Business.Synthesizer.Helpers;
using JJ.Framework.Reflection.Exceptions;

namespace JJ.Business.Synthesizer.Calculation.Operators
{
    internal class Average_OperatorCalculator : OperatorCalculatorBase_WithChildCalculators
    {
        private readonly OperatorCalculatorBase _signalCalculator;
        private readonly OperatorCalculatorBase _timeSliceDurationCalculator;
        private readonly OperatorCalculatorBase _sampleCountCalculator;
        private readonly int _dimensionIndex;
        private readonly DimensionStacks _dimensionStack;

        private double _sampleLength;
        private double _sampleCountDouble;

        private Queue<double> _queue;

        private double _sum;
        private double _average;
        private double _previousTime;
        private double _passedSamplingLength;

        public Average_OperatorCalculator(
            OperatorCalculatorBase signalCalculator,
            OperatorCalculatorBase timeSliceDurationCalculator,
            OperatorCalculatorBase sampleCountCalculator,
            DimensionEnum dimensionEnum,
            DimensionStacks dimensionStack)
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

        public override double Calculate()
        {
            double position = _dimensionStack.Get(_dimensionIndex);

            // Update _passedSampleTime
            double positionChange = position - _previousTime;
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

            if (_passedSamplingLength >= _sampleLength)
            {
                // Use a queueing trick to update the average without traversing a whole list.
                // This also makes the average update more continually.
                double oldValue = _queue.Dequeue();
                double newValue = _signalCalculator.Calculate();
                _queue.Enqueue(newValue);

                _sum -= oldValue;
                _sum += newValue;

                _average = _sum / _sampleCountDouble;

                // It may not be arithmetically perfect, that we ignore the fact that
                // _passedSampleTime may be significantly greater than _sampleDuration,
                // but in practice for this application it might not matter very much.
                _passedSamplingLength = 0.0;
            }

            _previousTime = position;

            return _average;
        }

        public override void Reset()
        {
            double position = _dimensionStack.Get(_dimensionIndex);

            _previousTime = position;

            _sum = 0.0;
            _average = 0.0;
            _passedSamplingLength = 0.0;

            double timeSliceDuration = _timeSliceDurationCalculator.Calculate();
            _sampleCountDouble = _sampleCountCalculator.Calculate();

            if (ConversionHelper.CanCastToNonNegativeInt32(_sampleCountDouble))
            {
                _sampleCountDouble = (int)_sampleCountDouble;
            }
            else
            {
                _sampleCountDouble = 0.0;
            }

            _sampleLength = timeSliceDuration / _sampleCountDouble;

            _queue = CreateQueue(_sampleCountDouble);

            base.Reset();
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