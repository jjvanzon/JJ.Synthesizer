using JJ.Framework.Reflection.Exceptions;
using System;
using JJ.Framework.Mathematics;

namespace JJ.Business.Synthesizer.Calculation.Operators
{
    internal class SawTooth_WithoutPhaseShift_OperatorCalculator : OperatorCalculatorBase
    {
        private OperatorCalculatorBase _pitchCalculator;

        private double _phase;
        private double _previousTime;

        public SawTooth_WithoutPhaseShift_OperatorCalculator(OperatorCalculatorBase pitchCalculator)
        {
            if (pitchCalculator == null) throw new NullException(() => pitchCalculator);

            _pitchCalculator = pitchCalculator;
        }

        public override double Calculate(double time, int channelIndex)
        {
            double pitch = _pitchCalculator.Calculate(time, channelIndex);

            double dt = time - _previousTime;
            _phase = _phase + dt * pitch;

            double period = 1 / pitch;

            double timeInCycle = _phase % period;
            if (timeInCycle < 0)
            {
                timeInCycle = period + timeInCycle; // A subtraction in disguise.
            }

            double positionInCycle = timeInCycle / period;

            double value = 1 - 2 * positionInCycle;

            _previousTime = time;

            return value;
        }

        // TODO: Remove outcommented code.
        //private double GetSample(double time)
        //{
        //    // This is the formula that would work with t between 0 and 1, with a frequency of 1 hertz
        //    double value = 1 - 2 * time;
        //    return value;
        //}
    }
}
