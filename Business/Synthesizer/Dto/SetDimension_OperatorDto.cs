using JJ.Business.Synthesizer.Enums;

namespace JJ.Business.Synthesizer.Dto
{
    internal class SetDimension_OperatorDto : OperatorDtoBase
    {
        public override string OperatorTypeName => nameof(OperatorTypeEnum.SetDimension);

        public OperatorDtoBase PassThroughInputOperatorDto => InputOperatorDtos[0];
        public OperatorDtoBase ValueOperatorDto => InputOperatorDtos[1];

        public DimensionEnum StandardDimensionEnum { get; set; }
        public string CustomDimensionName { get; set; }

        public SetDimension_OperatorDto(
            OperatorDtoBase passThroughInputOperatorDto,
            OperatorDtoBase valueOperatorDto)
            : base(new OperatorDtoBase[] { passThroughInputOperatorDto, valueOperatorDto })
        { }
    }
}