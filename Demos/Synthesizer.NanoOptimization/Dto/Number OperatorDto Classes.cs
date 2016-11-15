using JJ.Demos.Synthesizer.NanoOptimization.Helpers;

namespace JJ.Demos.Synthesizer.NanoOptimization.Dto
{
    internal class Number_OperatorDto : OperatorDtoBase_WithoutInputOperatorDtos
    {
        public override string OperatorTypeName => OperatorNames.Number;
        public virtual double Number { get; set; }
    }

    internal class Number_OperatorDto_NaN : Number_OperatorDto
    {
        public override double Number => double.NaN;
    }

    internal class Number_OperatorDto_One : Number_OperatorDto
    {
        public override double Number => 1.0;
    }

    internal class Number_OperatorDto_Zero : Number_OperatorDto
    {
        public override double Number => 0.0;
    }
}
