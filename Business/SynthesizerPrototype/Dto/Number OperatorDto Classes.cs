using JJ.Business.SynthesizerPrototype.Helpers;

namespace JJ.Business.SynthesizerPrototype.Dto
{
    public class Number_OperatorDto : OperatorDtoBase_WithoutInputOperatorDtos
    {
        public override string OperatorTypeName => nameof(OperatorTypeEnum.Number);
        public virtual double Number { get; set; }
    }

    public class Number_OperatorDto_NaN : Number_OperatorDto
    {
        public override double Number => double.NaN;
    }

    public class Number_OperatorDto_One : Number_OperatorDto
    {
        public override double Number => 1.0;
    }

    public class Number_OperatorDto_Zero : Number_OperatorDto
    {
        public override double Number => 0.0;
    }
}
