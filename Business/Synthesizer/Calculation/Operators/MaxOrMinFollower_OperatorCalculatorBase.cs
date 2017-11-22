using System.Collections.Generic;
using System.Runtime.CompilerServices;
using JJ.Business.Synthesizer.Helpers;
using JJ.Framework.Collections;

namespace JJ.Business.Synthesizer.Calculation.Operators
{
	/// <summary>
	/// Base class for MaxFollower_OperatorCalculator and MinFollower_OperatorCalculator that have almost the same implementation.
	/// </summary>
	internal abstract class MaxOrMinFollower_OperatorCalculatorBase : OperatorCalculatorBase_WithChildCalculators
	{
		private readonly OperatorCalculatorBase _signalCalculator;
		private readonly OperatorCalculatorBase _sliceLengthCalculator;
		private readonly OperatorCalculatorBase _sampleCountCalculator;
		private readonly OperatorCalculatorBase _positionInputCalculator;
		private readonly VariableInput_OperatorCalculator _positionOutputCalculator;

		private double _sampleDuration;
		private double _sampleCountDouble;

		private Queue<double> _queue;

		/// <summary>
		/// Even though the RedBlackTree does not store duplicates,
		/// which is something you would want, this might not significantly affect the outcome.
		/// </summary>
		private RedBlackTree<double, double> _redBlackTree;

		private double _maxOrMin;
		private double _previousPosition;
		private double _nextSamplePosition;
		private double _sliceLength;

		public MaxOrMinFollower_OperatorCalculatorBase(
			OperatorCalculatorBase signalCalculator,
			OperatorCalculatorBase sliceLengthCalculator,
			OperatorCalculatorBase sampleCountCalculator,
			OperatorCalculatorBase positionInputCalculator,
			VariableInput_OperatorCalculator positionOutputCalculator)
			: base(new[]
			{
				signalCalculator,
				sliceLengthCalculator,
				sampleCountCalculator,
				positionInputCalculator,
				positionOutputCalculator
			})
		{
			_signalCalculator = signalCalculator;
			_sliceLengthCalculator = sliceLengthCalculator;
			_sampleCountCalculator = sampleCountCalculator;
			_positionInputCalculator = positionInputCalculator;
			_positionOutputCalculator = positionOutputCalculator;

			ResetNonRecursive();
		}

		protected abstract double GetMaxOrMin(RedBlackTree<double, double> redBlackTree);

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public override double Calculate()
		{
			double position = _positionInputCalculator.Calculate();

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
					double positionDifferenceTooMuch = positionDifference - _sliceLength;
					if (positionDifferenceTooMuch > 0.0)
					{
						_nextSamplePosition += positionDifferenceTooMuch;
					}

					do
					{
						_positionOutputCalculator._value = _nextSamplePosition;

						CalculateValueAndUpdateCollections();

						_nextSamplePosition += _sampleDuration;
					}
					while (position > _nextSamplePosition);

					_maxOrMin = GetMaxOrMin(_redBlackTree);
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
					double positionDifferenceTooMuch = positionDifference - _sliceLength;
					if (positionDifferenceTooMuch > 0.0)
					{
						_nextSamplePosition -= positionDifferenceTooMuch;
					}

					do
					{
						_positionOutputCalculator._value = _nextSamplePosition;

						CalculateValueAndUpdateCollections();

						_nextSamplePosition -= _sampleDuration;
					}
					while (position < _nextSamplePosition);

					_maxOrMin = GetMaxOrMin(_redBlackTree);
				}
			}

			// Check difference with brute force
			// (slight difference due to RedBlackTree not adding duplicates):
			//double treeMax = _max;
			//_max = _queue.Max();
			//if (treeMax != _max)
			//{
			//	int i = 0;
			//}

			_previousPosition = position;

			return _maxOrMin;
		}

		private void CalculateValueAndUpdateCollections()
		{
			double newValue = _signalCalculator.Calculate();

			double oldValue = _queue.Dequeue();
			_queue.Enqueue(newValue);

			_redBlackTree.Delete(oldValue);
			_redBlackTree.Insert(newValue, newValue);
		}

		public override void Reset()
		{
			ResetNonRecursive();

			base.Reset();
		}

		private void ResetNonRecursive()
		{
			double position = _positionInputCalculator.Calculate();

			_previousPosition = position;

			_maxOrMin = 0.0;
			_nextSamplePosition = 0.0;

			_sliceLength = _sliceLengthCalculator.Calculate();
			_sampleCountDouble = _sampleCountCalculator.Calculate();

			if (ConversionHelper.CanCastToNonNegativeInt32(_sampleCountDouble))
			{
				_sampleCountDouble = (int)_sampleCountDouble;
			}
			else
			{
				_sampleCountDouble = 0.0;
			}

			_sampleDuration = _sliceLength / _sampleCountDouble;

			_redBlackTree = new RedBlackTree<double, double>();
			_queue = CreateQueue();
		}

		private Queue<double> CreateQueue()
		{
			int sampleCountInt = 0;
			if (ConversionHelper.CanCastToNonNegativeInt32(_sampleCountDouble))
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
