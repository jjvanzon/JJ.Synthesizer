using JJ.Business.Synthesizer.Enums;

namespace JJ.Business.Synthesizer.Dto
{
    internal class PatchInlet_OperatorDto : OperatorDtoBase_WithoutInputOperatorDtos
    {
        public override string OperatorTypeName => nameof(OperatorTypeEnum.PatchInlet);

        public double DefaultValue { get; set; }
        public int ListIndex { get; set; }
        public string Name { get; set; }
        public DimensionEnum DimensionEnum { get; set; }
    }
}
