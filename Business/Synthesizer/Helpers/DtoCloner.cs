using System;
using JetBrains.Annotations;
using JJ.Business.Synthesizer.Dto;
using JJ.Framework.Exceptions;

namespace JJ.Business.Synthesizer.Helpers
{
    internal static class DtoCloner
    {
        public static void Clone_AggregateOverDimensionProperties(OperatorDtoBase_AggregateOverDimension_AllVars source, OperatorDtoBase_AggregateOverDimension_AllVars dest)
        {
            dest.SignalOperatorDto = source.SignalOperatorDto;
            dest.FromOperatorDto = source.FromOperatorDto;
            dest.TillOperatorDto = source.TillOperatorDto;
            dest.StepOperatorDto = source.StepOperatorDto;
            dest.CollectionRecalculationEnum = source.CollectionRecalculationEnum;

            Clone_WithDimensionProperties(source, dest);
        }

        public static void TryClone_AggregateOverDimensionProperties(OperatorDtoBase_AggregateOverDimension_AllVars source, IOperatorDto dest)
        {
            var castedDest = dest as OperatorDtoBase_AggregateOverDimension_AllVars;
            if (castedDest != null)
            {
                Clone_AggregateOverDimensionProperties(source, castedDest);
            }
        }

        public static void Clone_AggregateFollowerProperties(OperatorDtoBase_AggregateFollower_AllVars source, OperatorDtoBase_AggregateFollower_AllVars dest)
        {
            dest.SignalOperatorDto = source.SignalOperatorDto;
            dest.SliceLengthOperatorDto = source.SliceLengthOperatorDto;
            dest.SampleCountOperatorDto = source.SampleCountOperatorDto;

            Clone_WithDimensionProperties(source, dest);
        }

        public static void TryClone_AggregateFollowerProperties(OperatorDtoBase_AggregateFollower_AllVars source, IOperatorDto dest)
        {
            var castedDest = dest as OperatorDtoBase_AggregateFollower_AllVars;
            if (castedDest != null)
            {
                Clone_AggregateFollowerProperties(source, castedDest);
            }
        }

        public static void Clone_CacheOperatorProperties(Cache_OperatorDtoBase_NotConstSignal source, Cache_OperatorDtoBase_NotConstSignal dest)
        {
            dest.OperatorID = source.OperatorID;
            dest.SignalOperatorDto = source.SignalOperatorDto;
            dest.StartOperatorDto = source.StartOperatorDto;
            dest.EndOperatorDto = source.EndOperatorDto;
            dest.SamplingRateOperatorDto = source.SamplingRateOperatorDto;
            dest.ChannelCount = source.ChannelCount;
            dest.InterpolationTypeEnum = source.InterpolationTypeEnum;
            dest.SpeakerSetupEnum = source.SpeakerSetupEnum;
            dest.ArrayDto = source.ArrayDto;

            Clone_OperatorBaseProperties(source, dest);
        }

        public static void TryClone_CacheOperatorProperties(Cache_OperatorDtoBase_NotConstSignal source, IOperatorDto dest)
        {
            var castedDest = dest as Cache_OperatorDtoBase_NotConstSignal;
            if (castedDest != null)
            {
                Clone_CacheOperatorProperties(source, castedDest);
            }

            TryClone_WithDimensionProperties(source, dest);
        }

        public static void Clone_ClosestOverDimensionProperties(ClosestOverDimension_OperatorDto source, ClosestOverDimension_OperatorDto dest)
        {
            dest.InputOperatorDto = source.InputOperatorDto;
            dest.CollectionOperatorDto = source.CollectionOperatorDto;
            dest.FromOperatorDto = source.FromOperatorDto;
            dest.TillOperatorDto = source.TillOperatorDto;
            dest.StepOperatorDto = source.StepOperatorDto;
            dest.CollectionRecalculationEnum = source.CollectionRecalculationEnum;

            Clone_WithDimensionProperties(source, dest);
        }

        public static void TryClone_CurveProperties(Curve_OperatorDto source, IOperatorDto dest)
        {
            {
                var castedDest = dest as Curve_OperatorDtoBase_WithMinX;
                if (castedDest != null)
                {
                    Clone_CurveProperties_WithMinX(source, castedDest);
                    return;
                }
            }

            {
                var castedDest = dest as Curve_OperatorDtoBase_WithoutMinX;
                if (castedDest != null)
                {
                    Clone_CurveProperties_WithoutMinX(source, castedDest);
                }
            }
        }

        public static void Clone_CurveProperties_WithMinX(Curve_OperatorDto source, Curve_OperatorDtoBase_WithMinX dest)
        {
            if (!source.CurveID.HasValue) throw new NullException(() => source.CurveID);

            dest.MinX = source.MinX;

            Clone_CurveProperties_WithoutMinX(source, dest);
        }

        public static void Clone_CurveProperties_WithoutMinX(Curve_OperatorDto source, Curve_OperatorDtoBase_WithoutMinX dest)
        {
            if (!source.CurveID.HasValue) throw new NullException(() => source.CurveID);

            dest.CurveID = source.CurveID.Value;
            dest.ArrayDto = source.ArrayDto;

            Clone_WithDimensionProperties(source, dest);
        }

        public static void Clone_WithDimensionProperties(IOperatorDto_WithDimension source, IOperatorDto_WithDimension dest)
        {
            dest.CanonicalCustomDimensionName = source.CanonicalCustomDimensionName;
            dest.StandardDimensionEnum = source.StandardDimensionEnum;

            Clone_OperatorBaseProperties(source, dest);
        }

        public static void TryClone_WithDimensionProperties(IOperatorDto_WithDimension source, IOperatorDto dest)
        {
            var castedDest = dest as IOperatorDto_WithDimension;
            if (castedDest != null)
            {
                Clone_WithDimensionProperties(source, castedDest);
            }
        }

        public static void TryClone_FilterProperties(OperatorDtoBase_Filter_VarSignal source, IOperatorDto dest)
        {
            var castedDest = dest as OperatorDtoBase_Filter_VarSignal;
            if (castedDest != null)
            {
                Clone_FilterProperties(source, castedDest);
            }
        }

        public static void Clone_FilterProperties(OperatorDtoBase_Filter_VarSignal source, OperatorDtoBase_Filter_VarSignal dest)
        {
            dest.SignalOperatorDto = source.SignalOperatorDto;
            dest.SamplingRate = source.SamplingRate;
            dest.NyquistFrequency = source.NyquistFrequency;

            Clone_OperatorBaseProperties(source, dest);
        }

        public static void Clone_InterpolateOperatorProperties(IInterpolate_OperatorDto_VarSignal source, IInterpolate_OperatorDto_VarSignal dest)
        {
            dest.ResampleInterpolationTypeEnum = source.ResampleInterpolationTypeEnum;
            dest.SignalOperatorDto = source.SignalOperatorDto;

            Clone_WithDimensionProperties(source, dest);
        }

        public static void TryClone_InterpolateOperatorProperties(Interpolate_OperatorDto source, IOperatorDto dest)
        {
            var castedDest = dest as IInterpolate_OperatorDto_VarSignal;
            if (castedDest != null)
            {
                Clone_InterpolateOperatorProperties(source, castedDest);
            }
        }

        public static void Clone_RandomOperatorProperties(IRandom_OperatorDto source, IRandom_OperatorDto dest)
        {
            dest.ResampleInterpolationTypeEnum = source.ResampleInterpolationTypeEnum;
            dest.OperatorID = source.OperatorID;
            dest.ArrayDto = source.ArrayDto;

            Clone_WithDimensionProperties(source, dest);
        }

        public static void TryClone_RandomOperatorProperties(IRandom_OperatorDto source, IOperatorDto dest)
        {
            var castedDest = dest as IRandom_OperatorDto;
            if (castedDest != null)
            {
                Clone_RandomOperatorProperties(source, castedDest);
            }
        }

        public static void Clone_SampleProperties(Sample_OperatorDto source, ISample_OperatorDto_WithSampleID dest)
        {
            if (!source.SampleID.HasValue) throw new NullException(() => source.SampleID);

            dest.SampleID = source.SampleID.Value;
            dest.SampleChannelCount = source.SampleChannelCount;
            dest.InterpolationTypeEnum = source.InterpolationTypeEnum;
            dest.TargetChannelCount = source.TargetChannelCount;
            dest.ArrayDtos = source.ArrayDtos;

            Clone_WithDimensionProperties(source, dest);
        }

        public static void TryClone_SampleProperties(Sample_OperatorDto source, IOperatorDto dest)
        {
            var castedDest = dest as ISample_OperatorDto_WithSampleID;
            if (castedDest != null)
            {
                Clone_SampleProperties(source, castedDest);
            }
        }

        public static void Clone_OperatorBaseProperties([NotNull] IOperatorDto source, [NotNull] IOperatorDto dest)
        {
            if (source == null) throw new ArgumentNullException(nameof(source));
            if (dest == null) throw new ArgumentNullException(nameof(dest));

            dest.OperatorID = source.OperatorID;
        }
    }
}
