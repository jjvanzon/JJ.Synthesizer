using System.Collections.Generic;
using JJ.Business.Synthesizer.Helpers;
using JJ.Framework.Exceptions.Basic;

namespace JJ.Business.Synthesizer.Calculation.Operators
{
	internal abstract class OperatorCalculatorBase_Follower_1stAttempt : Interpolate_OperatorCalculator_Base_1stAttempt
    {
		private readonly OperatorCalculatorBase _sliceLengthCalculator;

		private double _aggregate;
		protected double _sampleCountDouble;
		protected Queue<double> _queue;
        private double _y;

        public OperatorCalculatorBase_Follower_1stAttempt(
            OperatorCalculatorBase signalCalculator,
            OperatorCalculatorBase sliceLengthCalculator,
            OperatorCalculatorBase sampleCountCalculator,
            OperatorCalculatorBase positionCalculator)
            : base(signalCalculator, sampleCountCalculator, positionCalculator, sliceLengthCalculator)
        {
            _sliceLengthCalculator = sliceLengthCalculator ?? throw new NullException(() => sliceLengthCalculator);

            // ReSharper disable once VirtualMemberCallInConstructor
            ResetNonRecursive();
        }

        protected abstract double Aggregate(double sample);

		public sealed override double Calculate()
		{
			double x = _positionCalculator.Calculate();

            if (MustShiftForward(x))
            {
                ShiftForward();
                SetNextSample();
                Precalculate();
			}

		    return Calculate(x);
		}

        protected override void SetNextSample()
        {
            double y = _signalCalculator.Calculate();
            _queue.Enqueue(y);
            _y = y;
        }

        protected override void Precalculate() 
            => _aggregate = Aggregate(_y);

        protected override double Calculate(double x)
        {
            base.Calculate(x);

            return _aggregate;
        }

		protected override void ResetNonRecursive()
		{
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
