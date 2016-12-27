using JJ.Business.SynthesizerPrototype.Tests.Helpers;

namespace JJ.Business.SynthesizerPrototype.Tests.Dto
{
    internal class Number_OperatorDto : OperatorDtoBase_WithoutInputOperatorDtos
    {
        public override string OperatorTypeName => nameof(OperatorTypeEnum.Number);
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
