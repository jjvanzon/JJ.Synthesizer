using JJ.Business.Synthesizer.Enums;

namespace JJ.Business.Synthesizer.Dto
{
    internal class BandPassFilterConstantPeakGain_OperatorDto : OperatorDtoBase_BandPassFilter
    {
        public override string OperatorTypeName => nameof(OperatorTypeEnum.BandPassFilterConstantPeakGain);

        public BandPassFilterConstantPeakGain_OperatorDto(
            OperatorDtoBase signalOperatorDto, 
            OperatorDtoBase centerFrequencyOperatorDto, 
            OperatorDtoBase bandWidthOperatorDto) 
            : base(signalOperatorDto, centerFrequencyOperatorDto, bandWidthOperatorDto)
        {
        }
    }
}
