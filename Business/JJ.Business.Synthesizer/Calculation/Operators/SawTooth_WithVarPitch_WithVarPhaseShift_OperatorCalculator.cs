using JJ.Framework.Reflection.Exceptions;
using System;
using JJ.Framework.Mathematics;

namespace JJ.Business.Synthesizer.Calculation.Operators
{
    internal class SawTooth_WithVarPitch_WithVarPhaseShift_OperatorCalculator : OperatorCalculatorBase
    {
        private OperatorCalculatorBase _pitchCalculator;
        private OperatorCalculatorBase _phaseShiftCalculator;

        private double _phase;
        private double _previousTime;

        public SawTooth_WithVarPitch_WithVarPhaseShift_OperatorCalculator(
            OperatorCalculatorBase pitchCalculator,
            OperatorCalculatorBase phaseShiftCalculator)
        {
            if (pitchCalculator == null) throw new NullException(() => pitchCalculator);
            if (phaseShiftCalculator == null) throw new NullException(() => phaseShiftCalculator);

            _pitchCalculator = pitchCalculator;
            _phaseShiftCalculator = phaseShiftCalculator;
        }

        public override double Calculate(double time, int channelIndex)
        {
            double pitch = _pitchCalculator.Calculate(time, channelIndex);
            double phaseShift = _phaseShiftCalculator.Calculate(time, channelIndex);

            double dt = time - _previousTime;
            _phase = _phase + dt * pitch;

            double shiftedPhase = _phase + phaseShift;
            double value = -1 + (2 * shiftedPhase % 2);

            _previousTime = time;

            return value;
        }
    }
}
