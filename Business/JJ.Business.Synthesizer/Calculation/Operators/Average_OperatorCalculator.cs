using System;
using System.Collections.Generic;
using System.Linq;

namespace JJ.Business.Synthesizer.Calculation.Operators
{
    internal class Average_OperatorCalculator : OperatorCalculatorBase_WithChildCalculators
    {
        // TODO: Use channelIndex variable in ResetState.
        private const int DEFAULT_CHANNEL_INDEX = 0;

        private readonly OperatorCalculatorBase _signalCalculator;
        private readonly OperatorCalculatorBase _timeSliceDurationCalculator;
        private readonly OperatorCalculatorBase _sampleCountCalculator;

        private double _sampleDuration;
        private double _sampleCountDouble;

        private Queue<double> _queue;

        private double _sum;
        private double _average;
        private double _previousTime;
        private double _passedSampleTime;

        public Average_OperatorCalculator(
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

            ResetState();
        }

        public override double Calculate(double time, int channelIndex)
        {
            // Update _passedSampleTime
            double dt = time - _previousTime;
            if (dt >= 0)
            {
                _passedSampleTime += dt;
            }
            else
            {
                // Substitute for Math.Abs().
                // This makes it work for time that goes in reverse and even time that quickly goes back and forth.
                _passedSampleTime -= dt;
            }

            if (_passedSampleTime >= _sampleDuration)
            {
                // Use a queueing trick to update the average without traversing a whole list.
                // This also makes the average update more continually.
                double oldValue = _queue.Dequeue();
                double newValue = _signalCalculator.Calculate(time, channelIndex);
                _queue.Enqueue(newValue);

                _sum -= oldValue;
                _sum += newValue;

                _average = _sum / _sampleCountDouble;

                // It may not be arithmetically perfect, that we ignore the fact that
                // _passedSampleTime may be significantly greater than _sampleDuration,
                // but in practice for this application it might not matter very much.
                _passedSampleTime = 0.0;
            }

            _previousTime = time;

            return _average;
        }

        public override void ResetState()
        {
            _sum = 0.0;
            _average = 0.0;
            _previousTime = 0.0;
            _passedSampleTime = 0.0;

            double timeSliceDuration = _timeSliceDurationCalculator.Calculate(_previousTime, DEFAULT_CHANNEL_INDEX);
            _sampleCountDouble = _sampleCountCalculator.Calculate(_previousTime, DEFAULT_CHANNEL_INDEX);

            if (CalculationHelper.CanCastToNonNegativeInt32(_sampleCountDouble))
            {
                _sampleCountDouble = (int)_sampleCountDouble;
            }
            else
            {
                _sampleCountDouble = 0.0;
            }

            _sampleDuration = timeSliceDuration / _sampleCountDouble;

            _queue = CreateQueue(_sampleCountDouble);

            base.ResetState();
        }

        private Queue<double> CreateQueue(double sampleCountDouble)
        {
            int sampleCountInt = 0;
            if (CalculationHelper.CanCastToNonNegativeInt32(sampleCountDouble))
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