//using System;
//using System.Collections.Generic;
//using System.Linq;
//using JJ.Business.Synthesizer.Calculation.Arrays;
//using JJ.Business.Synthesizer.Enums;
//using JJ.Data.Synthesizer;

//namespace JJ.Business.Synthesizer.Calculation.Curves
//{
//    /// <summary>
//    /// To optimize the calculation, it converts a curve to equally spaced samples
//    /// and linearly interpolates the samples.
//    /// </summary>
//    internal class CurveCalculator : ICurveCalculator
//    {
//        private const double MINIMUM_SAMPLES_PER_NODE = 100.0;
//        private const int MAXIMUM_SAMPLE_COUNT = 10000;

//        private ArrayCalculatorBase _arrayCalculator;

//        public CurveCalculator(Curve curve)
//        {
//            IList<Node> sortedNodes = curve.Nodes.OrderBy(x => x.Time).ToArray();

//            double valueBefore = sortedNodes.First().Value;
//            double valueAfter = sortedNodes.Last().Value;
//            double minTime = sortedNodes.First().Time;
//            double maxTime = sortedNodes.Last().Time;
//            double totalDuration = maxTime - minTime;
//            double minNodeDuration = GetMinNodeLength(sortedNodes);

//            // Try basing sample count on MINIMUM_SAMPLES_PER_NODE.
//            double step = minNodeDuration / MINIMUM_SAMPLES_PER_NODE;
//            int sampleCount = (int)(totalDuration / step) + 1; // + 1 because 2 sample durations require 3 samples.

//            // If that gets to high, base it on MAXIMUM_SAMPLE_COUNT
//            if (sampleCount > MAXIMUM_SAMPLE_COUNT)
//            {
//                sampleCount = MAXIMUM_SAMPLE_COUNT;
//                step = totalDuration / (sampleCount - 1); // - 1 because 3 samples make 2 sample durations.
//            }

//            double samplingRate = (sampleCount - 1) / totalDuration; // - 1 because 3 samples make 2 sample durations.

//            // Calculate the array.
//            double[] samples = new double[sampleCount];
//            ICurveCalculator interpretedCurveCalculator = new InterpretedCurveCalculator(curve);
//            double time = minTime;
//            for (int i = 0; i < sampleCount; i++)
//            {
//                double sample = interpretedCurveCalculator.CalculateValue(time);
//                samples[i] = sample;
//                time += step;
//            }

//            _arrayCalculator = ArrayCalculatorFactory.CreateArrayCalculator(
//                samples, 
//                samplingRate, 
//                minTime,
//                // Value before might not be the same as the first sample: 
//                // if first node has value 1 but is an 'Off' node it will result in value 0 the whole period,
//                // but just before that, the value is 1.
//                valueBefore, 
//                valueAfter,
//                // TODO: Vary interpolation from the outside.
//                ResampleInterpolationTypeEnum.LineRememberT0);
//        }

//        private static double GetMinNodeLength(IList<Node> sortedNodes)
//        {
//            double minNodeLength = CalculationHelper.VERY_HIGH_VALUE;

//            for (int i = 0; i < sortedNodes.Count - 1; i++)
//            {
//                Node nodeA = sortedNodes[i];
//                Node nodeB = sortedNodes[i + 1];

//                double nodeLength = nodeB.Time - nodeA.Time;

//                if (minNodeLength > nodeLength)
//                {
//                    minNodeLength = nodeLength;
//                }
//            }

//            return minNodeLength;
//        }

//        public double CalculateValue(double time)
//        {
//            double value = _arrayCalculator.CalculateValue(time);
//            return value;
//        }
//    }
//}