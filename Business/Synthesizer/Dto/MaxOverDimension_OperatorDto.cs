using JJ.Business.Synthesizer.Enums;

namespace JJ.Business.Synthesizer.Dto
{
    internal class MaxOverDimension_OperatorDto : OperatorDtoBase_AggregateOverDimension
    {
        public override string OperatorTypeName => nameof(OperatorTypeEnum.MaxOverDimension);

        public MaxOverDimension_OperatorDto(
            OperatorDtoBase signalOperatorDto, 
            OperatorDtoBase fromOperatorDto, 
            OperatorDtoBase tillOperatorDto, 
            OperatorDtoBase stepOperatorDto) 
            : base(signalOperatorDto, fromOperatorDto, tillOperatorDto, stepOperatorDto)
        { }
    }
}
