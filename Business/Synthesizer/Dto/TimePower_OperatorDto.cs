using JJ.Business.Synthesizer.Enums;

namespace JJ.Business.Synthesizer.Dto
{
    internal class TimePower_OperatorDto : OperatorDto
    {
        public override string OperatorTypeName => nameof(OperatorTypeEnum.TimePower);

        public OperatorDto SignalOperatorDto => ChildOperatorDtos[0];
        public OperatorDto ExponentOperatorDto => ChildOperatorDtos[1];
        public OperatorDto OriginOperatorDto => ChildOperatorDtos[2];

        public TimePower_OperatorDto(
            OperatorDto signalOperatorDto,
            OperatorDto exponentOperatorDto,
            OperatorDto originOperatorDto)
            : base(new OperatorDto[] { signalOperatorDto, exponentOperatorDto, originOperatorDto })
        { }
    }
}