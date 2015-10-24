using JJ.Framework.Reflection.Exceptions;
using System;
using JJ.Framework.Mathematics;

namespace JJ.Business.Synthesizer.Calculation.Operators
{
    internal class SquareWave_WithConstPitch_WithoutPhaseShift_OperatorCalculator : OperatorCalculatorBase
    {
        private readonly double _pitch;
        private double _phase;
        private double _previousTime;

        public SquareWave_WithConstPitch_WithoutPhaseShift_OperatorCalculator(double pitch)
        {
            if (pitch == 0) throw new ZeroException(() => pitch);

            _pitch = pitch;
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
}

