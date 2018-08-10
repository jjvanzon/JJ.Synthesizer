using System.Collections.Generic;
using JJ.Business.Synthesizer.Helpers;
// ReSharper disable ConvertIfStatementToConditionalTernaryExpression

namespace JJ.Business.Synthesizer.Calculation.Operators
{
    internal abstract class OperatorCalculatorBase_FollowingSampler_ManyPoint : OperatorCalculatorBase_FollowingSampler
    {
        protected double _yLast;
        protected double _yFirst;
        private Queue<double> _queue;

        public OperatorCalculatorBase_FollowingSampler_ManyPoint(
            OperatorCalculatorBase signalCalculator,
            OperatorCalculatorBase samplingRateCalculator,
            OperatorCalculatorBase positionCalculator,
            params OperatorCalculatorBase[] additionalChildCalculators)
            : base(signalCalculator, samplingRateCalculator, positionCalculator, additionalChildCalculators) { }

        protected override void ShiftForward() => Shift();
        protected override void ShiftBackward() => Shift();

        private void Shift()
        {
            if (_queue.Count != 0)
            {
                _yLast = _queue.Dequeue();
            }
            else
            {
                _yLast = default;
            }
        }

        protected override void SetNextSample() => SetSample();
        protected override void SetPreviousSample() => SetSample();

        private void SetSample()
        {
            _yFirst = _signalCalculator.Calculate();
            _queue.Enqueue(_yFirst);
        }

        protected override void ResetNonRecursive()
        {
            base.ResetNonRecursive();

            int sampleCountInt = GetPointCountInt();

            _queue = CreateQueue(sampleCountInt);
        }

        private int GetPointCountInt()
        {
            double sampleCountDouble = GetSampleCount();

            int sampleCountInt = default;

            if (CalculationHelper.CanCastToNonNegativeInt32(sampleCountDouble))
            {
                sampleCountInt = (int)sampleCountDouble;
            }

            return sampleCountInt;
        }

        protected abstract double GetSampleCount();

        private Queue<double> CreateQueue(int pointCountInt)
        {
            var queue = new Queue<double>(pointCountInt);
            for (int i = 0; i < pointCountInt; i++)
            {
                queue.Enqueue(0.0);
            }
            return queue;
        }
    }
}