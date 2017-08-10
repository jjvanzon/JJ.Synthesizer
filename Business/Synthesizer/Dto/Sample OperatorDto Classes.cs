using System.Collections.Generic;
using JJ.Business.Synthesizer.Enums;

namespace JJ.Business.Synthesizer.Dto
{
    internal class Sample_OperatorDto : Sample_OperatorDtoBase_WithSampleID
    { }

    internal class Sample_OperatorDto_NoSample : OperatorDtoBase_WithoutInputs
    {
        public override OperatorTypeEnum OperatorTypeEnum => OperatorTypeEnum.Sample;
    }

    internal class Sample_OperatorDto_ZeroFrequency : OperatorDtoBase_ZeroFrequency
    {
        public override OperatorTypeEnum OperatorTypeEnum => OperatorTypeEnum.Sample;
    }

    internal class Sample_OperatorDto_VarFrequency_WithPhaseTracking : Sample_OperatorDtoBase_WithSampleID
    { }

    internal class Sample_OperatorDto_ConstFrequency_WithOriginShifting : Sample_OperatorDtoBase_WithSampleID
    { }

    internal class Sample_OperatorDto_VarFrequency_MonoToStereo_WithPhaseTracking : Sample_OperatorDtoBase_WithSampleID
    { }

    internal class Sample_OperatorDto_ConstFrequency_MonoToStereo_WithOriginShifting : Sample_OperatorDtoBase_WithSampleID
    { }

    internal class Sample_OperatorDto_VarFrequency_StereoToMono_WithPhaseTracking : Sample_OperatorDtoBase_WithSampleID
    { }

    internal class Sample_OperatorDto_ConstFrequency_StereoToMono_WithOriginShifting : Sample_OperatorDtoBase_WithSampleID
    { }

    internal class Sample_OperatorDto_VarFrequency_NoPhaseTracking : Sample_OperatorDtoBase_WithSampleID
    { }

    internal class Sample_OperatorDto_ConstFrequency_NoOriginShifting : Sample_OperatorDtoBase_WithSampleID
    { }

    internal class Sample_OperatorDto_VarFrequency_MonoToStereo_NoPhaseTracking : Sample_OperatorDtoBase_WithSampleID
    { }

    internal class Sample_OperatorDto_ConstFrequency_MonoToStereo_NoOriginShifting : Sample_OperatorDtoBase_WithSampleID
    { }

    internal class Sample_OperatorDto_VarFrequency_StereoToMono_NoPhaseTracking : Sample_OperatorDtoBase_WithSampleID
    { }

    internal class Sample_OperatorDto_ConstFrequency_StereoToMono_NoOriginShifting : Sample_OperatorDtoBase_WithSampleID
    { }

    internal abstract class Sample_OperatorDtoBase_WithSampleID : OperatorDtoBase_WithFrequency, IOperatorDto_WithAdditionalChannelDimension, IOperatorDto_WithTargetChannelCount
    {
        public override OperatorTypeEnum OperatorTypeEnum => OperatorTypeEnum.Sample;

        public InterpolationTypeEnum InterpolationTypeEnum { get; set; }
        public int SampleChannelCount { get; set; }
        public int TargetChannelCount { get; set; }
        /// <summary> 0 in case of no sample. (That made it easier than nullable in one DTO, not nullable in the other.) </summary>
        public int SampleID { get; set; }
        public int ChannelDimensionStackLevel { get; set; }
        public IList<ArrayDto> ArrayDtos { get; set; }
    }
}
