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

            // Module phase
            double period = 1 / pitch;
            double relativePhase = _phase % period;
            if (relativePhase < 0)
            {
                relativePhase = period + relativePhase; // A subtraction in disguise.
            }

            // Make phase 1-based
            double normalizedPhase = relativePhase / period;

            // TODO: Mod something should take care of the sawtooth waveform.
            // You do not need trickery to get it to start at the top again:
            // you can do it algebraically with a mod.
            // But then a saw up would be better.
            // Then you would not need the relativePhase or the normalizedPhase.

            //double value = -1 + 2 /(_phase % period) * 2

            // Get value
            double value = 1 - 2 * normalizedPhase;

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
