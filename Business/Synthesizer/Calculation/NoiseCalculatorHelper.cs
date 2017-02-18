using System;
using JJ.Business.Synthesizer.Configuration;
using JJ.Business.Synthesizer.Helpers;
using JJ.Framework.Common;
using JJ.Framework.Exceptions;
using JJ.Framework.Mathematics;

namespace JJ.Business.Synthesizer.Calculation
{
    public static class NoiseCalculatorHelper
    {
        /// <summary>
        /// Prevent artifacts by making sure the random offsets are not too close together,
        /// But also should not too far apart, or the chance that we get the same offset becomes bigger.
        /// </summary>
        private const double OFFSET_SNAP_IN_SECONDS = 0.1;
        private static readonly int _offsetSnapCount = GetOffsetSnapCount();

        public static double[] Samples { get; } = CreateSamples();
        public static int SamplingRate { get; } = GetSamplingRate();

        public static double GenerateOffset()
        {
            int offsetSnapPosition = Randomizer.GetInt32(_offsetSnapCount - 1);
            double offset = offsetSnapPosition * OFFSET_SNAP_IN_SECONDS;
            return offset;
        }

        private static double[] CreateSamples()
        {
            int samplingRate = GetSamplingRate();
            double cachedSeconds = GetCachedSeconds();

            double sampleCountDouble = samplingRate * cachedSeconds;

            if (!ConversionHelper.CanCastToNonNegativeInt32(sampleCountDouble))
            {
                throw new Exception($"sampleCount '{sampleCountDouble}' cannot be cast to non-negative Int32.");
            }

            int sampleCount = (int)sampleCountDouble;

            var samples = new double[sampleCount];
            for (int i = 0; i < sampleCount; i++)
            {
                double noiseSample = Randomizer.GetDouble() * 2.0 - 1.0;
                samples[i] = noiseSample;
            }

            return samples;
        }

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
