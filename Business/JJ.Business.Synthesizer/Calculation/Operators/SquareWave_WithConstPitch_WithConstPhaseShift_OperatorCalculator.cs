using JJ.Framework.Reflection.Exceptions;
using System;
using JJ.Framework.Mathematics;

namespace JJ.Business.Synthesizer.Calculation.Operators
{
    internal class SquareWave_WithConstPitch_WithConstPhaseShift_OperatorCalculator : OperatorCalculatorBase
    {
        private readonly double _pitch;
        private readonly double _phaseShift;
        private double _phase;
        private double _previousTime;

        public SquareWave_WithConstPitch_WithConstPhaseShift_OperatorCalculator(
            double pitch,
            double phaseShift)
        {
            _pitch = pitch;
            _phaseShift = phaseShift;
        }

        public override double Calculate(double time, int channelIndex)
        {
            double dt = time - _previousTime;
            _phase = _phase + dt * _pitch;

            double value;
            double shiftedPhase = _phase + _phaseShift;
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
