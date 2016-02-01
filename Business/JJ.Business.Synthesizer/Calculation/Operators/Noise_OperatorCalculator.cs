using JJ.Framework.Reflection.Exceptions;

namespace JJ.Business.Synthesizer.Calculation.Operators
{
    internal class Noise_OperatorCalculator : OperatorCalculatorBase
    {
        private readonly NoiseCalculator _noiseCalculator;

        /// <summary> Each operator should start at a different time offset in the pre-generated noise, to prevent artifacts. </summary>
        private readonly double _offset;

        public Noise_OperatorCalculator(NoiseCalculator noiseCalculator, double offset)
        {
            if (noiseCalculator == null) throw new NullException(() => noiseCalculator);

            _noiseCalculator = noiseCalculator;
            _offset = offset;
        }

        public override double Calculate(double time, int channelIndex)
        {
            double x = _noiseCalculator.GetValue(time + _offset);
            return x;
        }
    }
}
