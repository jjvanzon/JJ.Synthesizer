using JJ.Business.Synthesizer.Enums;

namespace JJ.Business.Synthesizer.Dto
{
    internal class Pulse_OperatorDto : OperatorDtoBase
    {
        public override string OperatorTypeName => nameof(OperatorTypeEnum.Pulse);

        public OperatorDtoBase FrequencyOperatorDto => InputOperatorDtos[0];
        public OperatorDtoBase WidthOperatorDto => InputOperatorDtos[1];

        public Pulse_OperatorDto(
            OperatorDtoBase frequencyOperatorDto,
            OperatorDtoBase widthOperatorDto)
            : base(new OperatorDtoBase[] { frequencyOperatorDto, widthOperatorDto})
        { }
    }
}