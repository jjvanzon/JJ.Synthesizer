using JJ.Business.Synthesizer.Enums;

namespace JJ.Business.Synthesizer.Dto
{
    internal class Reset_OperatorDto : OperatorDto
    {
        public override string OperatorTypeName => nameof(OperatorTypeEnum.Reset);

        public OperatorDto PassThroughInputOperatorDto => InputOperatorDtos[0];

        public Reset_OperatorDto(OperatorDto passThroughInputOperatorDto)
            : base(new OperatorDto[] { passThroughInputOperatorDto })
        { }
    }
}
