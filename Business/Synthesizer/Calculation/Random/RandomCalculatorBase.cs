using JJ.Framework.Mathematics;

namespace JJ.Business.Synthesizer.Calculation.Random
{
    /// <summary>
    /// Random values are not generated on the fly, but a set of values is cached,
    /// which is faster and also necessary for resampling, interpolation,
    /// going back in time and other processing to work.
    /// 
    /// Each instance shares the same pre-sampled noise data, but has a different offset into it,
    /// so that there is a different virtual random set for each instance of RandomCalculator.
    /// </summary>
    internal abstract class RandomCalculatorBase
    {
        private const int SAMPLE_COUNT = 100000;
        private const int OFFSET_COUNT = 100;
        private const int OFFSET_SNAP = SAMPLE_COUNT / OFFSET_COUNT;

        protected static readonly double[] _samples = CreateSamples();
        protected double _offset;

        private static double[] CreateSamples()
        {
            var samples = new double[SAMPLE_COUNT];
            for (int i = 0; i < SAMPLE_COUNT; i++)
            {
                double noiseSample = Randomizer.GetDouble();
                samples[i] = noiseSample;
            }
            return samples;
        }

        public void Reseed()
        {
            int offsetIndex = Randomizer.GetInt32(OFFSET_COUNT - 1);
            _offset = offsetIndex * OFFSET_SNAP;
        }
    }
}
