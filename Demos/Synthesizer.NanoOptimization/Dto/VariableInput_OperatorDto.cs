using JJ.Demos.Synthesizer.NanoOptimization.Helpers;

namespace JJ.Demos.Synthesizer.NanoOptimization.Dto
{
    internal class VariableInput_OperatorDto : OperatorDtoBase_WithoutInputOperatorDtos
    {
        public double DefaultValue { get; set; }
        public override string OperatorTypeName => OperatorNames.VariableInput;
    }
}
