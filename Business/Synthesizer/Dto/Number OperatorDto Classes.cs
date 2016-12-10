using JJ.Business.Synthesizer.Enums;

namespace JJ.Business.Synthesizer.Dto
{
    internal class Number_OperatorDto : OperatorDtoBase_WithoutInputOperatorDtos
    {
        public override string OperatorTypeName => nameof(OperatorTypeEnum.Number);

        public virtual double Number { get; set; }
    }

    /// <summary> For Machine Optimization </summary>
    internal class Number_OperatorDto_NaN : Number_OperatorDto
    {
        public override string OperatorTypeName => nameof(OperatorTypeEnum.Number);

        public override double Number => double.NaN;
    }

    /// <summary> For Machine Optimization </summary>
    internal class Number_OperatorDto_One : Number_OperatorDto
    {
        public override string OperatorTypeName => nameof(OperatorTypeEnum.Number);

        public override double Number => 1.0;
    }

    /// <summary> For Machine Optimization </summary>
    internal class Number_OperatorDto_Zero : Number_OperatorDto
    {
        public override string OperatorTypeName => nameof(OperatorTypeEnum.Number);

        public override double Number => 0.0;
    }
}
