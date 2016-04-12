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

        public static CurveArrayInfo ConvertToCurveArrayInfo(Curve curve)
        {
            if (curve == null) throw new NullException(() => curve);

            IList<Node> sortedNodes = curve.Nodes.OrderBy(n => n.X).ToArray();
            
            double yBefore = sortedNodes.First().Y;
            double yAfter = sortedNodes.Last().Y;
            double minX = sortedNodes.First().X;
            double maxX = sortedNodes.Last().X;
            double totalXSpan = maxX - minX;
            double minNodeXSpan = GetMinNodeXSpan(sortedNodes);

            // Try basing sample count on MINIMUM_SAMPLES_PER_NODE.
            double step = minNodeXSpan / MINIMUM_SAMPLES_PER_NODE;
            int sampleCount = (int)(totalXSpan / step) + 1; // + 1 because 2 sample durations require 3 samples.

            // If that gets to high, base it on MAXIMUM_SAMPLE_COUNT
            if (sampleCount > MAXIMUM_SAMPLE_COUNT)
            {
                sampleCount = MAXIMUM_SAMPLE_COUNT;
                step = totalXSpan / (sampleCount - 1); // - 1 because 3 samples make 2 sample durations.
            }

            double samplingRate = (sampleCount - 1) / totalXSpan; // - 1 because 3 samples make 2 sample durations.

            // Calculate the array.
            double[] samples = new double[sampleCount];
            ICurveCalculator interpretedCurveCalculator = new InterpretedCurveCalculator(curve);
            double x = minX;
            for (int i = 0; i < sampleCount; i++)
            {
                double sample = interpretedCurveCalculator.CalculateY(x);
                samples[i] = sample;
                x += step;
            }

            return new CurveArrayInfo
            {
                Array = samples,
                Rate = samplingRate,
                MinX = minX,
                YBefore = yBefore,
                YAfter = yAfter
            };
        }

        private static double GetMinNodeXSpan(IList<Node> sortedNodes)
        {
            double minNodeLength = CalculationHelper.VERY_HIGH_VALUE;

            for (int i = 0; i < sortedNodes.Count - 1; i++)
            {
                Node nodeA = sortedNodes[i];
                Node nodeB = sortedNodes[i + 1];

                double nodeXSpan = nodeB.X - nodeA.X;

                if (minNodeLength > nodeXSpan)
                {
                    minNodeLength = nodeXSpan;
                }
            }

            return minNodeLength;
        }
    }
}
