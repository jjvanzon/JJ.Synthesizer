using JJ.Framework.Reflection.Exceptions;
using System;
using JJ.Framework.Mathematics;

namespace JJ.Business.Synthesizer.Calculation.Operators
{
    internal class SawTooth_WithPhaseShift_OperatorCalculator : OperatorCalculatorBase
    {
        private OperatorCalculatorBase _pitchCalculator;
        private OperatorCalculatorBase _phaseShiftCalculator;

        private double _phase;
        private double _previousTime;

        public SawTooth_WithPhaseShift_OperatorCalculator(OperatorCalculatorBase pitchCalculator, OperatorCalculatorBase phaseShiftCalculator)
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
            _phase = _phase + Maths.TWO_PI * dt * pitch;
            _previousTime = time;

            //double value = Math.Sin(Maths.TWO_PI * phaseShift + _phase);
            //return value;

            throw new NotImplementedException();
        }
    }
}
