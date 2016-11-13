using JJ.Business.Synthesizer.Enums;

namespace JJ.Business.Synthesizer.Dto
{
    internal class Power_OperatorDto : OperatorDto
    {
        public override string OperatorTypeName => nameof(OperatorTypeEnum.Power);

        public OperatorDto BaseOperatorDto => InputOperatorDtos[0];
        public OperatorDto ExponentOperatorDto => InputOperatorDtos[1];

        public Power_OperatorDto(
            OperatorDto baseOperatorDto,
            OperatorDto exponentOperatorDto)
            : base(new OperatorDto[] { baseOperatorDto, exponentOperatorDto })
        { }
    }
}
