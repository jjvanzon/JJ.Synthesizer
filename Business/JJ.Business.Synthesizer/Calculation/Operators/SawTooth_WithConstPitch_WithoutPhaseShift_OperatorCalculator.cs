using JJ.Framework.Reflection.Exceptions;
using System;
using JJ.Framework.Mathematics;

namespace JJ.Business.Synthesizer.Calculation.Operators
{
    internal class SawTooth_WithConstPitch_WithoutPhaseShift_OperatorCalculator : OperatorCalculatorBase
    {
        private readonly double _pitch;
        private double _phase;
        private double _previousTime;

        public SawTooth_WithConstPitch_WithoutPhaseShift_OperatorCalculator(double pitch)
        {
            _pitch = pitch;
        }

        public override double Calculate(double time, int channelIndex)
        {
            double dt = time - _previousTime;
            _phase = _phase + dt * _pitch;

            double value = -1 + (2 * _phase % 2);

            _previousTime = time;

            return value;
        }
    }
}

