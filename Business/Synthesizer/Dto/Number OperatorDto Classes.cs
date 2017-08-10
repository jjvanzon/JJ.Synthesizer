using JJ.Business.Synthesizer.Enums;

namespace JJ.Business.Synthesizer.Dto
{
    internal class Number_OperatorDto : OperatorDtoBase_WithoutInputs
    {
        public override OperatorTypeEnum OperatorTypeEnum => OperatorTypeEnum.Number;

        public virtual double Number { get; set; }
    }

    /// <summary> For Machine Optimization </summary>
    internal class Number_OperatorDto_NaN : Number_OperatorDto
    {
        public override OperatorTypeEnum OperatorTypeEnum => OperatorTypeEnum.Number;

        public override double Number => double.NaN;
    }

    /// <summary> For Machine Optimization </summary>
    internal class Number_OperatorDto_One : Number_OperatorDto
    {
        public override OperatorTypeEnum OperatorTypeEnum => OperatorTypeEnum.Number;

        public override double Number => 1.0;
    }

    /// <summary> For Machine Optimization </summary>
    internal class Number_OperatorDto_Zero : Number_OperatorDto
    {
        public override OperatorTypeEnum OperatorTypeEnum => OperatorTypeEnum.Number;

        public override double Number => 0.0;
    }
}
