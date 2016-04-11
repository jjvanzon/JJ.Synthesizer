using JJ.Framework.Reflection.Exceptions;
using JJ.Business.Synthesizer.Calculation.Curves;
using JJ.Business.Synthesizer.Enums;

namespace JJ.Business.Synthesizer.Calculation.Operators
{
    internal class Curve_MinX_OperatorCalculator : OperatorCalculatorBase
    {
        private readonly CurveCalculator_MinX _curveCalculator;
        private readonly int _xDimensionIndex;

        private double _origin;

        public Curve_MinX_OperatorCalculator(
            CurveCalculator_MinX curveCalculator,
            DimensionEnum xDimensionEnum)
        {
            if (curveCalculator == null) throw new NullException(() => curveCalculator);

            _curveCalculator = curveCalculator;
            _xDimensionIndex = (int)xDimensionEnum;
        }

        public override double Calculate(DimensionStack dimensionStack)
        {
            double x = dimensionStack.Get(_xDimensionIndex);

            double transformedX = x - _origin;
            double result = _curveCalculator.CalculateY(transformedX);
            return result;
        }

        public override void Reset(DimensionStack dimensionStack)
        {
            double x = dimensionStack.Get(_xDimensionIndex);

            _origin = x;

            base.Reset(dimensionStack);
        }
    }

    internal class Curve_MinXZero_OperatorCalculator : OperatorCalculatorBase
    {
        private readonly CurveCalculator_MinXZero _curveCalculator;
        private readonly int _xDimensionIndex;

        private double _origin;

        public Curve_MinXZero_OperatorCalculator(
            CurveCalculator_MinXZero curveCalculator,
            DimensionEnum xDimensionEnum)
        {
            if (curveCalculator == null) throw new NullException(() => curveCalculator);

            _curveCalculator = curveCalculator;
            _xDimensionIndex = (int)xDimensionEnum;
        }

        public override double Calculate(DimensionStack dimensionStack)
        {
            double x = dimensionStack.Get(_xDimensionIndex);
            double transformedX = x - _origin;
            double result = _curveCalculator.CalculateY(transformedX);
            return result;
        }

        public override void Reset(DimensionStack dimensionStack)
        {
            double x = dimensionStack.Get(_xDimensionIndex);

            _origin = x;

            base.Reset(dimensionStack);
        }
    }
}
