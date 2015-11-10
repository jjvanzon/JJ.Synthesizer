using JJ.Framework.Reflection.Exceptions;

namespace JJ.Business.Synthesizer.Calculation.Operators
{
    internal class WhiteNoise_OperatorCalculator : OperatorCalculatorBase
    {
        private WhiteNoiseCalculator _whiteNoiseCalculator;

        /// <summary> Each operator should start at a different time offset in the pre-generated noise, to prevent artifacts. </summary>
        private double _offset;

        public WhiteNoise_OperatorCalculator(WhiteNoiseCalculator whiteNoiseCalculator, double offset)
        {
            if (whiteNoiseCalculator == null) throw new NullException(() => whiteNoiseCalculator);

            _whiteNoiseCalculator = whiteNoiseCalculator;
            _offset = offset;
        }

        public override double Calculate(double time, int channelIndex)
        {
            double x = _whiteNoiseCalculator.GetValue(time + _offset);
            return x;
        }
    }
}
