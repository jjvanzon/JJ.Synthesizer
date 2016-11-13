using JJ.Business.Synthesizer.Enums;

namespace JJ.Business.Synthesizer.Dto
{
    internal class Pulse_OperatorDto : OperatorDto
    {
        public override string OperatorTypeName => nameof(OperatorTypeEnum.Pulse);

        public OperatorDto FrequencyOperatorDto => InputOperatorDtos[0];
        public OperatorDto WidthOperatorDto => InputOperatorDtos[1];

        public Pulse_OperatorDto(
            OperatorDto frequencyOperatorDto,
            OperatorDto widthOperatorDto)
            : base(new OperatorDto[] { frequencyOperatorDto, widthOperatorDto})
        { }
    }
}