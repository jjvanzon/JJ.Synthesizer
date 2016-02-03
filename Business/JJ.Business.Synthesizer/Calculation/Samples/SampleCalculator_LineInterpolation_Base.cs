using JJ.Data.Synthesizer;

namespace JJ.Business.Synthesizer.Calculation.Samples
{
    internal abstract class SampleCalculator_LineInterpolation_Base : SampleCalculatorBase
    {
        // TODO: extraSampleCount 1 should be enough, but it went wrong when I tried a stereo sample.

        public SampleCalculator_LineInterpolation_Base(Sample sample, byte[] bytes)
            : base(sample, bytes, extraSampleCount: 2)
        { }

        public override double CalculateValue(double time, int channelIndex)
        {
            // Return if sample not in range.
            // Execute it on the doubles, to prevent integer overflow.
            if (time < 0.0) return _valueBefore;
            if (time > _duration) return _valueAfter;

            double t = time * _rate;

            int t0 = (int)t;
            int t1 = t0 + 1; // See 'extraSampleCount' above.

            double x0 = _samples[channelIndex, t0];
            double x1 = _samples[channelIndex, t1];

            double x = x0 + (x1 - x0) * (t - t0);
            return x;
        }
    }
}