using JJ.Framework.Collections;

namespace JJ.Business.Synthesizer.Calculation.Operators
{
	/// <summary>
	/// Base class for MaxFollower_OperatorCalculator and MinFollower_OperatorCalculator that have almost the same implementation.
	/// </summary>
	internal abstract class MaxOrMinFollower_OperatorCalculatorBase : OperatorCalculatorBase_Follower
	{
		/// <summary>
		/// Even though the RedBlackTree does not store duplicates,
		/// which is something you would want, this might not significantly affect the outcome.
		/// </summary>
		protected RedBlackTree<double, double> _redBlackTree;

		public MaxOrMinFollower_OperatorCalculatorBase(
			OperatorCalculatorBase signalCalculator,
			OperatorCalculatorBase sliceLengthCalculator,
			OperatorCalculatorBase sampleCountCalculator,
			OperatorCalculatorBase positionInputCalculator)
			: base(signalCalculator, sliceLengthCalculator, sampleCountCalculator, positionInputCalculator) { }

		/// <summary> base returns default </summary>
		protected override double Aggregate(double sample)
		{
			double oldValue = _queue.Dequeue();

			_redBlackTree.Insert(sample, sample);
			_redBlackTree.Delete(oldValue);

			return default;
		}

		protected override void ResetNonRecursive()
		{
			base.ResetNonRecursive();

			_redBlackTree = new RedBlackTree<double, double>();
		}
	}
}