using JJ.Business.Synthesizer.Enums;

namespace JJ.Business.Synthesizer.Dto
{
    internal class If_OperatorDto : OperatorDto
    {
        public override string OperatorTypeName => nameof(OperatorTypeEnum.If);

        public OperatorDto ConditionOperatorDto => InputOperatorDtos[0];
        public OperatorDto ThenOperatorDto => InputOperatorDtos[1];
        public OperatorDto ElseOperatorDto => InputOperatorDtos[2];

        public If_OperatorDto(
            OperatorDto conditionOperatorDto,
            OperatorDto thenOperatorDto,
            OperatorDto elseOperatorDto)
            : base(new OperatorDto[] { conditionOperatorDto, thenOperatorDto, elseOperatorDto })
        { }
    }
}