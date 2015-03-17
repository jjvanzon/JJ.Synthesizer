using JJ.Framework.Reflection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JJ.Business.Synthesizer.Calculation.Operators.Entities
{
    internal class Sine_WithPhaseStart_Calculator : OperatorCalculatorBase
    {
        private OperatorCalculatorBase _volumeCalculator;
        private OperatorCalculatorBase _pitchCalculator;
        private OperatorCalculatorBase _phaseStartCalculator;

        public Sine_WithPhaseStart_Calculator(
            OperatorCalculatorBase volumeCalculator, 
            OperatorCalculatorBase pitchCalculator, 
            OperatorCalculatorBase phaseStartCalculator)
        {
            if (volumeCalculator == null) throw new NullException(() => volumeCalculator);
            if (pitchCalculator == null) throw new NullException(() => pitchCalculator);
            if (phaseStartCalculator == null) throw new NullException(() => phaseStartCalculator);

            _volumeCalculator = volumeCalculator;
            _pitchCalculator = pitchCalculator;
            _phaseStartCalculator = phaseStartCalculator;
        }

        public override double Calculate(double time, int channelIndex)
        {
            double volume = _volumeCalculator.Calculate(time, channelIndex);
            double pitch = _pitchCalculator.Calculate(time, channelIndex);
            double phaseStart = _phaseStartCalculator.Calculate(time, channelIndex);

            double result = volume * Math.Sin(2 * (Math.PI * phaseStart + Math.PI * pitch * time));
            return result;
        }
    }
}
