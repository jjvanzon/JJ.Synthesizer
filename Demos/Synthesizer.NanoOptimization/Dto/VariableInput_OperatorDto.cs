using JJ.Demos.Synthesizer.NanoOptimization.Helpers;

namespace JJ.Demos.Synthesizer.NanoOptimization.Dto
{
    internal class VariableInput_OperatorDto : OperatorDtoBase
    {
        public double DefaultValue { get; set; }
        public override string OperatorTypeName => OperatorNames.VariableInput;

        public VariableInput_OperatorDto(double defaultValue)
            : base(new OperatorDtoBase[0])
        {
            DefaultValue = defaultValue;
        }
    }
}
