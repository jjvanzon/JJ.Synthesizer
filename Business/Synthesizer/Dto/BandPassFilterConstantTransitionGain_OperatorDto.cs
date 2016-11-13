using JJ.Business.Synthesizer.Enums;

namespace JJ.Business.Synthesizer.Dto
{
    internal class BandPassFilterConstantTransitionGain_OperatorDto : OperatorDtoBase_BandPassFilter
    {
        public override string OperatorTypeName => nameof(OperatorTypeEnum.BandPassFilterConstantTransitionGain);

        public BandPassFilterConstantTransitionGain_OperatorDto(
            OperatorDtoBase signalOperatorDto, 
            OperatorDtoBase centerFrequencyOperatorDto, 
            OperatorDtoBase bandWidthOperatorDto) 
            : base(signalOperatorDto, centerFrequencyOperatorDto, bandWidthOperatorDto)
        {
        }
    }
}
