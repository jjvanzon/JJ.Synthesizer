using JJ.Business.Synthesizer.Enums;

namespace JJ.Business.Synthesizer.Dto
{
    internal class HighShelfFilter_OperatorDto : OperatorDtoBase_ShelfFilter
    {
        public override string OperatorTypeName => nameof(OperatorTypeEnum.HighShelfFilter);

        public HighShelfFilter_OperatorDto(
            OperatorDtoBase signalOperatorDto, 
            OperatorDtoBase transitionFrequencyOperatorDto, 
            OperatorDtoBase transitionSlopeOperatorDto, 
            OperatorDtoBase dbGainOperatorDto) 
            : base(
                  signalOperatorDto, 
                  transitionFrequencyOperatorDto, 
                  transitionSlopeOperatorDto, 
                  dbGainOperatorDto)
        { }
    }
}
