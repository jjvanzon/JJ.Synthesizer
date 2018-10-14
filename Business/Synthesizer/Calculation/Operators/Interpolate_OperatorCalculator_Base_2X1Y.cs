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


        /// <summary>
        /// The >= instead of > is on purpose.
        /// In case of block interpolation it matters that you switch values when you are right on the border between values.
        /// Note that for backward direction this is not done. You have to choose if right on the border it will be value A or B.
        /// We choose B.
        /// </summary>
		protected sealed override bool MustShiftForward(double x) => x >= _x1;

		protected sealed override void ShiftForward() => _x0 = _x1;

		protected sealed override bool MustShiftBackward(double x) => x < _x0;

		protected sealed override void ShiftBackward() => _x1 = _x0;

		protected sealed override void Precalculate() { }

		protected sealed override double Calculate(double x) => _y0;
	}
}