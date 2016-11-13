using JJ.Business.Synthesizer.Enums;

namespace JJ.Business.Synthesizer.Dto
{
    internal class Select_OperatorDto : OperatorDtoBase
    {
        public override string OperatorTypeName => nameof(OperatorTypeEnum.Select);

        public OperatorDtoBase SignalOperatorDto => InputOperatorDtos[0];
        public OperatorDtoBase PositionOperatorDto => InputOperatorDtos[1];

        public Select_OperatorDto(OperatorDtoBase signalOperatorDto, OperatorDtoBase positionOperatorDto)
            : base(new OperatorDtoBase[] { signalOperatorDto, positionOperatorDto })
        { }
    }
}
