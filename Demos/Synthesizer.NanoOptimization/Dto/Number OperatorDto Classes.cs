using JJ.Demos.Synthesizer.NanoOptimization.Helpers;

namespace JJ.Demos.Synthesizer.NanoOptimization.Dto
{
    internal class Number_OperatorDto : OperatorDtoBase
    {
        public double Number { get; set; }
        public override string OperatorTypeName => OperatorNames.Number;

        public Number_OperatorDto(double number)
            : base(new OperatorDtoBase[0])
        {
            Number = number;
        }
    }

    internal class Number_OperatorDto_NaN : Number_OperatorDto
    {
        public override string OperatorTypeName => OperatorNames.Number;

        public Number_OperatorDto_NaN()
            : base(double.NaN)
        { }
    }

    internal class Number_OperatorDto_One : Number_OperatorDto
    {
        public override string OperatorTypeName => OperatorNames.Number;

        public Number_OperatorDto_One()
            : base(1.0)
        { }
    }

    internal class Number_OperatorDto_Zero : Number_OperatorDto
    {
        public override string OperatorTypeName => OperatorNames.Number;

        public Number_OperatorDto_Zero()
            : base(0.0)
        { }
    }
}
