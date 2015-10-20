using JJ.Framework.Reflection.Exceptions;
using System;
using JJ.Framework.Mathematics;

namespace JJ.Business.Synthesizer.Calculation.Operators
{
    internal class Sine_OperatorCalculator : OperatorCalculatorBase
    {
        private OperatorCalculatorBase _volumeCalculator;
        private OperatorCalculatorBase _pitchCalculator;

        private double _phase;
        private double _previousTime;

        public Sine_OperatorCalculator(OperatorCalculatorBase volumeCalculator, OperatorCalculatorBase pitchCalculator)
        {
            if (volumeCalculator == null) throw new NullException(() => volumeCalculator);
            if (pitchCalculator == null) throw new NullException(() => pitchCalculator);

            _volumeCalculator = volumeCalculator;
            _pitchCalculator = pitchCalculator;
        }

        public override double Calculate(double time, int channelIndex)
        {
            double volume = _volumeCalculator.Calculate(time, channelIndex);
            double pitch = _pitchCalculator.Calculate(time, channelIndex);

            double dt = time - _previousTime;
            _phase = _phase + Maths.TWO_PI * dt * pitch;
            _previousTime = time;

            double value = volume * Math.Sin(_phase);
            return value;
        }
    }
}
