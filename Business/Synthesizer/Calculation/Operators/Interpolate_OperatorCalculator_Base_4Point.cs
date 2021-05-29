namespace JJ.Business.Synthesizer.Calculation.Operators
{
    internal abstract class Interpolate_OperatorCalculator_Base_4Point : Interpolate_OperatorCalculator_Base
    {
        protected Interpolate_OperatorCalculator_Base_4Point(
            OperatorCalculatorBase signalCalculator,
            OperatorCalculatorBase samplingRateCalculator,
            OperatorCalculatorBase positionInputCalculator)
            : base(signalCalculator, samplingRateCalculator, positionInputCalculator) { }

        protected double _xMinus1;
        protected double _x0;
        protected double _x1;
        protected double _x2;
        protected double _yMinus1;
        protected double _y0;
        protected double _y1;
        protected double _y2;

        protected sealed override bool MustShiftForward(double x) => x > _x1;

        protected sealed override void ShiftForward()
        {
            _xMinus1 = _x0;
            _x0 = _x1;
            _x1 = _x2;
            _yMinus1 = _y0;
            _y0 = _y1;
            _y1 = _y2;
        }

        protected sealed override bool MustShiftBackward(double x) => x < _x0;

        protected sealed override void ShiftBackward()
        {
            _x2 = _x1;
            _x1 = _x0;
            _x0 = _xMinus1;
            _y2 = _y1;
            _y1 = _y0;
            _y0 = _yMinus1;
        }
    }
}