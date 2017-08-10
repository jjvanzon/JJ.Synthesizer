//using System;

//using JJ.Business.Synthesizer.Dto;
//using JJ.Framework.Exceptions;

//namespace JJ.Business.Synthesizer.Helpers
//{
//    internal static class DtoCloner
//    {
//        public static void CloneProperties(OperatorDtoBase_AggregateOverDimension source, OperatorDtoBase_AggregateOverDimension dest)
//        {
//            dest.CollectionRecalculationEnum = source.CollectionRecalculationEnum;

//            CloneProperties((IOperatorDto_WithDimension)source, dest);
//        }

//        public static void TryClone_AggregateOverDimensionProperties(OperatorDtoBase_AggregateOverDimension source, IOperatorDto dest)
//        {
//            if (dest is OperatorDtoBase_AggregateOverDimension castedDest)
//            {
//                CloneProperties(source, castedDest);
//            }
//        }

//        public static void CloneProperties(Cache_OperatorDtoBase_NotConstSignal source, Cache_OperatorDtoBase_NotConstSignal dest)
//        {
//            dest.ChannelCount = source.ChannelCount;
//            dest.InterpolationTypeEnum = source.InterpolationTypeEnum;
//            dest.SpeakerSetupEnum = source.SpeakerSetupEnum;
//            dest.ArrayDto = source.ArrayDto;

//            CloneProperties((IOperatorDto)source, dest);
//        }

//        public static void TryClone_CacheOperatorProperties(Cache_OperatorDtoBase_NotConstSignal source, IOperatorDto dest)
//        {
//            if (dest is Cache_OperatorDtoBase_NotConstSignal castedDest)
//            {
//                CloneProperties(source, castedDest);
//            }

//            TryClone_WithDimensionProperties(source, dest);
//        }

//        public static void CloneProperties(ClosestOverDimension_OperatorDto source, ClosestOverDimension_OperatorDto dest)
//        {
//            dest.CollectionRecalculationEnum = source.CollectionRecalculationEnum;

//            CloneProperties((IOperatorDto_WithDimension)source, dest);
//        }

//        public static void TryClone_CurveProperties(Curve_OperatorDto source, IOperatorDto dest)
//        {
//            {
//                if (dest is Curve_OperatorDtoBase_WithMinX castedDest)
//                {
//                    CloneProperties(source, castedDest);
//                    return;
//                }
//            }

//            {
//                if (dest is Curve_OperatorDtoBase_WithoutMinX castedDest)
//                {
//                    CloneProperties(source, castedDest);
//                }
//            }
//        }

//        public static void CloneProperties(Curve_OperatorDto source, Curve_OperatorDtoBase_WithMinX dest)
//        {
//            dest.MinX = source.MinX;

//            CloneProperties(source, (Curve_OperatorDtoBase_WithoutMinX)dest);
//        }

//        public static void CloneProperties(Curve_OperatorDto source, Curve_OperatorDtoBase_WithoutMinX dest)
//        {
//            dest.CurveID = source.CurveID;
//            dest.ArrayDto = source.ArrayDto;

//            CloneProperties((IOperatorDto_WithDimension)source, dest);
//        }

//        public static void CloneProperties(IOperatorDto_WithDimension source, IOperatorDto_WithDimension dest)
//        {
//            dest.CanonicalCustomDimensionName = source.CanonicalCustomDimensionName;
//            dest.StandardDimensionEnum = source.StandardDimensionEnum;

//            CloneProperties((IOperatorDto)source, dest);
//        }

//        public static void TryClone_WithDimensionProperties(IOperatorDto_WithDimension source, IOperatorDto dest)
//        {
//            if (dest is IOperatorDto_WithDimension castedDest)
//            {
//                CloneProperties(source, castedDest);
//            }
//        }

//        public static void TryClone_FilterProperties(OperatorDtoBase_Filter_VarSound source, IOperatorDto dest)
//        {
//            if (dest is OperatorDtoBase_Filter_VarSound castedDest)
//            {
//                CloneProperties(source, castedDest);
//            }
//        }

//        public static void CloneProperties(OperatorDtoBase_Filter_VarSound source, OperatorDtoBase_Filter_VarSound dest)
//        {
//            dest.TargetSamplingRate = source.TargetSamplingRate;
//            dest.NyquistFrequency = source.NyquistFrequency;

//            CloneProperties((IOperatorDto)source, dest);
//        }

//        public static void Clone_InterpolateOperatorProperties(Interpolate_OperatorDto source, Interpolate_OperatorDto dest)
//        {
//            dest.ResampleInterpolationTypeEnum = source.ResampleInterpolationTypeEnum;

//            CloneProperties(source, dest);
//        }

//        public static void TryClone_InterpolateOperatorProperties(Interpolate_OperatorDto source, IOperatorDto dest)
//        {
//            if (dest is Interpolate_OperatorDto castedDest)
//            {
//                Clone_InterpolateOperatorProperties(source, castedDest);
//            }
//        }

//        public static void CloneProperties(Random_OperatorDto source, Random_OperatorDto dest)
//        {
//            dest.ResampleInterpolationTypeEnum = source.ResampleInterpolationTypeEnum;
//            dest.ArrayDto = source.ArrayDto;

//            CloneProperties((IOperatorDto_WithDimension)source, dest);
//        }

//        public static void TryClone_RandomOperatorProperties(Random_OperatorDto source, IOperatorDto dest)
//        {
//            if (dest is Random_OperatorDto castedDest)
//            {
//                CloneProperties(source, castedDest);
//            }
//        }

//        public static void CloneProperties(Sample_OperatorDto source, Sample_OperatorDtoBase_WithSampleID dest)
//        {
//            dest.SampleID = source.SampleID;
//            dest.SampleChannelCount = source.SampleChannelCount;
//            dest.InterpolationTypeEnum = source.InterpolationTypeEnum;
//            dest.TargetChannelCount = source.TargetChannelCount;
//            dest.ArrayDtos = source.ArrayDtos;

//            CloneProperties((IOperatorDto_WithDimension)source, dest);
//        }

//        public static void TryClone_SampleProperties(Sample_OperatorDto source, IOperatorDto dest)
//        {
//            if (dest is Sample_OperatorDtoBase_WithSampleID castedDest)
//            {
//                CloneProperties(source, castedDest);
//            }
//        }

//        public static void CloneProperties(IOperatorDto source, IOperatorDto dest)
//        {
//            if (source == null) throw new ArgumentNullException(nameof(source));
//            if (dest == null) throw new ArgumentNullException(nameof(dest));

//            dest.Inputs = source.Inputs;
//            dest.OperatorID = source.OperatorID;
//            dest.OperationIdentity = source.OperationIdentity;
//        }
//    }
//}
