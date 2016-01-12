using JJ.Data.Synthesizer;

namespace JJ.Business.Synthesizer.Calculation.Samples
{
    internal abstract class LineInterpolation_SampleCalculatorBase : SampleCalculatorBase
    {
        public LineInterpolation_SampleCalculatorBase(Sample sample, byte[] bytes)
            : base(sample, bytes)
        { }

        public override double CalculateValue(double time, int channelIndex)
        {
            double t = time * _rate;

            // Return if sample not in range.
            if (t < 0) return 0;

            int t0 = (int)t;
            int t1 = t0 + 1;

            // Return if sample not in range.
            if (t1 >= _samples.Length) return 0;

            double x0 = _samples[channelIndex, t0];
            double x1 = _samples[channelIndex, t1];

            double x = x0 + (x1 - x0) * (t - t0);
            return x;
        }
    }
}