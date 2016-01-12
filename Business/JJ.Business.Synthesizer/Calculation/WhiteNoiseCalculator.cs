using JJ.Framework.Mathematics;
using JJ.Framework.Reflection.Exceptions;

namespace JJ.Business.Synthesizer.Calculation
{
    /// <summary>
    /// White noise is generated not on the fly, but by a cached 10 seconds of noise,
    /// which is faster and also necessary to be able to filter out frequencies using resampling and interpolation.
    /// 10 seconds should be enough to not notice a repeating pattern.
    /// </summary>
    internal class WhiteNoiseCalculator
    {
        /// <summary>
        /// The amount of pre-calculated noise seconds should be large enough not to hear artifacts.
        /// </summary>
        private const int PRE_CALCULATED_SECONDS = 10;
        /// <summary> 
        /// Prevent artifacts by making sure the random offsets are not too close together,
        /// But also should not too far apart, or the chance that we get the same offset becomes bigger.
        /// </summary>
        private const double OFFSET_SNAP_IN_NUMBER_OF_SECONDS = 0.1;
        private const int OFFSET_SNAP_COUNT = (int)(PRE_CALCULATED_SECONDS / OFFSET_SNAP_IN_NUMBER_OF_SECONDS);

        private readonly int _samplingRate;
        private readonly double[] _samples;
        private readonly int _sampleCountMinus1;

        /// <summary>
        /// White noise is generated not on the fly, but by a cached 10 seconds of noise,
        /// which is faster and also necessary to be able to filter out frequencies using resampling and interpolation.
        /// 10 seconds should be enough to not notice a repeating pattern.
        /// </summary>
        public WhiteNoiseCalculator(int samplingRate)
        {
            if (samplingRate <= 0) throw new LessThanOrEqualException(() => samplingRate, 0);
            _samplingRate = samplingRate;

            int sampleCount = _samplingRate * PRE_CALCULATED_SECONDS;

            _samples = new double[sampleCount];
            for (int i = 0; i < sampleCount; i++)
            {
                double noiseSample = Randomizer.GetDouble() * 2.0 - 1.0;
                _samples[i] = noiseSample;
            }

            _sampleCountMinus1 = sampleCount - 1;
        }

        /// <summary> 
        /// Each operator should start at a different time offset in the pre-generated noise, to prevent artifacts.
        /// </summary>
        public double GetRandomOffset()
        {
            int offsetSnapPosition = Randomizer.GetInt32(OFFSET_SNAP_COUNT - 1);
            double offset = offsetSnapPosition * OFFSET_SNAP_IN_NUMBER_OF_SECONDS;
            return offset;
        }

        /// <summary>
        /// For each white noise operator it is advised to add an offset to the time.
        /// You can get a random offset using the GetRandomOffset() method.
        /// </summary>
        public double GetValue(double time)
        {
            // Block interpolation should be enough, 
            // because in practice the time speed should so 
            // that each sample is a random number.

            double t = time * _samplingRate;

            t = t % _sampleCountMinus1;

            int t0 = (int)t;

            double x = _samples[t0];
            return x;
        }
    }
}
