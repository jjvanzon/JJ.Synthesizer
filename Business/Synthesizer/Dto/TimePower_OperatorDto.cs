using JJ.Business.Synthesizer.Enums;

namespace JJ.Business.Synthesizer.Dto
{
    internal class TimePower_OperatorDto : OperatorDto
    {
        public override string OperatorTypeName => nameof(OperatorTypeEnum.TimePower);

        public OperatorDto SignalOperatorDto => InputOperatorDtos[0];
        public OperatorDto ExponentOperatorDto => InputOperatorDtos[1];
        public OperatorDto OriginOperatorDto => InputOperatorDtos[2];

        public TimePower_OperatorDto(
            OperatorDto signalOperatorDto,
            OperatorDto exponentOperatorDto,
            OperatorDto originOperatorDto)
            : base(new OperatorDto[] { signalOperatorDto, exponentOperatorDto, originOperatorDto })
        { }
    }
}