using JJ.Business.Synthesizer.Enums;

namespace JJ.Business.Synthesizer.Dto
{
    internal class RangeOverDimension_OperatorDto : OperatorDtoBase
    {
        public override string OperatorTypeName => nameof(OperatorTypeEnum.RangeOverDimension);

        public OperatorDtoBase FromOperatorDto => InputOperatorDtos[0];
        public OperatorDtoBase TillOperatorDto => InputOperatorDtos[1];
        public OperatorDtoBase StepOperatorDto => InputOperatorDtos[2];

        public RangeOverDimension_OperatorDto(
            OperatorDtoBase fromOperatorDto,
            OperatorDtoBase tillOperatorDto,
            OperatorDtoBase stepOperatorDto)
            : base(new OperatorDtoBase[] { fromOperatorDto, tillOperatorDto, stepOperatorDto })
        { }
    }
}