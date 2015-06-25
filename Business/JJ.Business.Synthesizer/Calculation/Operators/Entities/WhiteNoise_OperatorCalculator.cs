using JJ.Framework.Mathematics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JJ.Business.Synthesizer.Calculation.Operators.Entities
{
    internal class WhiteNoise_OperatorCalculator : OperatorCalculatorBase
    {
        /// <summary> It is not great that I assume a sampling rate here. </summary>
        private const int ASSUMED_SAMPLING_RATE = 44100;
        /// <summary> The amount of pre-calculated noise seconds should be large enough not to hear artifacts. </summary>
        private const int PRE_CALCULATED_NOISE_SECONDS = 10;
        private const int PRE_CALCULATED_NOISE_SAMPLE_COUNT = ASSUMED_SAMPLING_RATE * 10;
        /// <summary> Prevents random offsets to be too close together, to prevent artifacts. </summary>
        private const double OFFSET_SNAP_IN_SECONDS = 0.1;
        private const int OFFSET_SNAP_IN_SAMPLES = (int)(ASSUMED_SAMPLING_RATE * OFFSET_SNAP_IN_SECONDS);
        private const int SNAP_POSITION_COUNT = PRE_CALCULATED_NOISE_SAMPLE_COUNT / OFFSET_SNAP_IN_SAMPLES;
        private const int PRE_CALCULATED_NOISE_SAMPLE_COUNT_MINUS_1 = PRE_CALCULATED_NOISE_SAMPLE_COUNT - 1;

        private static double[] _noiseSamples = new double[PRE_CALCULATED_NOISE_SAMPLE_COUNT];

        /// <summary> Each operator should start at a different time offset in the pre-generated noise, to prevent artifacts. </summary>
        private double _offset;

        static WhiteNoise_OperatorCalculator()
        {
            int randomSeed = Guid.NewGuid().GetHashCode();
            var random = new Random(randomSeed);

            for (int i = 0; i < PRE_CALCULATED_NOISE_SAMPLE_COUNT; i++)
            {
                double noiseSample = Randomizer.GetDouble() * 2.0 - 1.0;
                _noiseSamples[i] = noiseSample;
            }
        }

        public WhiteNoise_OperatorCalculator()
        {
            int offsetSnapPosition = Randomizer.GetInt32(SNAP_POSITION_COUNT - 1);
            int offset = offsetSnapPosition * OFFSET_SNAP_IN_SAMPLES;
        }

        public override double Calculate(double time, int channelIndex)
        {
            double t = (time + _offset) * ASSUMED_SAMPLING_RATE;

            t = t % PRE_CALCULATED_NOISE_SAMPLE_COUNT_MINUS_1;

            int t0 = (int)t;

            double x0 = _noiseSamples[t0];
            double x1 = _noiseSamples[t0 + 1];

            double x = x0 + (x1 - x0) * (t - t0);
            return x;
        }
    }
}
