using System;
using System.Collections.Generic;
using System.Linq;
using JJ.Framework.Mathematics;

namespace JJ.Business.Synthesizer.Calculation.Random
{
    /// <summary>
    /// Random values are not generated on the fly, but a set of values is cached,
    /// which is faster and also necessary for resampling, interpolation and 
    /// going back in time.
    /// </summary>
    internal abstract class RandomCalculatorBase
    {
        private const int SAMPLE_COUNT = 100000;
        protected const int SAMPLE_COUNT_DOUBLE = SAMPLE_COUNT;
        private const int OFFSET_COUNT = 100;
        private const int OFFSET_SNAP = SAMPLE_COUNT / OFFSET_COUNT;

        protected static readonly double[] _samples = CreateSamples();

        /// <summary>
        /// Random values are not generated on the fly, but a set of values is cached,
        /// which is faster and also necessary for resampling, interpolation and 
        /// going back in time to work.
        /// </summary>
        private static double[] CreateSamples()
        {
            double[] samples = new double[SAMPLE_COUNT];
            for (int i = 0; i < SAMPLE_COUNT; i++)
            {
                double noiseSample = Randomizer.GetDouble();
                samples[i] = noiseSample;
            }
            return samples;
        }

        /// <summary> 
        /// Each operator should start at a different offset in the pre-generated samples,
        /// so almost every operator uses a different range of values.
        /// </summary>
        public static int GetRandomOffset()
        {
            int offsetIndex = Randomizer.GetInt32(OFFSET_COUNT - 1);
            int offset = offsetIndex * OFFSET_SNAP;
            return offset;
        }

        /// <summary>
        /// For each Random operator it is advised to add an offset to the time.
        /// You can get a random offset using the GetRandomOffset() method.
        /// </summary>
        public abstract double GetValue(double time);
    }
}
