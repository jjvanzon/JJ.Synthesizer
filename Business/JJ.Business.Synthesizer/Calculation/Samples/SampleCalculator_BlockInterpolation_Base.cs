using JJ.Data.Synthesizer;

namespace JJ.Business.Synthesizer.Calculation.Samples
{
    internal abstract class SampleCalculator_BlockInterpolation_Base : SampleCalculatorBase
    {
        public SampleCalculator_BlockInterpolation_Base(Sample sample, byte[] bytes)
            : base(sample, bytes, extraSampleCount: 0)
        { }

        public override double CalculateValue(double time, int channelIndex)
        {
            // Return if sample not in range.
            // Execute it on the doubles, to prevent integer overflow.
            if (time < 0.0) return _valueBefore;
            if (time > _duration) return _valueAfter;

            double t = time * _rate;
            int t0 = (int)t;

            double value = _samples[channelIndex, t0];
            return value;
        }
    }
}