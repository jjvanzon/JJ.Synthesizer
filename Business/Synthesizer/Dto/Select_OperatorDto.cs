using JJ.Business.Synthesizer.Enums;

namespace JJ.Business.Synthesizer.Dto
{
    internal class Select_OperatorDto : OperatorDto
    {
        public override string OperatorTypeName => nameof(OperatorTypeEnum.Select);

        public OperatorDto SignalOperatorDto => InputOperatorDtos[0];
        public OperatorDto PositionOperatorDto => InputOperatorDtos[1];

        public Select_OperatorDto(OperatorDto signalOperatorDto, OperatorDto positionOperatorDto)
            : base(new OperatorDto[] { signalOperatorDto, positionOperatorDto })
        { }
    }
}
