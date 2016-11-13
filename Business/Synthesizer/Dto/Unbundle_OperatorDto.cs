using JJ.Business.Synthesizer.Enums;

namespace JJ.Business.Synthesizer.Dto
{
    internal class Unbundle_OperatorDto : OperatorDtoBase
    {
        public override string OperatorTypeName => nameof(OperatorTypeEnum.Unbundle);
        
        // TODO: You need information to identify which outlet of the Unbundle Operator is represented here.

        public OperatorDtoBase InputOperatorDto => InputOperatorDtos[0];

        public DimensionEnum StandardDimensionEnum { get; set; }
        public string CustomDimensionName { get; set; }
        public int OutletIndex { get; set; }

        public Unbundle_OperatorDto(OperatorDtoBase inputOperatorDto)
            : base(new OperatorDtoBase[] { inputOperatorDto })
        { }
    }
}
