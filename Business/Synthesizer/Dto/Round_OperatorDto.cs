using JJ.Business.Synthesizer.Enums;

namespace JJ.Business.Synthesizer.Dto
{
    internal class Round_OperatorDto : OperatorDto
    {
        public override string OperatorTypeName => nameof(OperatorTypeEnum.Round);

        public OperatorDto SignalOperatorDto => InputOperatorDtos[0];
        public OperatorDto StepOperatorDto => InputOperatorDtos[1];
        public OperatorDto OffsetOperatorDto => InputOperatorDtos[2];

        public Round_OperatorDto(
            OperatorDto signalOperatorDto,
            OperatorDto stepOperatorDto,
            OperatorDto offsetOperatorDto)
            : base(new OperatorDto[] { signalOperatorDto, stepOperatorDto, offsetOperatorDto })
        { }
    }
}