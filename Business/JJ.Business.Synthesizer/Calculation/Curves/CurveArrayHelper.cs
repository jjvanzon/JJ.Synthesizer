using System;
using System.Collections.Generic;
using System.Linq;
using JJ.Data.Synthesizer;
using JJ.Framework.Reflection.Exceptions;

namespace JJ.Business.Synthesizer.Calculation.Curves
{
    internal static class CurveArrayHelper
    {
        private const double MINIMUM_SAMPLES_PER_NODE = 100.0;
        private const int MAXIMUM_SAMPLE_COUNT = 10000;

        public static CurveArrayInfo GetArrayInfo(Curve curve)
        {
            if (curve == null) throw new NullException(() => curve);

            IList<Node> sortedNodes = curve.Nodes.OrderBy(x => x.Time).ToArray();

            double valueBefore = sortedNodes.First().Value;
            double valueAfter = sortedNodes.Last().Value;
            double minTime = sortedNodes.First().Time;
            double maxTime = sortedNodes.Last().Time;
            double totalDuration = maxTime - minTime;
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

            double samplingRate = (sampleCount - 1) / totalDuration; // - 1 because 3 samples make 2 sample durations.

            // Calculate the array.
            double[] samples = new double[sampleCount];
            ICurveCalculator interpretedCurveCalculator = new InterpretedCurveCalculator(curve);
            double time = minTime;
            for (int i = 0; i < sampleCount; i++)
            {
                double sample = interpretedCurveCalculator.CalculateValue(time);
                samples[i] = sample;
                time += step;
            }

            return new CurveArrayInfo
            {
                Array = samples,
                Rate = samplingRate,
                MinTime = minTime,
                ValueBefore = valueBefore,
                ValueAfter = valueAfter
            };
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
    }
}
