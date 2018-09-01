using System.Collections.Generic;
using JJ.Business.Synthesizer.Helpers;

namespace JJ.Business.Synthesizer.Calculation.Operators
{
    internal abstract class OperatorCalculatorBase_Follower : OperatorCalculatorBase_WithChildCalculators
    {
        private readonly OperatorCalculatorBase _signalCalculator;
        private readonly OperatorCalculatorBase _sliceLengthCalculator;
        private readonly OperatorCalculatorBase _sampleCountCalculator;
        private readonly OperatorCalculatorBase _positionCalculator;

        private double _sampleDistance;
        private double _previousPosition;
        private double _passedSamplingLength;

        private double _aggregate;
        protected double _sampleCountDouble;
        protected Queue<double> _queue;

        public OperatorCalculatorBase_Follower(
            OperatorCalculatorBase signalCalculator,
            OperatorCalculatorBase sliceLengthCalculator,
            OperatorCalculatorBase sampleCountCalculator,
            OperatorCalculatorBase positionCalculator)
            : base(
                new[]
                {
                    signalCalculator,
                    sliceLengthCalculator,
                    sampleCountCalculator,
                    positionCalculator
                })
        {
            _signalCalculator = signalCalculator;
            _sliceLengthCalculator = sliceLengthCalculator;
            _sampleCountCalculator = sampleCountCalculator;
            _positionCalculator = positionCalculator;

            // ReSharper disable once VirtualMemberCallInConstructor
            ResetNonRecursive();
        }

        protected abstract double Aggregate(double sample);

        public sealed override double Calculate()
        {
            double position = _positionCalculator.Calculate();

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
                double sample = _signalCalculator.Calculate();

                _queue.Enqueue(sample);

                _aggregate = Aggregate(sample);

                // It may not be arithmetically perfect, that we ignore the fact that
                // _passedSampleLength may be significantly greater than _sampleDuration,
                // but in practice for this application it might not matter very much.
                _passedSamplingLength = 0.0;
            }

            _previousPosition = position;

            return _aggregate;
        }

        public sealed override void Reset()
        {
            base.Reset();

            ResetNonRecursive();
        }

        protected virtual void ResetNonRecursive()
        {
            double position = _positionCalculator.Calculate();

            _previousPosition = position;

            _aggregate = 0.0;
            _passedSamplingLength = 0.0;

            double sliceLength = _sliceLengthCalculator.Calculate();
            _sampleCountDouble = _sampleCountCalculator.Calculate();

            if (CalculationHelper.CanCastToNonNegativeInt32(_sampleCountDouble))
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
            var sampleCountInt = 0;

            if (CalculationHelper.CanCastToNonNegativeInt32(sampleCountDouble))
            {
                sampleCountInt = (int)_sampleCountDouble;
            }

            var queue = new Queue<double>(sampleCountInt);

            for (var i = 0; i < sampleCountInt; i++)
            {
                queue.Enqueue(0.0);
            }

            return queue;
        }
    }
}