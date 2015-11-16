using JJ.Data.Synthesizer;

namespace JJ.Business.Synthesizer.Calculation.Samples
{
    internal abstract class BlockInterpolation_SampleCalculatorBase : SampleCalculatorBase
    {
        public BlockInterpolation_SampleCalculatorBase(Sample sample, byte[] bytes)
            : base(sample, bytes)
        { }

        public override double CalculateValue(double time, int channelIndex)
        {
            double t = time * _rate;
            int t0 = (int)t;

            // Return if sample not in range.
            if (t0 < 0) return 0;
            if (t0 > _samples.Length - 1) return 0;

            double value = _samples[channelIndex, t0];
            return value;
        }
    }
}