using JJ.Framework.Reflection.Exceptions;
using JJ.Business.Synthesizer.Calculation.Curves;
using JJ.Business.Synthesizer.Enums;

namespace JJ.Business.Synthesizer.Calculation.Operators
{
    internal class Curve_MinTime_OperatorCalculator : OperatorCalculatorBase
    {
        private readonly CurveCalculator_MinTime _curveCalculator;
        private double _origin;

        public Curve_MinTime_OperatorCalculator(CurveCalculator_MinTime curveCalculator)
        {
            if (curveCalculator == null) throw new NullException(() => curveCalculator);

            _curveCalculator = curveCalculator;
        }

        public override double Calculate(DimensionStack dimensionStack)
        {
            double time = dimensionStack.Get(DimensionEnum.Time);

            double transformedTime = time - _origin;
            double result = _curveCalculator.CalculateValue(transformedTime);
            return result;
        }

        public override void Reset(DimensionStack dimensionStack)
        {
            double time = dimensionStack.Get(DimensionEnum.Time);

            _origin = time;

            base.Reset(dimensionStack);
        }
    }

    internal class Curve_MinTimeZero_OperatorCalculator : OperatorCalculatorBase
    {
        private readonly CurveCalculator_MinTimeZero _curveCalculator;
        private double _origin;

        public Curve_MinTimeZero_OperatorCalculator(CurveCalculator_MinTimeZero curveCalculator)
        {
            if (curveCalculator == null) throw new NullException(() => curveCalculator);

            _curveCalculator = curveCalculator;
        }

        public override double Calculate(DimensionStack dimensionStack)
        {
            double time = dimensionStack.Get(DimensionEnum.Time);
            double transformedTime = time - _origin;
            double result = _curveCalculator.CalculateValue(transformedTime);
            return result;
        }

        public override void Reset(DimensionStack dimensionStack)
        {
            double time = dimensionStack.Get(DimensionEnum.Time);

            _origin = time;

            base.Reset(dimensionStack);
        }
    }
}
