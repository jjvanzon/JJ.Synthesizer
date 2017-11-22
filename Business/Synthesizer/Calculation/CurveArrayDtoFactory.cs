using System.Collections.Generic;
using System.Linq;
using JJ.Business.Synthesizer.Dto;
using JJ.Business.Synthesizer.Enums;
using JJ.Business.Synthesizer.Helpers;
using JJ.Data.Synthesizer.Entities;
using JJ.Framework.Exceptions;

namespace JJ.Business.Synthesizer.Calculation
{
	internal static class CurveArrayDtoFactory
	{
		private const double MINIMUM_SAMPLES_PER_NODE = 100.0;
		private const int MAXIMUM_SAMPLE_COUNT = 10000;
		// These defaults may become variables in the future.
		private const InterpolationTypeEnum DEFAULT_INTERPOLATION_TYPE_ENUM = InterpolationTypeEnum.Line;
		private const bool DEFAULT_IS_ROTATING = false;
		
		public static ArrayDto ConvertToArrayDto(Curve curve)
		{
			if (curve == null) throw new NullException(() => curve);

			IList<Node> sortedNodes = curve.Nodes.OrderBy(n => n.X).ToArray();
			
			double yBefore = sortedNodes.First().Y;
			double yAfter = sortedNodes.Last().Y;
			double minX = sortedNodes.First().X;
			double maxX = sortedNodes.Last().X;
			double totalLengthX = maxX - minX;
			double smallestNodeLengthX = GetSmallestNodeLengthX(sortedNodes);

			// Try basing sample count on MINIMUM_SAMPLES_PER_NODE.
			double step = smallestNodeLengthX / MINIMUM_SAMPLES_PER_NODE;
			int sampleCount = (int)(totalLengthX / step) + 1; // + 1 because 2 sample durations require 3 samples.

			// If that gets to high, base it on MAXIMUM_SAMPLE_COUNT
			if (sampleCount > MAXIMUM_SAMPLE_COUNT)
			{
				sampleCount = MAXIMUM_SAMPLE_COUNT;
				step = totalLengthX / (sampleCount - 1); // - 1 because 3 samples make 2 sample durations.
			}

			double samplingRate = (sampleCount - 1) / totalLengthX; // - 1 because 3 samples make 2 sample durations.

			// Calculate the array.
			var samples = new double[sampleCount];
			ICalculatorWithPosition interpretedCurveCalculator = new InterpretedCurveCalculator(curve);
			double x = minX;
			for (int i = 0; i < sampleCount; i++)
			{
				double sample = interpretedCurveCalculator.Calculate(x);
				samples[i] = sample;
				x += step;
			}

			return new ArrayDto
			{
				Array = samples,
				Rate = samplingRate,
				MinPosition = minX,
				ValueBefore = yBefore,
				ValueAfter = yAfter,
				InterpolationTypeEnum = DEFAULT_INTERPOLATION_TYPE_ENUM,
				IsRotating = DEFAULT_IS_ROTATING
			};
		}
		
		private static double GetSmallestNodeLengthX(IList<Node> sortedNodes)
		{
			double smallestNodeLengthX = CalculationHelper.VERY_HIGH_VALUE;

			for (int i = 0; i < sortedNodes.Count - 1; i++)
			{
				Node nodeA = sortedNodes[i];
				Node nodeB = sortedNodes[i + 1];

				double nodeLengthX = nodeB.X - nodeA.X;

				if (smallestNodeLengthX > nodeLengthX)
				{
					smallestNodeLengthX = nodeLengthX;
				}
			}

			return smallestNodeLengthX;
		}
	}
}
