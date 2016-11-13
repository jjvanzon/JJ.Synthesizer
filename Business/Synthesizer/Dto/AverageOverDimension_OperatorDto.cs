using JJ.Business.Synthesizer.Enums;

namespace JJ.Business.Synthesizer.Dto
{
    internal class AverageOverDimension_OperatorDto : OperatorDtoBase_AggregateOverDimension
    {
        public override string OperatorTypeName => nameof(OperatorTypeEnum.AverageOverDimension);

        public AverageOverDimension_OperatorDto(
            OperatorDtoBase signalOperatorDto, 
            OperatorDtoBase fromOperatorDto, 
            OperatorDtoBase tillOperatorDto, 
            OperatorDtoBase stepOperatorDto) 
            : base(signalOperatorDto, fromOperatorDto, tillOperatorDto, stepOperatorDto)
        { }
    }
}
