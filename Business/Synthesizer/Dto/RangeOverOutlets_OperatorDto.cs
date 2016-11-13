using JJ.Business.Synthesizer.Enums;

namespace JJ.Business.Synthesizer.Dto
{
    internal class RangeOverOutlets_OperatorDto : OperatorDtoBase
    {
        public override string OperatorTypeName => nameof(OperatorTypeEnum.RangeOverOutlets);

        public OperatorDtoBase FromOperatorDto => InputOperatorDtos[0];
        public OperatorDtoBase StepOperatorDto => InputOperatorDtos[1];

        public RangeOverOutlets_OperatorDto(
            OperatorDtoBase fromOperatorDto,
            OperatorDtoBase stepOperatorDto)
            : base(new OperatorDtoBase[] 
            {
                fromOperatorDto,
                stepOperatorDto
            })
        { }
    }
}