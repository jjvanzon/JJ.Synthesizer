using JJ.Business.Synthesizer.Enums;

namespace JJ.Business.Synthesizer.Dto
{
    internal class VariableInput_OperatorDto : OperatorDtoBase_WithoutInputOperatorDtos
    {
        public override OperatorTypeEnum OperatorTypeEnum => OperatorTypeEnum.PatchInlet;

        public double DefaultValue { get; set; }
        public int ListIndex { get; set; }
        public string CanonicalName { get; set; }
        public DimensionEnum DimensionEnum { get; set; }
    }
}
