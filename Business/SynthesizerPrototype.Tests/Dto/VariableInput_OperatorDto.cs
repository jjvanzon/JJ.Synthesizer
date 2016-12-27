using JJ.Business.SynthesizerPrototype.Tests.Helpers;

namespace JJ.Business.SynthesizerPrototype.Tests.Dto
{
    internal class VariableInput_OperatorDto : OperatorDtoBase_WithoutInputOperatorDtos
    {
        public double DefaultValue { get; set; }
        public override string OperatorTypeName => nameof(OperatorTypeEnum.VariableInput);
    }
}
