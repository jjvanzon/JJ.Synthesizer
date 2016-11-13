using JJ.Business.Synthesizer.Enums;

namespace JJ.Business.Synthesizer.Dto
{
    internal class If_OperatorDto : OperatorDtoBase
    {
        public override string OperatorTypeName => nameof(OperatorTypeEnum.If);

        public OperatorDtoBase ConditionOperatorDto => InputOperatorDtos[0];
        public OperatorDtoBase ThenOperatorDto => InputOperatorDtos[1];
        public OperatorDtoBase ElseOperatorDto => InputOperatorDtos[2];

        public If_OperatorDto(
            OperatorDtoBase conditionOperatorDto,
            OperatorDtoBase thenOperatorDto,
            OperatorDtoBase elseOperatorDto)
            : base(new OperatorDtoBase[] { conditionOperatorDto, thenOperatorDto, elseOperatorDto })
        { }
    }
}