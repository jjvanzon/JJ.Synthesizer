using System;
using JJ.Business.Synthesizer.Enums;
using JJ.Framework.Reflection.Exceptions;

namespace JJ.Business.Synthesizer.Calculation.Operators
{
    internal class Triangle_WithConstFrequency_WithConstPhaseShift_OperatorCalculator : OperatorCalculatorBase
    {
        private readonly double _frequency;
        private readonly double _phaseShift;

        public Triangle_WithConstFrequency_WithConstPhaseShift_OperatorCalculator(double frequency, double phaseShift)
        {
            _frequency = frequency;
            _phaseShift = phaseShift;

            // Correct the phase shift, because our calculation starts with value -1, but in practice you want to start at value 0 going up.
            _phaseShift += 0.25;
        }

        public override double Calculate(DimensionStack dimensionStack)
        {
            double time = dimensionStack.Get(DimensionEnum.Time);

            double phase = time * _frequency + _phaseShift;
            double relativePhase = phase % 1.0;
            if (relativePhase < 0.5)
            {
                // Starts going up at a rate of 2 up over 1/2 a cycle.
                double value = -1.0 + 4.0 * relativePhase;
                return value;
            }
            else
            {
                // And then going down at phase 1/2.
                // (Extending the line to x = 0 it ends up at y = 3.)
                double value = 3.0 - 4.0 * relativePhase;
                return value;
            }
        }
    }

    internal class Triangle_WithConstFrequency_WithVarPhaseShift_OperatorCalculator : OperatorCalculatorBase_WithChildCalculators
    {
        private readonly double _frequency;
        private readonly OperatorCalculatorBase _phaseShiftCalculator;

        public Triangle_WithConstFrequency_WithVarPhaseShift_OperatorCalculator(
            double frequency,
            OperatorCalculatorBase phaseShiftCalculator)
            : base(new OperatorCalculatorBase[] { phaseShiftCalculator })
        {
            if (phaseShiftCalculator == null) throw new NullException(() => phaseShiftCalculator);
            if (phaseShiftCalculator is Number_OperatorCalculator) throw new IsTypeException<Number_OperatorCalculator>(() => phaseShiftCalculator);

            _frequency = frequency;
            _phaseShiftCalculator = phaseShiftCalculator;
        }

        public override double Calculate(DimensionStack dimensionStack)
        {
            double time = dimensionStack.Get(DimensionEnum.Time);

            double phaseShift = _phaseShiftCalculator.Calculate(dimensionStack);
            double phase = time * _frequency + phaseShift;

            // Correct the phase, because our calculation starts with value -1, but in practice you want to start at value 0 going up.
            phase += 0.25;

            double relativePhase = phase % 1.0;
            if (relativePhase < 0.5)
            {
                // Starts going up at a rate of 2 up over 1/2 a cycle.
                double value = -1.0 + 4.0 * relativePhase;
                return value;
            }
            else
            {
                // And then going down at phase 1/2.
                // (Extending the line to x = 0 it ends up at y = 3.)
                double value = 3.0 - 4.0 * relativePhase;
                return value;
            }
        }
    }

    internal class Triangle_WithVarFrequency_WithConstPhaseShift_OperatorCalculator : OperatorCalculatorBase_WithChildCalculators
    {
        private readonly OperatorCalculatorBase _frequencyCalculator;

        private double _phase;
        private double _previousTime;

        public Triangle_WithVarFrequency_WithConstPhaseShift_OperatorCalculator(
            OperatorCalculatorBase frequencyCalculator,
            double phaseShift)
            : base(new OperatorCalculatorBase[] { frequencyCalculator })
        {
            if (frequencyCalculator == null) throw new NullException(() => frequencyCalculator);
            if (frequencyCalculator is Number_OperatorCalculator) throw new IsTypeException<Number_OperatorCalculator>(() => frequencyCalculator);

            _frequencyCalculator = frequencyCalculator;

            // TODO: Assert so it does not become NaN.
            _phase = phaseShift;

            // Correct the phase, because our calculation starts with value -1, but in practice you want to start at value 0 going up.
            _phase += 0.25;
        }

        public override double Calculate(DimensionStack dimensionStack)
        {
            double time = dimensionStack.Get(DimensionEnum.Time);

            double frequency = _frequencyCalculator.Calculate(dimensionStack);

            double dt = time - _previousTime;
            double phase = _phase + dt * frequency;

            // Prevent phase from becoming a special number, rendering it unusable forever.
            if (Double.IsNaN(phase) || Double.IsInfinity(phase))
            {
                return Double.NaN;
            }
            _phase = phase;

            double value;
            double relativePhase = _phase % 1.0;
            if (relativePhase < 0.5)
            {
                // Starts going up at a rate of 2 up over 1/2 a cycle.
                value = -1.0 + 4.0 * relativePhase;
            }
            else
            {
                // And then going down at phase 1/2.
                // (Extending the line to x = 0 it ends up at y = 3.)
                value = 3.0 - 4.0 * relativePhase;
            }

            _previousTime = time;

            return value;
        }

        public override void Reset(DimensionStack dimensionStack)
        {
            double time = dimensionStack.Get(DimensionEnum.Time);

            _previousTime = time;
            _phase = 0.0;

            base.Reset(dimensionStack);
        }
    }

    internal class Triangle_WithVarFrequency_WithVarPhaseShift_OperatorCalculator : OperatorCalculatorBase_WithChildCalculators
    {
        private readonly OperatorCalculatorBase _frequencyCalculator;
        private readonly OperatorCalculatorBase _phaseShiftCalculator;

        private double _phase;
        private double _previousTime;

        public Triangle_WithVarFrequency_WithVarPhaseShift_OperatorCalculator(
            OperatorCalculatorBase frequencyCalculator,
            OperatorCalculatorBase phaseShiftCalculator)
            : base(new OperatorCalculatorBase[] { frequencyCalculator, phaseShiftCalculator })
        {
            if (frequencyCalculator == null) throw new NullException(() => frequencyCalculator);
            if (frequencyCalculator is Number_OperatorCalculator) throw new IsTypeException<Number_OperatorCalculator>(() => frequencyCalculator);
            if (phaseShiftCalculator == null) throw new NullException(() => phaseShiftCalculator);
            if (phaseShiftCalculator is Number_OperatorCalculator) throw new IsTypeException<Number_OperatorCalculator>(() => phaseShiftCalculator);

            _frequencyCalculator = frequencyCalculator;
            _phaseShiftCalculator = phaseShiftCalculator;

            // Correct the phase, because our calculation starts with value -1, but in practice you want to start at value 0 going up.
            _phase += 0.25;
        }

        public override double Calculate(DimensionStack dimensionStack)
        {
            double time = dimensionStack.Get(DimensionEnum.Time);

            double frequency = _frequencyCalculator.Calculate(dimensionStack);
            double phaseShift = _phaseShiftCalculator.Calculate(dimensionStack);

            double dt = time - _previousTime;
            double phase = _phase + dt * frequency;

            // Prevent phase from becoming a special number, rendering it unusable forever.
            if (Double.IsNaN(phase) || Double.IsInfinity(phase))
            {
                return Double.NaN;
            }
            _phase = phase;

            double shiftedPhase = _phase + phaseShift;
            double relativePhase = shiftedPhase % 1.0;
            double value;
            if (relativePhase < 0.5)
            {
                // Starts going up at a rate of 2 up over 1/2 a cycle.
                value = -1.0 + 4.0 * relativePhase;
            }
            else
            {
                // And then going down at phase 1/2.
                // (Extending the line to x = 0 it ends up at y = 3.)
                value = 3.0 - 4.0 * relativePhase;
            }

            _previousTime = time;

            return value;
        }

        public override void Reset(DimensionStack dimensionStack)
        {
            double time = dimensionStack.Get(DimensionEnum.Time);

            _previousTime = time;
            _phase = 0.0;

            base.Reset(dimensionStack);
        }
    }
}
