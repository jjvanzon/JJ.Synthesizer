using JJ.Framework.Reflection.Exceptions;
using System;

namespace JJ.Business.Synthesizer.Calculation.Operators
{
    internal class SawTooth_WithVarPitch_WithConstPhaseShift_OperatorCalculator : OperatorCalculatorBase
    {
        private readonly OperatorCalculatorBase _pitchCalculator;
        private readonly double _phaseShift;
        private double _phase;
        private double _previousTime;

        public SawTooth_WithVarPitch_WithConstPhaseShift_OperatorCalculator(
            OperatorCalculatorBase pitchCalculator,
            double phaseShift)
        {
            if (pitchCalculator == null) throw new NullException(() => pitchCalculator);
            if (pitchCalculator is Number_OperatorCalculator) throw new IsTypeException<Number_OperatorCalculator>(() => pitchCalculator);
            if (phaseShift == 0) throw new ZeroException(() => phaseShift);

            _pitchCalculator = pitchCalculator;
            _phaseShift = phaseShift;
        }

        public override double Calculate(double time, int channelIndex)
        {
            double pitch = _pitchCalculator.Calculate(time, channelIndex);

            double dt = time - _previousTime;
            _phase = _phase + dt * pitch;

            double shiftedPhase = _phase + _phaseShift;
            double value = -1 + (2 * shiftedPhase % 2);

            _previousTime = time;

            return value;
        }
    }
}
