using System;
using JJ.Business.Synthesizer.Dto.Operators;

namespace JJ.Business.Synthesizer.Helpers
{
	internal static class DtoCloner
	{
		public static void CloneProperties(IOperatorDto source, IOperatorDto dest)
		{
			if (source == null) throw new ArgumentNullException(nameof(source));
			if (dest == null) throw new ArgumentNullException(nameof(dest));

			dest.Inputs = source.Inputs;
			dest.OperationIdentity = source.OperationIdentity;

			TryCloneDimension(source, dest);
			TryCloneCollectionRecalculation(source, dest);
			TryCloneRandom(source, dest);
			TryCloneFilterVarSound(source, dest);
			TryCloneSampleWithRate1(source, dest);
			TryCloneCurve(source, dest);
			TryCloneCache(source, dest);
			TryClonePositionReader(source, dest);
			TryCloneResampleInterpolation(source, dest);
		}

		// Type checks are done instead of overloads with different parameter types,
		// so what gets cloned is not dependent on the overload you just happened to call,
		// but is dependent on the specific concrete type you passed along.

		private static void TryCloneDimension(IOperatorDto source, IOperatorDto dest)
		{
			if (!(source is IOperatorDto_WithDimension castedSource) ||
			    !(dest is IOperatorDto_WithDimension castedDest))
			{
				return;
			}

			castedDest.CanonicalCustomDimensionName = castedSource.CanonicalCustomDimensionName;
			castedDest.StandardDimensionEnum = castedSource.StandardDimensionEnum;
		}

		private static void TryCloneCollectionRecalculation(IOperatorDto source, IOperatorDto dest)
		{
			if (!(source is OperatorDtoBase_WithCollectionRecalculation castedSource) ||
			    !(dest is OperatorDtoBase_WithCollectionRecalculation castedDest))
			{
				return;
			}

			castedDest.CollectionRecalculationEnum = castedSource.CollectionRecalculationEnum;
		}

		private static void TryCloneRandom(IOperatorDto source, IOperatorDto dest)
		{
			if (!(source is Random_OperatorDto castedSource) ||
			    !(dest is Random_OperatorDto castedDest))
			{
				return;
			}

			castedDest.ArrayDto = castedSource.ArrayDto;
			castedDest.Rate = castedSource.Rate;
		}

		private static void TryCloneFilterVarSound(IOperatorDto source, IOperatorDto dest)
		{
			if (!(source is OperatorDtoBase_Filter_VarSound castedSource) ||
			    !(dest is OperatorDtoBase_Filter_VarSound castedDest))
			{
				return;
			}

			castedDest.TargetSamplingRate = castedSource.TargetSamplingRate;
			castedDest.NyquistFrequency = castedSource.NyquistFrequency;
		}

		private static void TryCloneSampleWithRate1(IOperatorDto source, IOperatorDto dest)
		{
			if (!(source is SampleWithRate1_OperatorDto castedSource) ||
			    !(dest is SampleWithRate1_OperatorDto castedDest))
			{
				return;
			}

			castedDest.SampleID = castedSource.SampleID;
			castedDest.SampleChannelCount = castedSource.SampleChannelCount;
			castedDest.InterpolationTypeEnum = castedSource.InterpolationTypeEnum;
			castedDest.TargetChannelCount = castedSource.TargetChannelCount;
			castedDest.ArrayDtos = castedSource.ArrayDtos;
		}

		private static void TryCloneCurve(IOperatorDto source, IOperatorDto dest)
		{
			if (!(source is Curve_OperatorDto castedSource) || 
			    !(dest is Curve_OperatorDto castedDest))
			{
				return;
			}

			castedDest.CurveID = castedSource.CurveID;
			castedDest.ArrayDto = castedSource.ArrayDto;
			castedDest.MinX = castedSource.MinX;
		}

		private static void TryCloneCache(IOperatorDto source, IOperatorDto dest)
		{
			if (!(source is Cache_OperatorDto castedSource) ||
			    !(dest is Cache_OperatorDto castedDest))
			{
				return;
			}

			castedDest.ChannelCount = castedSource.ChannelCount;
			castedDest.InterpolationTypeEnum = castedSource.InterpolationTypeEnum;
			castedDest.SpeakerSetupEnum = castedSource.SpeakerSetupEnum;
			castedDest.ArrayDto = castedSource.ArrayDto;
		}

		private static void TryCloneResampleInterpolation(IOperatorDto source, IOperatorDto dest)
		{
			if (!(source is IOperatorDto_WithResampleInterpolation castedSource) ||
			    !(dest is IOperatorDto_WithResampleInterpolation castedDest))
			{
				return;
			}

			castedDest.ResampleInterpolationTypeEnum = castedSource.ResampleInterpolationTypeEnum;
		}

		private static void TryClonePositionReader(IOperatorDto source, IOperatorDto dest)
		{
			if (!(source is IOperatorDto_PositionReader castedSource) ||
			    !(dest is IOperatorDto_PositionReader castedDest))
			{
				return;
			}

			castedDest.Position = castedSource.Position;
		}
	}
}