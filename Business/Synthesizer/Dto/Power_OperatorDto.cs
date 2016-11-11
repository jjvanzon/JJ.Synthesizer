using JJ.Business.Synthesizer.Enums;

namespace JJ.Business.Synthesizer.Dto
{
    internal class Power_OperatorDto : OperatorDto
    {
        public override string OperatorTypeName => nameof(OperatorTypeEnum.Power);

        public OperatorDto BaseOperatorDto => ChildOperatorDtos[0];
        public OperatorDto ExponentOperatorDto => ChildOperatorDtos[1];

        public Power_OperatorDto(
            OperatorDto baseOperatorDto,
            OperatorDto exponentOperatorDto)
            : base(new OperatorDto[] { baseOperatorDto, exponentOperatorDto })
        { }
    }
}
