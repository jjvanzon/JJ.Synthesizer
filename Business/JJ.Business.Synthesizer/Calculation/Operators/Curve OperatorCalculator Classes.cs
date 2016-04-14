using JJ.Framework.Reflection.Exceptions;
using JJ.Business.Synthesizer.Calculation.Curves;
using JJ.Business.Synthesizer.Enums;

namespace JJ.Business.Synthesizer.Calculation.Operators
{
    internal class Curve_MinX_OperatorCalculator : OperatorCalculatorBase
    {
        private readonly CurveCalculator_MinX _curveCalculator;
        private readonly int _dimensionIndex;

        private double _origin;

        public Curve_MinX_OperatorCalculator(
            CurveCalculator_MinX curveCalculator,
            DimensionEnum dimensionEnum)
        {
            if (curveCalculator == null) throw new NullException(() => curveCalculator);

            _curveCalculator = curveCalculator;
            _dimensionIndex = (int)dimensionEnum;
        }

        public override double Calculate(DimensionStack dimensionStack)
        {
            double position = dimensionStack.Get(_dimensionIndex);

            double transformedPosition = position - _origin;
            double result = _curveCalculator.CalculateY(transformedPosition);
            return result;
        }

        public override void Reset(DimensionStack dimensionStack)
        {
            double position = dimensionStack.Get(_dimensionIndex);

            _origin = position;

            base.Reset(dimensionStack);
        }
    }

    internal class Curve_MinXZero_OperatorCalculator : OperatorCalculatorBase
    {
        private readonly CurveCalculator_MinXZero _curveCalculator;
        private readonly int _dimensionIndex;

        private double _origin;

        public Curve_MinXZero_OperatorCalculator(
            CurveCalculator_MinXZero curveCalculator,
            DimensionEnum dimensionEnum)
        {
            if (curveCalculator == null) throw new NullException(() => curveCalculator);

            _curveCalculator = curveCalculator;
            _dimensionIndex = (int)dimensionEnum;
        }

        public override double Calculate(DimensionStack dimensionStack)
        {
            double position = dimensionStack.Get(_dimensionIndex);
            double transformedPosition = position - _origin;
            double result = _curveCalculator.CalculateY(transformedPosition);
            return result;
        }

        public override void Reset(DimensionStack dimensionStack)
        {
            double position = dimensionStack.Get(_dimensionIndex);

            _origin = position;

            base.Reset(dimensionStack);
        }
    }
}
