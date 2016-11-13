using JJ.Business.Synthesizer.Enums;

namespace JJ.Business.Synthesizer.Dto
{
    internal class Round_OperatorDto : OperatorDtoBase
    {
        public override string OperatorTypeName => nameof(OperatorTypeEnum.Round);

        public OperatorDtoBase SignalOperatorDto => InputOperatorDtos[0];
        public OperatorDtoBase StepOperatorDto => InputOperatorDtos[1];
        public OperatorDtoBase OffsetOperatorDto => InputOperatorDtos[2];

        public Round_OperatorDto(
            OperatorDtoBase signalOperatorDto,
            OperatorDtoBase stepOperatorDto,
            OperatorDtoBase offsetOperatorDto)
            : base(new OperatorDtoBase[] { signalOperatorDto, stepOperatorDto, offsetOperatorDto })
        { }
    }
}