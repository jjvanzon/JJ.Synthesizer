using JJ.Framework.Collections;

namespace JJ.Business.Synthesizer.Calculation.Operators
{
	internal class MinFollower_OperatorCalculator : MaxOrMinFollower_OperatorCalculatorBase
	{
		public MinFollower_OperatorCalculator(
			OperatorCalculatorBase signalCalculator,
			OperatorCalculatorBase sliceLengthCalculator,
			OperatorCalculatorBase sampleCountCalculator,
			OperatorCalculatorBase positionInputCalculator,
			VariableInput_OperatorCalculator positionOutputCalculator)
			: base(
				signalCalculator,
				sliceLengthCalculator,
				sampleCountCalculator,
				positionInputCalculator,
				positionOutputCalculator)
		{ }

		protected override double GetMaxOrMin(RedBlackTree<double, double> redBlackTree)
		{
			return redBlackTree.GetMinimum();
		}
	}
}