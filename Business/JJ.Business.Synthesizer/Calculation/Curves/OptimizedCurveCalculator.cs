using System;
using System.Collections.Generic;
using System.Linq;
using JJ.Data.Synthesizer;

namespace JJ.Business.Synthesizer.Calculation.Curves
{
    /// <summary>
    /// To optimize the calculation, it converts a curve to equally spaced samples
    /// and linearly interpolates the samples.
    /// </summary>
    internal class OptimizedCurveCalculator : ICurveCalculator
    {
        private const double MINIMUM_SAMPLES_PER_NODE = 100.0;
        private const int MAXIMUM_SAMPLE_COUNT = 10000;

        /// <summary> 
        /// Value before might not be the same as the first sample: 
        /// if first node has value 1 but is an 'Off' node it will result in value 0 the whole period,
        /// but just before that, the value is 1.
        /// </summary>
        private readonly double _valueBefore;
        private readonly double _valueAfter;
        private readonly double _samplingRate;
        private readonly double _minTime;
        private readonly double _maxTime;

        /// <summary>
        /// NOTE: Length does not have to correspond to _duration.
        /// The array is one sample longer,
        /// for interpolation, for not worrying about floating point imprecision,
        /// and to prevent checks in real-time.
        /// Beware of using _samples.Length.
        /// Use the other fields instead.
        /// </summary>
        private readonly double[] _samples;

        public OptimizedCurveCalculator(Curve curve)
        {
            IList<Node> sortedNodes = curve.Nodes.OrderBy(x => x.Time).ToArray();

            _valueBefore = sortedNodes.First().Value;
            _valueAfter = sortedNodes.Last().Value;
            _minTime = sortedNodes.First().Time;
            _maxTime = sortedNodes.Last().Time;
            double totalDuration = _maxTime - _minTime;
            double minNodeDuration = GetMinNodeLength(sortedNodes);

            // Try basing sample count on MINIMUM_SAMPLES_PER_NODE.
            double step = minNodeDuration / MINIMUM_SAMPLES_PER_NODE;
            int sampleCount = (int)(totalDuration / step) + 1; // + 1 because 2 sample durations require 3 samples.

            // If that gets to high, base it on MAXIMUM_SAMPLE_COUNT
            if (sampleCount > MAXIMUM_SAMPLE_COUNT)
            {
                sampleCount = MAXIMUM_SAMPLE_COUNT;
                step = totalDuration / (sampleCount - 1); // - 1 because 3 samples make 2 sample durations.
            }

            // Put extra sample in the array,
            // for interpolation, not worrying about imprecision,
            // and to prevent checks in real-time.
            int sampleCountPlus1 = sampleCount + 1; 

            _samples = new double[sampleCountPlus1];
            _samplingRate = (sampleCount - 1) / totalDuration; // - 1 because 3 samples make 2 sample durations.

            ICurveCalculator interpretedCurveCalculator = new InterpretedCurveCalculator(curve);

            double time = _minTime;

            for (int i = 0; i < sampleCountPlus1; i++)
            {
                double sample = interpretedCurveCalculator.CalculateValue(time);
                _samples[i] = sample;

                time += step;
            }
        }

        private static double GetMinNodeLength(IList<Node> sortedNodes)
        {
            double minNodeLength = CalculationHelper.VERY_HIGH_VALUE;

            for (int i = 0; i < sortedNodes.Count - 1; i++)
            {
                Node nodeA = sortedNodes[i];
                Node nodeB = sortedNodes[i + 1];

                double nodeLength = nodeB.Time - nodeA.Time;

                if (minNodeLength > nodeLength)
                {
                    minNodeLength = nodeLength;
                }
            }

            return minNodeLength;
        }

        public double CalculateValue(double time)
        {
            checked
            {
                // Return if sample not in range.
                // Execute it on the doubles, to prevent integer overflow.
                if (time < _minTime) return _valueBefore;
                if (time > _maxTime) return _valueAfter;

                double t = (time - _minTime) * _samplingRate;

                int t0 = (int)t;
                int t1 = t0 + 1; // Array is guaranteed to have extra sample extra.

                double x0 = _samples[t0];
                double x1 = _samples[t1];

                double x = x0 + (x1 - x0) * (t - t0);

                return x;
            }
        }
    }
}