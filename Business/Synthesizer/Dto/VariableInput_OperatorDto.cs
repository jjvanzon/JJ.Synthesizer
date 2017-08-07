using System.Collections.Generic;
using JJ.Business.Synthesizer.Enums;

namespace JJ.Business.Synthesizer.Dto
{
    internal class VariableInput_OperatorDto : OperatorDtoBase_WithoutInputDtos
    {
        public override OperatorTypeEnum OperatorTypeEnum => OperatorTypeEnum.PatchInlet;

        public double DefaultValue { get; set; }
        public int Position { get; set; }
        public string CanonicalName { get; set; }
        public DimensionEnum DimensionEnum { get; set; }
    }
}
