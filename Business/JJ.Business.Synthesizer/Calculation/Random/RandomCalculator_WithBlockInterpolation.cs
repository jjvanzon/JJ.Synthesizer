using JJ.Framework.Mathematics;
using JJ.Framework.Reflection.Exceptions;

namespace JJ.Business.Synthesizer.Calculation
{
    /// <summary>
    /// Random values are not generated on the fly, but a set of values is cached,
    /// which is faster and also necessary for resampling, interpolation and 
    /// going back in time.
    /// </summary>
    internal class RandomCalculator_WithBlockInterpolation
    {
        private const int SAMPLE_COUNT = 100000;
        private const int OFFSET_COUNT = 100;
        private const int OFFSET_SNAP = SAMPLE_COUNT / OFFSET_COUNT;
        private const int SAMPLE_COUNT_MINUS_ONE = SAMPLE_COUNT - 1;

        private readonly double[] _samples;

        /// <summary>
        /// Random values are not generated on the fly, but a set of values is cached,
        /// which is faster and also necessary for resampling, interpolation and 
        /// going back in time to work.
        /// </summary>
        public RandomCalculator_WithBlockInterpolation()
        {
            _samples = new double[SAMPLE_COUNT];
            for (int i = 0; i < SAMPLE_COUNT; i++)
            {
                double noiseSample = Randomizer.GetDouble();
                _samples[i] = noiseSample;
            }
        }

        /// <summary> 
        /// Each operator should start at a different offset in the pre-generated samples,
        /// so almost every operator uses a different range of values.
        /// </summary>
        public int GetRandomOffset()
        {
            int offsetIndex = Randomizer.GetInt32(OFFSET_COUNT - 1);
            int offset = offsetIndex * OFFSET_SNAP;
            return offset;
        }

        /// <summary>
        /// For each Random operator it is advised to add an offset to the time.
        /// You can get a random offset using the GetRandomOffset() method.
        /// </summary>
        public double GetValue(double time)
        {
            double t = time % SAMPLE_COUNT_MINUS_ONE;

            int t0 = (int)t;

            double x = _samples[t0];
            return x;
        }
    }
}
