using JJ.Business.Synthesizer.Enums;

namespace JJ.Business.Synthesizer.Dto
{
    internal class MinOverDimension_OperatorDto : OperatorDtoBase_AggregateOverDimension
    {
        public override string OperatorTypeName => nameof(OperatorTypeEnum.MinOverDimension);

        public MinOverDimension_OperatorDto(
            OperatorDtoBase signalOperatorDto, 
            OperatorDtoBase fromOperatorDto, 
            OperatorDtoBase tillOperatorDto, 
            OperatorDtoBase stepOperatorDto) 
            : base(signalOperatorDto, fromOperatorDto, tillOperatorDto, stepOperatorDto)
        { }
    }
}
