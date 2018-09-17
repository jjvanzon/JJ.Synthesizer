using System.Runtime.CompilerServices;
using JJ.Business.Synthesizer.Calculation.Arrays;
using JJ.Framework.Exceptions.Basic;

namespace JJ.Business.Synthesizer.Calculation.Operators
{
    internal class Curve_OperatorCalculator_MinX_NoOriginShifting : OperatorCalculatorBase_WithChildCalculators
    {
        private readonly OperatorCalculatorBase _positionCalculator;
        private readonly ArrayCalculator_MinPosition_Line _underlyingArrayCalculator;

        public Curve_OperatorCalculator_MinX_NoOriginShifting(
            OperatorCalculatorBase positionCalculator,
            ArrayCalculator_MinPosition_Line underlyingArrayCalculator)
            : base(new[] { positionCalculator })
        {
            _positionCalculator = positionCalculator;
            _underlyingArrayCalculator = underlyingArrayCalculator ?? throw new NullException(() => underlyingArrayCalculator);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public override double Calculate()
        {
            double position = _positionCalculator.Calculate();
            double value = _underlyingArrayCalculator.Calculate(position);
            return value;
        }
    }

    internal class Curve_OperatorCalculator_MinX_WithOriginShifting : OperatorCalculatorBase_WithChildCalculators
    {
        private readonly OperatorCalculatorBase _positionCalculator;
        private readonly ArrayCalculator_MinPosition_Line _underlyingArrayCalculator;

        private double _origin;

        public Curve_OperatorCalculator_MinX_WithOriginShifting(
            OperatorCalculatorBase positionCalculator,
            ArrayCalculator_MinPosition_Line underlyingArrayCalculator)
            : base(new[] { positionCalculator })
        {
            _positionCalculator = positionCalculator;
            _underlyingArrayCalculator = underlyingArrayCalculator ?? throw new NullException(() => underlyingArrayCalculator);

            ResetPrivate();
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public override double Calculate()
        {
            double position = _positionCalculator.Calculate();
            double phase = position - _origin;
            double value = _underlyingArrayCalculator.Calculate(phase);
            return value;
        }

        public override void Reset() => ResetPrivate();
        private void ResetPrivate() => _origin = _positionCalculator.Calculate();
    }

    internal class Curve_OperatorCalculator_MinXZero_NoOriginShifting : OperatorCalculatorBase_WithChildCalculators
    {
        private readonly OperatorCalculatorBase _positionCalculator;
        private readonly ArrayCalculator_MinPositionZero_Line _underlyingArrayCalculator;

        public Curve_OperatorCalculator_MinXZero_NoOriginShifting(
            OperatorCalculatorBase positionCalculator,
            ArrayCalculator_MinPositionZero_Line underlyingArrayCalculator)
            : base(new[] { positionCalculator })
        {
            _positionCalculator = positionCalculator;
            _underlyingArrayCalculator = underlyingArrayCalculator ?? throw new NullException(() => underlyingArrayCalculator);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public override double Calculate()
        {
            double position = _positionCalculator.Calculate();
            double value = _underlyingArrayCalculator.Calculate(position);
            return value;
        }
    }

    internal class Curve_OperatorCalculator_MinXZero_WithOriginShifting : OperatorCalculatorBase_WithChildCalculators
    {
        private readonly OperatorCalculatorBase _positionCalculator;
        private readonly ArrayCalculator_MinPositionZero_Line _underlyingArrayCalculator;

        private double _origin;

        public Curve_OperatorCalculator_MinXZero_WithOriginShifting(
            OperatorCalculatorBase positionCalculator,
            ArrayCalculator_MinPositionZero_Line underlyingArrayCalculator)
            : base(new[] { positionCalculator })
        {
            _positionCalculator = positionCalculator;
            _underlyingArrayCalculator = underlyingArrayCalculator ?? throw new NullException(() => underlyingArrayCalculator);

            ResetPrivate();
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public override double Calculate()
        {
            double position = _positionCalculator.Calculate();
            double phase = position - _origin;
            double value = _underlyingArrayCalculator.Calculate(phase);
            return value;
        }

        public override void Reset() => ResetPrivate();
        private void ResetPrivate() => _origin = _positionCalculator.Calculate();
    }
}