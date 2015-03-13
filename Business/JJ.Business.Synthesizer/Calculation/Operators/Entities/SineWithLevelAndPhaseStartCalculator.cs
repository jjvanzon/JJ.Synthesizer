using JJ.Framework.Reflection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JJ.Business.Synthesizer.Calculation.Operators.Entities
{
    internal class SineWithLevelAndPhaseStartCalculator : OperatorCalculatorBase
    {
        private OperatorCalculatorBase _volumeCalculator;
        private OperatorCalculatorBase _pitchCalculator;
        private OperatorCalculatorBase _levelCalculator;
        private OperatorCalculatorBase _phaseStartCalculator;

        public SineWithLevelAndPhaseStartCalculator(
            OperatorCalculatorBase volumeCalculator, 
            OperatorCalculatorBase pitchCalculator, 
            OperatorCalculatorBase levelCalculator, 
            OperatorCalculatorBase phaseStartCalculator)
        {
            if (volumeCalculator == null) throw new NullException(() => volumeCalculator);
            if (pitchCalculator == null) throw new NullException(() => pitchCalculator);
            if (levelCalculator == null) throw new NullException(() => levelCalculator);
            if (phaseStartCalculator == null) throw new NullException(() => phaseStartCalculator);

            _volumeCalculator = volumeCalculator;
            _pitchCalculator = pitchCalculator;
            _levelCalculator = levelCalculator;
            _phaseStartCalculator = phaseStartCalculator;
        }

        public override double Calculate(double time, int channelIndex)
        {
            double volume = _volumeCalculator.Calculate(time, channelIndex);
            double pitch = _pitchCalculator.Calculate(time, channelIndex);
            double level = _levelCalculator.Calculate(time, channelIndex);
            double phaseStart = _phaseStartCalculator.Calculate(time, channelIndex);

            double result = level + volume * Math.Sin(2 * (Math.PI * phaseStart + Math.PI * pitch * time));
            return result;
        }
    }
}
