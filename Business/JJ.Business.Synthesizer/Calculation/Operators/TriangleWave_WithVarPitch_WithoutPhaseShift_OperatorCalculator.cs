using JJ.Framework.Reflection.Exceptions;
using System;
using JJ.Framework.Mathematics;

namespace JJ.Business.Synthesizer.Calculation.Operators
{
    internal class TriangleWave_WithVarPitch_WithoutPhaseShift_OperatorCalculator : OperatorCalculatorBase
    {
        private readonly OperatorCalculatorBase _pitchCalculator;
        private double _phase;
        private double _previousTime;

        public TriangleWave_WithVarPitch_WithoutPhaseShift_OperatorCalculator(OperatorCalculatorBase pitchCalculator)
        {
            if (pitchCalculator == null) throw new NullException(() => pitchCalculator);
            //if (pitchCalculator is Number_OperatorCalculator) throw new IsTypeException<Number_OperatorCalculator>(() => pitchCalculator);

            _pitchCalculator = pitchCalculator;
        }

        public override double Calculate(double time, int channelIndex)
        {
            double pitch = _pitchCalculator.Calculate(time, channelIndex);

            double dt = time - _previousTime;
            _phase = _phase + dt * pitch;

            double value;
            double relativePhase = _phase % 1.0;
            if (relativePhase < 0.5)
            {
                // Starts going up at phase -1/4 at a rate of 2 up over 1/2 a cycle.
                value = -1.0 + 4.0 * (relativePhase - 1.0 / 4.0);
            }
            else
            {
                // And then going down at phase 1/4.
                value = 1.0 - 4.0 * (relativePhase + 1.0 / 4.0);
            }

            _previousTime = time;

            return value;
        }
    }
}

