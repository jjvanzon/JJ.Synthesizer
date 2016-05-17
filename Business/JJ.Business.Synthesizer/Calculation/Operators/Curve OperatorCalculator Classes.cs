using JJ.Framework.Reflection.Exceptions;
using JJ.Business.Synthesizer.Calculation.Curves;
using System.Runtime.CompilerServices;

namespace JJ.Business.Synthesizer.Calculation.Operators
{
    internal class Curve_OperatorCalculator_NoPhaseTracking_MinX : OperatorCalculatorBase
    {
        private readonly CurveCalculator_MinX _curveCalculator;
        private readonly DimensionStack _dimensionStack;
        private readonly int _dimensionStackIndex;

        public Curve_OperatorCalculator_NoPhaseTracking_MinX(CurveCalculator_MinX curveCalculator, DimensionStack dimensionStack)
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

            double value = _curveCalculator.CalculateY(position);

            return value;
        }
    }

    internal class Curve_OperatorCalculator_PhaseTracking_MinX : OperatorCalculatorBase
    {
        private readonly CurveCalculator_MinX _curveCalculator;
        private readonly DimensionStack _dimensionStack;
        private readonly int _dimensionStackIndex;

        private double _previousPosition;
        private double _phase;

        public Curve_OperatorCalculator_PhaseTracking_MinX(CurveCalculator_MinX curveCalculator, DimensionStack dimensionStack)
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

            double positionChange = position - _previousPosition;

            _phase = _phase + positionChange;

            double value = _curveCalculator.CalculateY(_phase);

            _previousPosition = position;

            return value;
        }

        public override void Reset()
        {
            double position = _dimensionStack.Get(_dimensionStackIndex);

            _previousPosition = position;
            _phase = 0.0;

            base.Reset();
        }
    }

    internal class Curve_OperatorCalculator_NoPhaseTracking_MinXZero : OperatorCalculatorBase
    {
        private readonly CurveCalculator_MinXZero _curveCalculator;
        private readonly DimensionStack _dimensionStack;
        private readonly int _dimensionStackIndex;

        public Curve_OperatorCalculator_NoPhaseTracking_MinXZero(CurveCalculator_MinXZero curveCalculator, DimensionStack dimensionStack)
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
            double value = _curveCalculator.CalculateY(position);
            return value;
        }
    }

    internal class Curve_OperatorCalculator_PhaseTracking_MinXZero : OperatorCalculatorBase
    {
        private readonly CurveCalculator_MinXZero _curveCalculator;
        private readonly DimensionStack _dimensionStack;
        private readonly int _dimensionStackIndex;

        private double _phase;
        private double _previousPosition;

        public Curve_OperatorCalculator_PhaseTracking_MinXZero(CurveCalculator_MinXZero curveCalculator, DimensionStack dimensionStack)
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

            double positionChange = position - _previousPosition;

            _phase = _phase + positionChange;

            double value = _curveCalculator.CalculateY(_phase);

            _previousPosition = position;

            return value;
        }

        public override void Reset()
        {
            double position = _dimensionStack.Get(_dimensionStackIndex);

            _previousPosition = position;
            _phase = 0.0;

            base.Reset();
        }
    }
}
