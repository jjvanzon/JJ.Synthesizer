using JJ.Framework.Reflection.Exceptions;
using JJ.Business.Synthesizer.Calculation.Curves;
using JJ.Business.Synthesizer.Enums;
using System.Runtime.CompilerServices;

namespace JJ.Business.Synthesizer.Calculation.Operators
{
    internal class Curve_MinX_OperatorCalculator : OperatorCalculatorBase
    {
        private readonly CurveCalculator_MinX _curveCalculator;
        private readonly DimensionStack _dimensionStack;
        private readonly int _dimensionStackIndex;
        private double _origin;

        public Curve_MinX_OperatorCalculator(CurveCalculator_MinX curveCalculator, DimensionStack dimensionStack)
        {
            if (curveCalculator == null) throw new NullException(() => curveCalculator);
            OperatorCalculatorHelper.AssertDimensionStack_ForReaders(dimensionStack);

            _curveCalculator = curveCalculator;
            _dimensionStack = dimensionStack;
            _dimensionStackIndex = dimensionStack.CurrentIndex;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public override double Calculate()
        {
            double position = _dimensionStack.Get(_dimensionStackIndex);

            double transformedPosition = position - _origin;
            double result = _curveCalculator.CalculateY(transformedPosition);
            return result;
        }

        public override void Reset()
        {
            double position = _dimensionStack.Get(_dimensionStackIndex);

            _origin = position;

            base.Reset();
        }
    }

    internal class Curve_MinXZero_OperatorCalculator : OperatorCalculatorBase
    {
        private readonly CurveCalculator_MinXZero _curveCalculator;
        private readonly DimensionStack _dimensionStack;
        private readonly int _dimensionStackIndex;
        private double _origin;

        public Curve_MinXZero_OperatorCalculator(CurveCalculator_MinXZero curveCalculator, DimensionStack dimensionStack)
        {
            if (curveCalculator == null) throw new NullException(() => curveCalculator);
            OperatorCalculatorHelper.AssertDimensionStack_ForReaders(dimensionStack);

            _curveCalculator = curveCalculator;
            _dimensionStack = dimensionStack;
            _dimensionStackIndex = dimensionStack.CurrentIndex;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public override double Calculate()
        {
            double position = _dimensionStack.Get(_dimensionStackIndex);
            double transformedPosition = position - _origin;
            double result = _curveCalculator.CalculateY(transformedPosition);
            return result;
        }

        public override void Reset()
        {
            double position = _dimensionStack.Get(_dimensionStackIndex);

            _origin = position;

            base.Reset();
        }
    }
}
