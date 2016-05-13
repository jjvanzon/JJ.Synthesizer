using JJ.Framework.Reflection.Exceptions;
using JJ.Business.Synthesizer.Calculation.Curves;
using JJ.Business.Synthesizer.Enums;
using System.Runtime.CompilerServices;

namespace JJ.Business.Synthesizer.Calculation.Operators
{
    internal class Curve_MinX_OperatorCalculator : OperatorCalculatorBase
    {
        private readonly CurveCalculator_MinX _curveCalculator;
        private readonly int _dimensionIndex;
        private readonly DimensionStacks _dimensionStack;

        private double _origin;

        public Curve_MinX_OperatorCalculator(
            CurveCalculator_MinX curveCalculator,
            DimensionEnum dimensionEnum,
            DimensionStacks dimensionStack)
        {
            if (curveCalculator == null) throw new NullException(() => curveCalculator);
            OperatorCalculatorHelper.AssertDimensionEnum(dimensionEnum);
            if (dimensionStack == null) throw new NullException(() => dimensionStack);

            _curveCalculator = curveCalculator;
            _dimensionIndex = (int)dimensionEnum;
            _dimensionStack = dimensionStack;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public override double Calculate()
        {
            double position = _dimensionStack.Get(_dimensionIndex);

            double transformedPosition = position - _origin;
            double result = _curveCalculator.CalculateY(transformedPosition);
            return result;
        }

        public override void Reset()
        {
            double position = _dimensionStack.Get(_dimensionIndex);

            _origin = position;

            base.Reset();
        }
    }

    internal class Curve_MinXZero_OperatorCalculator : OperatorCalculatorBase
    {
        private readonly CurveCalculator_MinXZero _curveCalculator;
        private readonly int _dimensionIndex;
        private readonly DimensionStacks _dimensionStack;

        private double _origin;

        public Curve_MinXZero_OperatorCalculator(
            CurveCalculator_MinXZero curveCalculator,
            DimensionEnum dimensionEnum,
            DimensionStacks dimensionStack)
        {
            if (curveCalculator == null) throw new NullException(() => curveCalculator);
            OperatorCalculatorHelper.AssertDimensionEnum(dimensionEnum);
            if (dimensionStack == null) throw new NullException(() => dimensionStack);

            _curveCalculator = curveCalculator;
            _dimensionIndex = (int)dimensionEnum;
            _dimensionStack = dimensionStack;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public override double Calculate()
        {
            double position = _dimensionStack.Get(_dimensionIndex);
            double transformedPosition = position - _origin;
            double result = _curveCalculator.CalculateY(transformedPosition);
            return result;
        }

        public override void Reset()
        {
            double position = _dimensionStack.Get(_dimensionIndex);

            _origin = position;

            base.Reset();
        }
    }
}
