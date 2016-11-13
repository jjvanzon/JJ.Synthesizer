using JJ.Business.Synthesizer.Enums;

namespace JJ.Business.Synthesizer.Dto
{
    internal class Power_OperatorDto : OperatorDtoBase
    {
        public override string OperatorTypeName => nameof(OperatorTypeEnum.Power);

        public OperatorDtoBase BaseOperatorDto => InputOperatorDtos[0];
        public OperatorDtoBase ExponentOperatorDto => InputOperatorDtos[1];

        public Power_OperatorDto(
            OperatorDtoBase baseOperatorDto,
            OperatorDtoBase exponentOperatorDto)
            : base(new OperatorDtoBase[] { baseOperatorDto, exponentOperatorDto })
        { }
    }
}
