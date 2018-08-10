using System.Collections.Generic;
using JJ.Business.Synthesizer.Helpers;
using JJ.Framework.Exceptions.Basic;

namespace JJ.Business.Synthesizer.Calculation.Operators
{
	internal abstract class OperatorCalculatorBase_Follower_Original : OperatorCalculatorBase_WithChildCalculators
	{
		private readonly OperatorCalculatorBase _signalCalculator;
		private readonly OperatorCalculatorBase _sliceLengthCalculator;
		private readonly OperatorCalculatorBase _sampleCountCalculator;
		private readonly OperatorCalculatorBase _positionCalculator;

		private double _sampleDistance;
		private double _previousX;
		private double _passedDx;

		private double _aggregate;
		protected double _sampleCountDouble;
		protected Queue<double> _queue;

		public OperatorCalculatorBase_Follower_Original(
			OperatorCalculatorBase signalCalculator,
			OperatorCalculatorBase sliceLengthCalculator,
			OperatorCalculatorBase sampleCountCalculator,
			OperatorCalculatorBase positionCalculator)
			: base(new[]
			{
				signalCalculator,
				sliceLengthCalculator,
				sampleCountCalculator,
				positionCalculator
			})
		{
			_signalCalculator = signalCalculator ?? throw new NullException(() => signalCalculator);
			_sliceLengthCalculator = sliceLengthCalculator ?? throw new NullException(() => sliceLengthCalculator);
			_sampleCountCalculator = sampleCountCalculator ?? throw new NullException(() => sampleCountCalculator);
			_positionCalculator = positionCalculator ?? throw new NullException(() => signalCalculator);

			// ReSharper disable once VirtualMemberCallInConstructor
			ResetNonRecursive();
		}

		protected abstract double Aggregate(double sample);

		public sealed override double Calculate()
		{
            // Calculate (base)
			double x = _positionCalculator.Calculate();

		    // MustShiftForward
            double dx = x - _previousX;
			if (dx >= 0)
			{
				_passedDx += dx;
			}
			else
			{
				// Substitute for Math.Abs().
				// This makes it work for changes that go in reverse and even position changes that quickly goes back and forth.
				_passedDx -= dx;
			}

		    if (_passedDx >= _sampleDistance)
			{
                // SetNextSample
				double y = _signalCalculator.Calculate();
				_queue.Enqueue(y);

                // ShiftForward/Precalculate
                _aggregate = Aggregate(y);

			    // Phase tracking
				// It may not be arithmetically perfect, that we ignore the fact that
                // _passedSampleLength may be significantly greater than _sampleDuration,
                // but in practice for this application it might not matter very much.
                _passedDx = 0.0;
			}

            // Calculate (abstract)
            // Phase tracking
			_previousX = x;

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

			_previousX = position;

			_aggregate = 0.0;
			_passedDx = 0.0;

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
			int sampleCountInt = 0;
			if (CalculationHelper.CanCastToNonNegativeInt32(sampleCountDouble))
			{
				sampleCountInt = (int)_sampleCountDouble;
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
