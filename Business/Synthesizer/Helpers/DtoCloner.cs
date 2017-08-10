using System;

using JJ.Business.Synthesizer.Dto;
using JJ.Framework.Exceptions;

namespace JJ.Business.Synthesizer.Helpers
{
    internal static class DtoCloner
    {
        public static void CloneProperties(IOperatorDto source, IOperatorDto dest)
        {
            if (source == null) throw new ArgumentNullException(nameof(source));
            if (dest == null) throw new ArgumentNullException(nameof(dest));

            dest.Inputs = source.Inputs;
            dest.OperatorID = source.OperatorID;
            dest.OperationIdentity = source.OperationIdentity;

            // Type checks are done here instead of overloads with different parameter types,
            // so what gets cloned is not dependent on the overload you just happened to call,
            // but is dependent on the specific concrete type you passed along.
            {
                if (source is IOperatorDto_WithDimension castedSource &&
                    dest is IOperatorDto_WithDimension castedDest)
                {
                    castedDest.CanonicalCustomDimensionName = castedSource.CanonicalCustomDimensionName;
                    castedDest.StandardDimensionEnum = castedSource.StandardDimensionEnum;
                    castedDest.DimensionStackLevel = castedSource.DimensionStackLevel;
                }
            }

            {
                if (source is OperatorDtoBase_WithCollectionRecalculation castedSource &&
                    dest is OperatorDtoBase_WithCollectionRecalculation castedDest)
                {
                    castedDest.CollectionRecalculationEnum = castedSource.CollectionRecalculationEnum;
                }
            }

            {
                if (source is Interpolate_OperatorDto castedSource &&
                    dest is Interpolate_OperatorDto castedDest)
                {
                    castedDest.ResampleInterpolationTypeEnum = castedSource.ResampleInterpolationTypeEnum;
                }
            }

            {
                if (source is Random_OperatorDto castedSource &&
                    dest is Random_OperatorDto castedDest)
                {
                    castedDest.ResampleInterpolationTypeEnum = castedSource.ResampleInterpolationTypeEnum;
                    castedDest.ArrayDto = castedSource.ArrayDto;
                }
            }

            {
                if (source is OperatorDtoBase_Filter_VarSound castedSource &&
                    dest is OperatorDtoBase_Filter_VarSound castedDest)
                {
                    castedDest.TargetSamplingRate = castedSource.TargetSamplingRate;
                    castedDest.NyquistFrequency = castedSource.NyquistFrequency;
                }
            }

            {
                if (source is Sample_OperatorDtoBase_WithSampleID castedSource &&
                    dest is Sample_OperatorDtoBase_WithSampleID castedDest)
                {
                    castedDest.SampleID = castedSource.SampleID;
                    castedDest.SampleChannelCount = castedSource.SampleChannelCount;
                    castedDest.InterpolationTypeEnum = castedSource.InterpolationTypeEnum;
                    castedDest.TargetChannelCount = castedSource.TargetChannelCount;
                    castedDest.ArrayDtos = castedSource.ArrayDtos;
                }
            }

            {
                if (source is Curve_OperatorDtoBase_WithoutMinX castedSource &&
                    dest is Curve_OperatorDtoBase_WithoutMinX castedDest)
                {
                    castedDest.CurveID = castedSource.CurveID;
                    castedDest.ArrayDto = castedSource.ArrayDto;
                }
            }

            {
                if (source is Curve_OperatorDtoBase_WithMinX castedSource &&
                    dest is Curve_OperatorDtoBase_WithMinX castedDest)
                {
                    castedDest.MinX = castedSource.MinX;
                }
            }

            {
                if (source is Cache_OperatorDtoBase_NotConstSignal castedSource &&
                    dest is Cache_OperatorDtoBase_NotConstSignal castedDest)
                {
                    castedDest.ChannelCount = castedSource.ChannelCount;
                    castedDest.InterpolationTypeEnum = castedSource.InterpolationTypeEnum;
                    castedDest.SpeakerSetupEnum = castedSource.SpeakerSetupEnum;
                    castedDest.ArrayDto = castedSource.ArrayDto;
                }
            }
        }
    }
}
