namespace JJ.Business.Synthesizer.Calculation.Operators
{
	internal abstract class OperatorCalculatorBase_FollowingSampler_2X1Y : OperatorCalculatorBase_FollowingSampler
	{
		protected OperatorCalculatorBase_FollowingSampler_2X1Y(
			OperatorCalculatorBase signalCalculator,
			OperatorCalculatorBase samplingRateCalculator,
			OperatorCalculatorBase positionCalculator)
			: base(signalCalculator, samplingRateCalculator, positionCalculator) { }

		protected internal double _x0;
		protected internal double _x1;
		protected internal double _y0;

		protected sealed override void ShiftForward() => _x0 = _x1;

		protected sealed override void ShiftBackward() => _x1 = _x0;

		protected sealed override void Precalculate() { }

		protected sealed override double Calculate(double x) => _y0;
	}
}