using JJ.Business.Synthesizer.Enums;

namespace JJ.Business.Synthesizer.Dto
{
    internal class Reset_OperatorDto : OperatorDtoBase
    {
        public override string OperatorTypeName => nameof(OperatorTypeEnum.Reset);

        public OperatorDtoBase PassThroughInputOperatorDto => InputOperatorDtos[0];

        public Reset_OperatorDto(OperatorDtoBase passThroughInputOperatorDto)
            : base(new OperatorDtoBase[] { passThroughInputOperatorDto })
        { }
    }
}
