using JJ.Framework.Reflection.Exceptions;
using System;
using JJ.Framework.Mathematics;

namespace JJ.Business.Synthesizer.Calculation.Operators
{
    internal class SquareWave_WithConstPitch_WithConstPhaseShift_OperatorCalculator : OperatorCalculatorBase
    {
        private readonly double _pitch;
        private double _phase;
        private double _previousTime;

        public SquareWave_WithConstPitch_WithConstPhaseShift_OperatorCalculator(double pitch, double phaseShift)
        {
            if (pitch == 0) throw new ZeroException(() => pitch);

            _pitch = pitch;
            _phase = phaseShift;
        }

        public override double Calculate(double time, int channelIndex)
        {
            double dt = time - _previousTime;
            _phase = _phase + dt * _pitch;

            double value;
            double relativePhase = _phase % 1;
            if (relativePhase < 0.5)
            {
                value = -1;
            }
            else
            {
                value = 1;
            }

            _previousTime = time;

            return value;
        }
    }

    internal class SquareWave_WithConstPitch_WithVarPhaseShift_OperatorCalculator : OperatorCalculatorBase
    {
        private readonly double _pitch;
        private readonly OperatorCalculatorBase _phaseShiftCalculator;
        private double _phase;
        private double _previousTime;

        public SquareWave_WithConstPitch_WithVarPhaseShift_OperatorCalculator(
            double pitch,
            OperatorCalculatorBase phaseShiftCalculator)
        {
            if (pitch == 0) throw new ZeroException(() => pitch);
            if (phaseShiftCalculator == null) throw new NullException(() => phaseShiftCalculator);
            if (phaseShiftCalculator is Number_OperatorCalculator) throw new IsTypeException<Number_OperatorCalculator>(() => phaseShiftCalculator);

            _pitch = pitch;
            _phaseShiftCalculator = phaseShiftCalculator;
        }

        public override double Calculate(double time, int channelIndex)
        {
            double phaseShift = _phaseShiftCalculator.Calculate(time, channelIndex);

            double dt = time - _previousTime;
            _phase = _phase + dt * _pitch;

            double value;
            double shiftedPhase = _phase + phaseShift;
            double relativePhase = shiftedPhase % 1;
            if (relativePhase < 0.5)
            {
                value = -1;
            }
            else
            {
                value = 1;
            }

            _previousTime = time;

            return value;
        }
    }

    internal class SquareWave_WithVarPitch_WithConstPhaseShift_OperatorCalculator : OperatorCalculatorBase
    {
        private readonly OperatorCalculatorBase _pitchCalculator;
        private double _phase;
        private double _previousTime;

        public SquareWave_WithVarPitch_WithConstPhaseShift_OperatorCalculator(
            OperatorCalculatorBase pitchCalculator,
            double phaseShift)
        {
            if (pitchCalculator == null) throw new NullException(() => pitchCalculator);
            if (pitchCalculator is Number_OperatorCalculator) throw new IsTypeException<Number_OperatorCalculator>(() => pitchCalculator);

            _pitchCalculator = pitchCalculator;
            _phase = phaseShift;
        }

        public override double Calculate(double time, int channelIndex)
        {
            double pitch = _pitchCalculator.Calculate(time, channelIndex);

            double dt = time - _previousTime;
            _phase = _phase + dt * pitch;

            double value;
            double relativePhase = _phase % 1;
            if (relativePhase < 0.5)
            {
                value = -1;
            }
            else
            {
                value = 1;
            }

            _previousTime = time;

            return value;
        }
    }

    internal class SquareWave_WithVarPitch_WithVarPhaseShift_OperatorCalculator : OperatorCalculatorBase
    {
        private readonly OperatorCalculatorBase _pitchCalculator;
        private readonly OperatorCalculatorBase _phaseShiftCalculator;
        private double _phase;
        private double _previousTime;

        public SquareWave_WithVarPitch_WithVarPhaseShift_OperatorCalculator(
            OperatorCalculatorBase pitchCalculator,
            OperatorCalculatorBase phaseShiftCalculator)
        {
            if (pitchCalculator == null) throw new NullException(() => pitchCalculator);
            if (pitchCalculator is Number_OperatorCalculator) throw new IsTypeException<Number_OperatorCalculator>(() => pitchCalculator);
            if (phaseShiftCalculator == null) throw new NullException(() => phaseShiftCalculator);
            if (phaseShiftCalculator is Number_OperatorCalculator) throw new IsTypeException<Number_OperatorCalculator>(() => phaseShiftCalculator);

            _pitchCalculator = pitchCalculator;
            _phaseShiftCalculator = phaseShiftCalculator;
        }

        public override double Calculate(double time, int channelIndex)
        {
            double pitch = _pitchCalculator.Calculate(time, channelIndex);
            double phaseShift = _phaseShiftCalculator.Calculate(time, channelIndex);

            double dt = time - _previousTime;
            _phase = _phase + dt * pitch;

            double value;
            double shiftedPhase = _phase + phaseShift;
            double relativePhase = shiftedPhase % 1;
            if (relativePhase < 0.5)
            {
                value = -1;
            }
            else
            {
                value = 1;
            }

            _previousTime = time;

            return value;
        }
    }
}
