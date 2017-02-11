using System;
using System.Runtime.CompilerServices;
using JJ.Business.Synthesizer.Calculation.Arrays;
using JJ.Business.Synthesizer.Configuration;
using JJ.Business.Synthesizer.Helpers;
using JJ.Framework.Common;
using JJ.Framework.Mathematics;
using JJ.Framework.Exceptions;

namespace JJ.Business.Synthesizer.Calculation
{
    /// <summary>
    /// White noise is not generated on the fly, but by a cached 10 seconds of noise,
    /// which is faster and also necessary for resampling, interpolation, going back in time and other processing.
    /// 10 seconds should be enough to not notice a repeating pattern.
    /// 
    /// Each instance shares the same pre-sampled noise data, but has a different offset into it,
    /// so that there is a different virtual random set for each instance of NoiseCalculator.
    /// </summary>
    internal class NoiseCalculator : ICalculatorWithPosition
    {
        public NoiseCalculator()
        {
            Reseed();
        }

        /// <summary>
        /// Prevent artifacts by making sure the random offsets are not too close together,
        /// But also should not too far apart, or the chance that we get the same offset becomes bigger.
        /// </summary>
        private const double OFFSET_SNAP_IN_SECONDS = 0.1;
        private static readonly int _offsetSnapCount = GetOffsetSnapCount();
        private double _offset;

        /// <summary>
        /// Block interpolation should be enough,
        /// because in practice the time speed should so that each sample is a random number.
        /// </summary>
        private static readonly ArrayCalculator_RotatePosition_Block _arrayCalculator = CreateArrayCalculator();

        private static ArrayCalculator_RotatePosition_Block CreateArrayCalculator()
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

            var arrayCalculator = new ArrayCalculator_RotatePosition_Block(samples, samplingRate);

            return arrayCalculator;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public double Calculate(double time)
        {
            double transformedTime = time + _offset;

            return _arrayCalculator.Calculate(transformedTime);
        }

        public void Reseed()
        {
            int offsetSnapPosition = Randomizer.GetInt32(_offsetSnapCount - 1);
            _offset = offsetSnapPosition * OFFSET_SNAP_IN_SECONDS;
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
