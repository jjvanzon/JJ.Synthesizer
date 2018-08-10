using System.Collections.Generic;
using JJ.Business.Synthesizer.Helpers;
using JJ.Framework.Exceptions.Basic;

namespace JJ.Business.Synthesizer.Calculation.Operators
{
	internal abstract class OperatorCalculatorBase_Follower_2ndAttempt : OperatorCalculatorBase_FollowingSampler
    {
		private readonly OperatorCalculatorBase _sliceLengthCalculator;

        protected double _yMinus1;
        protected double _y0;
        private double _aggregate;

        /// <summary> Replace by _queue.Count? </summary>
		protected double _sampleCountDouble;
		protected Queue<double> _queue;

		public OperatorCalculatorBase_Follower_2ndAttempt(
			OperatorCalculatorBase signalCalculator,
			OperatorCalculatorBase sliceLengthCalculator,
			OperatorCalculatorBase samplingRateCalculator,
			OperatorCalculatorBase positionCalculator)
		    : base(signalCalculator, samplingRateCalculator, positionCalculator, sliceLengthCalculator)
        {
			_sliceLengthCalculator = sliceLengthCalculator ?? throw new NullException(() => sliceLengthCalculator);

			// ReSharper disable once VirtualMemberCallInConstructor
			ResetNonRecursive();
		}

		protected sealed override double Calculate(double x) => _aggregate;

        protected override void ShiftForward() => Shift();
        protected override void ShiftBackward() => Shift();

        private void Shift()
        {
            _yMinus1 = _y0;
            _queue.Dequeue();
        }

        protected override void SetNextSample() => SetSample();
        protected override void SetPreviousSample() => SetSample();

        private void SetSample()
        {
            _y0 = _signalCalculator.Calculate();
            _queue.Enqueue(_y0);
        }

        protected override void Precalculate() => _aggregate = Aggregate(_y0);

        protected abstract double Aggregate(double sample);

        protected override void ResetNonRecursive()
		{
		    base.ResetNonRecursive();

			_aggregate = 0.0;

			double sliceLength = _sliceLengthCalculator.Calculate();
		    double samplingRate = _samplingRateCalculator.Calculate();
			_sampleCountDouble = sliceLength * samplingRate;

			if (CalculationHelper.CanCastToNonNegativeInt32(_sampleCountDouble))
			{
				_sampleCountDouble = (int)_sampleCountDouble;
			}
			else
			{
				_sampleCountDouble = 0.0;
			}

			_queue = CreateQueue((int)_sampleCountDouble);
		}

		private Queue<double> CreateQueue(int initialSampleCountInt)
        {
			var queue = new Queue<double>(initialSampleCountInt);
			for (int i = 0; i < initialSampleCountInt; i++)
			{
				queue.Enqueue(0.0);
			}

			return queue;
		}
	}
}
