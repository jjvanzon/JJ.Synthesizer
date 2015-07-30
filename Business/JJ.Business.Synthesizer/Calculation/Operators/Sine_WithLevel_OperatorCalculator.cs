using JJ.Framework.Reflection.Exceptions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JJ.Business.Synthesizer.Calculation.Operators
{
    internal class Sine_WithLevel_OperatorCalculator : OperatorCalculatorBase
    {
        private OperatorCalculatorBase _volumeCalculator;
        private OperatorCalculatorBase _pitchCalculator;
        private OperatorCalculatorBase _levelCalculator;

        public Sine_WithLevel_OperatorCalculator(
            OperatorCalculatorBase volumeCalculator, 
            OperatorCalculatorBase pitchCalculator, 
            OperatorCalculatorBase levelCalculator)
        {
            if (volumeCalculator == null) throw new NullException(() => volumeCalculator);
            if (pitchCalculator == null) throw new NullException(() => pitchCalculator);
            if (levelCalculator == null) throw new NullException(() => levelCalculator);

            _volumeCalculator = volumeCalculator;
            _pitchCalculator = pitchCalculator;
            _levelCalculator = levelCalculator;
        }
            
        public override double Calculate(double time, int channelIndex)
        {
            double volume = _volumeCalculator.Calculate(time, channelIndex);
            double pitch = _pitchCalculator.Calculate(time, channelIndex); 
            double level = _levelCalculator.Calculate(time, channelIndex);

            return level + volume * Math.Sin(2 * Math.PI * pitch * time);
        }
    }
}
