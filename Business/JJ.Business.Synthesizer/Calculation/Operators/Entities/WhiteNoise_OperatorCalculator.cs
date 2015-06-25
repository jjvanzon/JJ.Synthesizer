using JJ.Framework.Mathematics;
using JJ.Framework.Reflection.Exceptions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JJ.Business.Synthesizer.Calculation.Operators.Entities
{
    internal class WhiteNoise_OperatorCalculator : OperatorCalculatorBase
    {
        private WhiteNoiseCalculator _whiteNoiseCalculator;

        /// <summary> Each operator should start at a different time offset in the pre-generated noise, to prevent artifacts. </summary>
        private double _offset;

        public WhiteNoise_OperatorCalculator(WhiteNoiseCalculator whiteNoiseCalculator)
        {
            if (whiteNoiseCalculator == null) throw new NullException(() => whiteNoiseCalculator);

            _whiteNoiseCalculator = whiteNoiseCalculator;
            _offset = whiteNoiseCalculator.GetRandomOffset();
        }

        public override double Calculate(double time, int channelIndex)
        {
            double x = _whiteNoiseCalculator.Calculate(time + _offset);
            return x;
        }
    }
}
