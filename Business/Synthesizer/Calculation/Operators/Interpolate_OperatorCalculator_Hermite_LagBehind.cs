using JJ.Framework.Mathematics;

namespace JJ.Business.Synthesizer.Calculation.Operators
{
    internal sealed class Interpolate_OperatorCalculator_Hermite_LagBehind 
        : Interpolate_OperatorCalculator_Base_4Point_LagBehind
    {
        private double _dx;
        private double _c0;
        private double _c1;
        private double _c2;
        private double _c3;

        public Interpolate_OperatorCalculator_Hermite_LagBehind(
            OperatorCalculatorBase signalCalculator,
            OperatorCalculatorBase samplingRateCalculator,
            OperatorCalculatorBase positionInputCalculator)
            : base(signalCalculator, samplingRateCalculator, positionInputCalculator)
            => ResetNonRecursive();

        protected override void Precalculate()
        {
            _dx = Dx();
            (_c0, _c1, _c2, _c3) = Interpolator.Hermite4pt3oX_PrecalculateVariables(_yMinus1, _y0, _y1, _y2);
        }

        protected override double Calculate(double x)
        {
            double t = (x - _x0) / _dx;
            double y = Interpolator.Hermite4pt3oX_FromPrecalculatedVariables(_c0, _c1, _c2, _c3, t);
            return y;
        }
    }
}
