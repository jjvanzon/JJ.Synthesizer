using System;
using System.Runtime.CompilerServices;
using JJ.Business.Synthesizer.Calculation.Arrays;
using JJ.Business.Synthesizer.Configuration;
using JJ.Business.Synthesizer.Helpers;
using JJ.Framework.Common;
using JJ.Framework.Mathematics;
using JJ.Framework.Reflection.Exceptions;

namespace JJ.Business.Synthesizer.Calculation
{
    /// <summary>
    /// White noise is generated not on the fly, but by a cached 10 seconds of noise,
    /// which is faster and also necessary to be able to filter out frequencies using resampling and interpolation.
    /// 10 seconds should be enough to not notice a repeating pattern.
    /// </summary>
    internal static class NoiseCalculator
    {
        /// <summary>
        /// Prevent artifacts by making sure the random offsets are not too close together,
        /// But also should not too far apart, or the chance that we get the same offset becomes bigger.
        /// </summary>
        private const double OFFSET_SNAP_IN_SECONDS = 0.1;

        private static int _offsetSnapCount = GetOffsetSnapCount();

        /// <summary>
        /// Block interpolation should be enough,
        /// because in practice the time speed should so
        /// that each sample is a random number.
        /// </summary>
        private static ArrayCalculator_RotatePosition_Block _arrayCalculator = CreateArrayCalculator();

        /// <summary>
        /// White noise is generated not on the fly, but by a cached 10 seconds of noise,
        /// which is faster and also necessary to be able to filter out frequencies using resampling and interpolation.
        /// 10 seconds should be enough to not notice a repeating pattern.
        /// </summary>
        private static ArrayCalculator_RotatePosition_Block CreateArrayCalculator()
        {
            int samplingRate = GetSamplingRate();
            double cachedSeconds = GetCachedSeconds();

            double sampleCountDouble = samplingRate * cachedSeconds;

            if (!ConversionHelper.CanCastToNonNegativeInt32(sampleCountDouble))
            {
                throw new Exception(String.Format("sampleCount '{0}' cannot be cast to non-negative Int32.", sampleCountDouble));
            }

            int sampleCount = (int)sampleCountDouble;

            double[] samples = new double[sampleCount];
            for (int i = 0; i < sampleCount; i++)
            {
                double noiseSample = Randomizer.GetDouble() * 2.0 - 1.0;
                samples[i] = noiseSample;
            }

            var arrayCalculator = new ArrayCalculator_RotatePosition_Block(samples, samplingRate);

            return arrayCalculator;
        }

        /// <summary>
        /// Each operator should start at a different time offset in the pre-generated noise, to prevent artifacts.
        /// </summary>
        public static double GetRandomOffset()
        {
            int offsetSnapPosition = Randomizer.GetInt32(_offsetSnapCount - 1);
            double offset = offsetSnapPosition * OFFSET_SNAP_IN_SECONDS;
            return offset;
        }

        /// <summary>
        /// For each white noise operator it is advised to add an offset to the time.
        /// You can get a random offset using the GetRandomOffset() method.
        /// </summary>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static double GetValue(double time)
        {
            return _arrayCalculator.CalculateValue(time);
        }

        // Helpers

        private static int GetSamplingRate()
        {
            var config = ConfigurationHelper.GetSection<ConfigurationSection>();
            if (config.CachedNoiseSamplingRate <= 0) throw new LessThanOrEqualException(() => config.CachedNoiseSamplingRate, 0);
            return config.CachedNoiseSamplingRate;
        }

        private static double GetCachedSeconds()
        {
            var config = ConfigurationHelper.GetSection<ConfigurationSection>();
            if (config.CachedNoiseSeconds <= 0) throw new LessThanOrEqualException(() => config.CachedNoiseSeconds, 0);
            return config.CachedNoiseSeconds;
        }

        private static int GetOffsetSnapCount()
        {
            double cachedSeconds = GetCachedSeconds();

            return (int)(cachedSeconds / OFFSET_SNAP_IN_SECONDS);
        }
    }
}
