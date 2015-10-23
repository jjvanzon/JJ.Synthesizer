using JJ.Framework.Reflection.Exceptions;
using System;

namespace JJ.Business.Synthesizer.Calculation.Operators
{
    internal class SquareWave_WithVarPitch_WithConstPhaseShift_OperatorCalculator : OperatorCalculatorBase
    {
        private readonly OperatorCalculatorBase _pitchCalculator;
        private readonly double _phaseShift;
        private double _phase;
        private double _previousTime;

        public SquareWave_WithVarPitch_WithConstPhaseShift_OperatorCalculator(
            OperatorCalculatorBase pitchCalculator,
            double phaseShift)
        {
            if (pitchCalculator == null) throw new NullException(() => pitchCalculator);

            _pitchCalculator = pitchCalculator;
            _phaseShift = phaseShift;
        }

        public override double Calculate(double time, int channelIndex)
        {
            double pitch = _pitchCalculator.Calculate(time, channelIndex);

            double dt = time - _previousTime;
            _phase = _phase + dt * pitch;

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
