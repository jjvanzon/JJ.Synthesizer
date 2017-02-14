using JJ.Business.Synthesizer.Enums;

namespace JJ.Business.Synthesizer.Dto
{
    internal class Sample_OperatorDto : OperatorDtoBase_VarFrequency
    {
        public override OperatorTypeEnum OperatorTypeEnum => OperatorTypeEnum.Sample;

        public InterpolationTypeEnum InterpolationTypeEnum { get; set; }
        public int ChannelCount { get; set; }
        public int? SampleID { get; set; }
    }

    internal class Sample_OperatorDto_NoSample : OperatorDtoBase_WithoutInputOperatorDtos
    {
        public override OperatorTypeEnum OperatorTypeEnum => OperatorTypeEnum.Sample;
    }

    internal class Sample_OperatorDto_ZeroFrequency : OperatorDtoBase_WithoutInputOperatorDtos
    {
        public override OperatorTypeEnum OperatorTypeEnum => OperatorTypeEnum.Sample;
    }

    internal class Sample_OperatorDto_VarFrequency_WithPhaseTracking : Sample_OperatorDto_VarFrequency
    { }

    internal class Sample_OperatorDto_ConstFrequency_WithOriginShifting : Sample_OperatorDto_ConstFrequency
    { }

    internal class Sample_OperatorDto_VarFrequency_MonoToStereo_WithPhaseTracking : Sample_OperatorDto_VarFrequency
    { }

    internal class Sample_OperatorDto_ConstFrequency_MonoToStereo_WithOriginShifting : Sample_OperatorDto_ConstFrequency
    { }

    internal class Sample_OperatorDto_VarFrequency_StereoToMono_WithPhaseTracking : Sample_OperatorDto_VarFrequency
    { }

    internal class Sample_OperatorDto_ConstFrequency_StereoToMono_WithOriginShifting : Sample_OperatorDto_ConstFrequency
    { }

    internal class Sample_OperatorDto_VarFrequency_NoPhaseTracking : Sample_OperatorDto_VarFrequency
    { }

    internal class Sample_OperatorDto_ConstFrequency_NoOriginShifting : Sample_OperatorDto_ConstFrequency
    { }

    internal class Sample_OperatorDto_VarFrequency_MonoToStereo_NoPhaseTracking : Sample_OperatorDto_VarFrequency
    { }

    internal class Sample_OperatorDto_ConstFrequency_MonoToStereo_NoOriginShifting : Sample_OperatorDto_ConstFrequency
    { }

    internal class Sample_OperatorDto_VarFrequency_StereoToMono_NoPhaseTracking : Sample_OperatorDto_VarFrequency
    { }

    internal class Sample_OperatorDto_ConstFrequency_StereoToMono_NoOriginShifting : Sample_OperatorDto_ConstFrequency
    { }

    internal abstract class Sample_OperatorDto_VarFrequency : OperatorDtoBase_VarFrequency, ISample_OperatorDto_WithSampleID
    {
        public override OperatorTypeEnum OperatorTypeEnum => OperatorTypeEnum.Sample;

        public InterpolationTypeEnum InterpolationTypeEnum { get; set; }
        public int ChannelCount { get; set; }
        public int SampleID { get; set; }
        public int ChannelDimensionStackLevel { get; set; }
    }

    internal abstract class Sample_OperatorDto_ConstFrequency : OperatorDtoBase_ConstFrequency, ISample_OperatorDto_WithSampleID
    {
        public override OperatorTypeEnum OperatorTypeEnum => OperatorTypeEnum.Sample;

        public InterpolationTypeEnum InterpolationTypeEnum { get; set; }
        public int ChannelCount { get; set; }
        public int SampleID { get; set; }
        public int ChannelDimensionStackLevel { get; set; }
    }

    internal interface ISample_OperatorDto_WithSampleID : IOperatorDto_WithDimension, IOperatorDto_WithAdditionalChannelDimension
    {
        InterpolationTypeEnum InterpolationTypeEnum { get; set; }
        int ChannelCount { get; set; }
        int SampleID { get; set; }
    }
}
