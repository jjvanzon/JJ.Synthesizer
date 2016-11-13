using JJ.Business.Synthesizer.Enums;

namespace JJ.Business.Synthesizer.Dto
{
    internal class TimePower_OperatorDto : OperatorDtoBase
    {
        public override string OperatorTypeName => nameof(OperatorTypeEnum.TimePower);

        public OperatorDtoBase SignalOperatorDto => InputOperatorDtos[0];
        public OperatorDtoBase ExponentOperatorDto => InputOperatorDtos[1];
        public OperatorDtoBase OriginOperatorDto => InputOperatorDtos[2];

        public TimePower_OperatorDto(
            OperatorDtoBase signalOperatorDto,
            OperatorDtoBase exponentOperatorDto,
            OperatorDtoBase originOperatorDto)
            : base(new OperatorDtoBase[] { signalOperatorDto, exponentOperatorDto, originOperatorDto })
        { }
    }
}