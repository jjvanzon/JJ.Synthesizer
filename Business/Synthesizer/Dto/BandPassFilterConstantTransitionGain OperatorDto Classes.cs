using JJ.Business.Synthesizer.Enums;

namespace JJ.Business.Synthesizer.Dto
{
    internal class BandPassFilterConstantTransitionGain_OperatorDto : OperatorDtoBase_BandPassFilter_VarCenterFrequency_VarBandWidth
    {
        public override string OperatorTypeName => nameof(OperatorTypeEnum.BandPassFilterConstantTransitionGain);

        public BandPassFilterConstantTransitionGain_OperatorDto(
            OperatorDtoBase signalOperatorDto,
            OperatorDtoBase centerFrequencyOperatorDto,
            OperatorDtoBase bandWidthOperatorDto)
            : base(signalOperatorDto, centerFrequencyOperatorDto, bandWidthOperatorDto)
        { }
    }

    internal class BandPassFilterConstantTransitionGain_OperatorDto_VarCenterFrequency_VarBandWidth : OperatorDtoBase_BandPassFilter_VarCenterFrequency_VarBandWidth
    {
        public override string OperatorTypeName => nameof(OperatorTypeEnum.BandPassFilterConstantTransitionGain);

        public BandPassFilterConstantTransitionGain_OperatorDto_VarCenterFrequency_VarBandWidth(
            OperatorDtoBase signalOperatorDto,
            OperatorDtoBase centerFrequencyOperatorDto,
            OperatorDtoBase bandWidthOperatorDto)
            : base(signalOperatorDto, centerFrequencyOperatorDto, bandWidthOperatorDto)
        { }
    }

    internal class BandPassFilterConstantTransitionGain_OperatorDto_ConstCenterFrequency_ConstBandWidth : OperatorDtoBase_BandPassFilter_ConstCenterFrequency_ConstBandWidth
    {
        public override string OperatorTypeName => nameof(OperatorTypeEnum.BandPassFilterConstantTransitionGain);

        public BandPassFilterConstantTransitionGain_OperatorDto_ConstCenterFrequency_ConstBandWidth(
            OperatorDtoBase signalOperatorDto,
            double centerFrequency,
            double bandWidth)
            : base(signalOperatorDto, centerFrequency, bandWidth)
        { }
    }
}
