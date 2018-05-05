namespace JJ.Business.Synthesizer.Calculation.Operators
{
	internal abstract class Interpolate_OperatorCalculator_Base_2X1Y : Interpolate_OperatorCalculator_Base
	{
		protected Interpolate_OperatorCalculator_Base_2X1Y(
			OperatorCalculatorBase signalCalculator,
			OperatorCalculatorBase samplingRateCalculator,
			OperatorCalculatorBase positionInputCalculator)
			: base(signalCalculator, samplingRateCalculator, positionInputCalculator) { }

		protected internal double _x0;
		protected internal double _x1;
		protected internal double _y0;

		protected sealed override bool MustShiftForward(double x) => x > _x1;

		protected sealed override void ShiftForward() => _x0 = _x1;

		protected sealed override bool MustShiftBackward(double x) => x < _x0;

		protected sealed override void ShiftBackward() => _x1 = _x0;

		protected sealed override void Precalculate() { }

		protected sealed override double Calculate(double x) => _y0;
	}
}