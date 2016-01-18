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

        private readonly double[] _samples;
        private readonly double _samplingRate;
        private readonly double _minTime;

        /// <summary> 
        /// Value before might not be the same as the first sample: 
        /// if first node has value 1 but is an 'Off' node it will result in value 0 the whole period,
        /// but just before that, the value is 1.
        /// </summary>
        private readonly double _valueBefore;
        private readonly double _valueAfter;

        public OptimizedCurveCalculator(Curve curve)
        {
            IList<Node> sortedNodes = curve.Nodes.OrderBy(x => x.Time).ToArray();

            _valueBefore = sortedNodes.First().Value;
            _valueAfter = sortedNodes.Last().Value;
            _minTime = sortedNodes.First().Time;
            double maxTime = sortedNodes.Last().Time;
            double totalTime = maxTime - _minTime;
            double minNodeDuration = GetMinNodeLength(sortedNodes);

            // Try basing sample count on MINIMUM_SAMPLES_PER_NODE.
            double step = minNodeDuration / MINIMUM_SAMPLES_PER_NODE;
            int sampleCount = (int)(totalTime / step) + 1;

            // If that gets to high, base it on MAXIMUM_SAMPLE_COUNT
            if (sampleCount > MAXIMUM_SAMPLE_COUNT)
            {
                sampleCount = MAXIMUM_SAMPLE_COUNT;
                step = totalTime / (sampleCount - 1);
            }

            _samples = new double[sampleCount];
            _samplingRate = (sampleCount - 1) / totalTime;

            ICurveCalculator interpretedCurveCalculator = new InterpretedCurveCalculator(curve);

            double time = _minTime;

            for (int i = 0; i < sampleCount; i++)
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
            double t = (time - _minTime) * _samplingRate;

            int t0 = (int)t;
            int t1 = t0 + 1;

            // Return if sample not in range.
            if (t0 < 0) return _valueBefore;
            if (t1 > _samples.Length - 1) return _valueAfter;

            double x0 = _samples[t0];
            double x1 = _samples[t1];

            double x = x0 + (x1 - x0) * (t - t0);

            return x;
        }
    }
}