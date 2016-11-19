using JJ.Business.Synthesizer.Tests.NanoOptimization.Helpers;

namespace JJ.Business.Synthesizer.Tests.NanoOptimization.Dto
{
    internal class VariableInput_OperatorDto : OperatorDtoBase_WithoutInputOperatorDtos
    {
        public double DefaultValue { get; set; }
        public override string OperatorTypeName => OperatorNames.VariableInput;
    }
}
