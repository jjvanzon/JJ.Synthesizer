namespace JJ.Business.Synthesizer.Calculation.Operators
{
    internal abstract class Interpolate_OperatorCalculator_Base_2Point_LagBehind : Interpolate_OperatorCalculator_Base_2Point
    {
        protected Interpolate_OperatorCalculator_Base_2Point_LagBehind(
            OperatorCalculatorBase signalCalculator,
            OperatorCalculatorBase samplingRateCalculator,
            OperatorCalculatorBase positionInputCalculator)
            : base(signalCalculator, samplingRateCalculator, positionInputCalculator) { }

        protected sealed override void SetNextSample()
        {
            _x1 += Dx();
            _y1 = _signalCalculator.Calculate();
        }

        protected sealed override void SetPreviousSample()
        {
            _x0 -= Dx();
            _y0 = _signalCalculator.Calculate();
        }

        protected sealed override void ResetNonRecursive()
        {
            double x = _positionInputCalculator.Calculate();
            double y = _signalCalculator.Calculate();

            double dx = Dx();

            _x0 = x;
            _x1 = x + dx;

            _y0 = y;
            _y1 = y;

            Precalculate();
        }
    }
}