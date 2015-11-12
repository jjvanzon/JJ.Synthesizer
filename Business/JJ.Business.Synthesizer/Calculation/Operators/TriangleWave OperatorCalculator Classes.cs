using System;
using JJ.Framework.Reflection.Exceptions;

namespace JJ.Business.Synthesizer.Calculation.Operators
{
    internal class TriangleWave_WithConstFrequency_WithConstPhaseShift_OperatorCalculator : OperatorCalculatorBase
    {
        private readonly double _frequency;
        private double _phase;
        private double _previousTime;

        public TriangleWave_WithConstFrequency_WithConstPhaseShift_OperatorCalculator(double frequency, double phaseShift)
        {
            _frequency = frequency;
            _phase = phaseShift;

            // Correct the phase, because our calculation starts with value -1, but in practice you want to start at value 0 going up.
            _phase += 0.25;
        }

        public override double Calculate(double time, int channelIndex)
        {
            double dt = time - _previousTime;
            _phase = _phase + dt * _frequency;

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
    }

    internal class TriangleWave_WithConstFrequency_WithVarPhaseShift_OperatorCalculator : OperatorCalculatorBase
    {
        private readonly double _frequency;
        private readonly OperatorCalculatorBase _phaseShiftCalculator;
        private double _phase;
        private double _previousTime;

        public TriangleWave_WithConstFrequency_WithVarPhaseShift_OperatorCalculator(
            double frequency,
            OperatorCalculatorBase phaseShiftCalculator)
        {
            if (phaseShiftCalculator == null) throw new NullException(() => phaseShiftCalculator);
            if (phaseShiftCalculator is Number_OperatorCalculator) throw new IsTypeException<Number_OperatorCalculator>(() => phaseShiftCalculator);

            _frequency = frequency;
            _phaseShiftCalculator = phaseShiftCalculator;

            // Correct the phase, because our calculation starts with value -1, but in practice you want to start at value 0 going up.
            _phase = 0.25;
        }

        public override double Calculate(double time, int channelIndex)
        {
            double phaseShift = _phaseShiftCalculator.Calculate(time, channelIndex);

            double dt = time - _previousTime;
            _phase = _phase + dt * _frequency;

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
    }

    internal class TriangleWave_WithVarFrequency_WithConstPhaseShift_OperatorCalculator : OperatorCalculatorBase
    {
        private readonly OperatorCalculatorBase _frequencyCalculator;
        private double _phase;
        private double _previousTime;

        public TriangleWave_WithVarFrequency_WithConstPhaseShift_OperatorCalculator(
            OperatorCalculatorBase frequencyCalculator,
            double phaseShift)
        {
            if (frequencyCalculator == null) throw new NullException(() => frequencyCalculator);
            if (frequencyCalculator is Number_OperatorCalculator) throw new IsTypeException<Number_OperatorCalculator>(() => frequencyCalculator);

            _frequencyCalculator = frequencyCalculator;
            _phase = phaseShift;

            // Correct the phase, because our calculation starts with value -1, but in practice you want to start at value 0 going up.
            _phase += 0.25;
        }

        public override double Calculate(double time, int channelIndex)
        {
            double frequency = _frequencyCalculator.Calculate(time, channelIndex);

            double dt = time - _previousTime;
            _phase = _phase + dt * frequency;

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
    }

    internal class TriangleWave_WithVarFrequency_WithVarPhaseShift_OperatorCalculator : OperatorCalculatorBase
    {
        private readonly OperatorCalculatorBase _frequencyCalculator;
        private readonly OperatorCalculatorBase _phaseShiftCalculator;
        private double _phase;
        private double _previousTime;

        public TriangleWave_WithVarFrequency_WithVarPhaseShift_OperatorCalculator(
            OperatorCalculatorBase frequencyCalculator,
            OperatorCalculatorBase phaseShiftCalculator)
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

        public override double Calculate(double time, int channelIndex)
        {
            double frequency = _frequencyCalculator.Calculate(time, channelIndex);
            double phaseShift = _phaseShiftCalculator.Calculate(time, channelIndex);

            double dt = time - _previousTime;
            _phase = _phase + dt * frequency;

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
    }
}