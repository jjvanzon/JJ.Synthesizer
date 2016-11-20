using JJ.Business.Synthesizer.Enums;
using JJ.Data.Synthesizer;

namespace JJ.Business.Synthesizer.Dto
{
    internal class Sample_OperatorDto : Sample_OperatorDto_VarFrequency
    { }

    internal abstract class Sample_OperatorDto_VarFrequency : OperatorDtoBase_VarFrequency
    {
        public override string OperatorTypeName => nameof(OperatorTypeEnum.Sample);

        public InterpolationTypeEnum InterpolationTypeEnum { get; set; }
        public Sample Sample { get; set; }
    }

    internal abstract class Sample_OperatorDto_ConstFrequency : OperatorDtoBase_ConstFrequency
    {
        public override string OperatorTypeName => nameof(OperatorTypeEnum.Sample);

        public InterpolationTypeEnum InterpolationTypeEnum { get; set; }
        public Sample Sample { get; set; }
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
}
