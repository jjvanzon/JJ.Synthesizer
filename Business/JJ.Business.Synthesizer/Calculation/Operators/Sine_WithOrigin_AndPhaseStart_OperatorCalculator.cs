using JJ.Framework.Reflection.Exceptions;
using System;

namespace JJ.Business.Synthesizer.Calculation.Operators
{
    internal class Sine_WithOrigin_AndPhaseStart_OperatorCalculator : OperatorCalculatorBase
    {
        private OperatorCalculatorBase _volumeCalculator;
        private OperatorCalculatorBase _pitchCalculator;
        private OperatorCalculatorBase _originCalculator;
        private OperatorCalculatorBase _phaseStartCalculator;

        public Sine_WithOrigin_AndPhaseStart_OperatorCalculator(
            OperatorCalculatorBase volumeCalculator, 
            OperatorCalculatorBase pitchCalculator, 
            OperatorCalculatorBase originCalculator, 
            OperatorCalculatorBase phaseStartCalculator)
        {
            if (volumeCalculator == null) throw new NullException(() => volumeCalculator);
            if (pitchCalculator == null) throw new NullException(() => pitchCalculator);
            if (originCalculator == null) throw new NullException(() => originCalculator);
            if (phaseStartCalculator == null) throw new NullException(() => phaseStartCalculator);

            _volumeCalculator = volumeCalculator;
            _pitchCalculator = pitchCalculator;
            _originCalculator = originCalculator;
            _phaseStartCalculator = phaseStartCalculator;
        }

        public override double Calculate(double time, int channelIndex)
        {
            double volume = _volumeCalculator.Calculate(time, channelIndex);
            double pitch = _pitchCalculator.Calculate(time, channelIndex);
            double origin = _originCalculator.Calculate(time, channelIndex);
            double phaseStart = _phaseStartCalculator.Calculate(time, channelIndex);

            double result = origin + volume * Math.Sin(2 * (Math.PI * phaseStart + Math.PI * pitch * time));
            return result;
        }
    }
}
