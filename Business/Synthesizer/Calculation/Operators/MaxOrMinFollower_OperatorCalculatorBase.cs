using JJ.Framework.Collections;

namespace JJ.Business.Synthesizer.Calculation.Operators
{
	/// <summary>
	/// Base class for MaxFollower_OperatorCalculator and MinFollower_OperatorCalculator that have almost the same implementation.
	/// </summary>
	internal abstract class MaxOrMinFollower_OperatorCalculatorBase : OperatorCalculatorBase_FollowingSampler_Aggregate
	{
		/// <summary>
		/// Even though the RedBlackTree does not store duplicates,
		/// which is something you would want, this might not significantly affect the outcome.
		/// </summary>
		protected RedBlackTree<double, double> _redBlackTree;

		public MaxOrMinFollower_OperatorCalculatorBase(
			OperatorCalculatorBase signalCalculator,
			OperatorCalculatorBase sliceLengthCalculator,
			OperatorCalculatorBase samplingRateCalculator,
			OperatorCalculatorBase positionInputCalculator)
			: base(signalCalculator, sliceLengthCalculator, samplingRateCalculator, positionInputCalculator) { }

		protected override void Precalculate()
		{
		    _redBlackTree.Insert(_yFirst, _yFirst);
		    _redBlackTree.Delete(_yLast);
		}

		protected override void ResetNonRecursive()
		{
			base.ResetNonRecursive();

			_redBlackTree = new RedBlackTree<double, double>();
		}
	}
}