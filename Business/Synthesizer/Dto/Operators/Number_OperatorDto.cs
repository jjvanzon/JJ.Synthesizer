using JJ.Business.Synthesizer.Enums;

namespace JJ.Business.Synthesizer.Dto.Operators
{
    internal class Number_OperatorDto : OperatorDtoBase_WithoutInputs
    {
        public Number_OperatorDto()
        { }

        public Number_OperatorDto(double number) => Number = number;

        public override OperatorTypeEnum OperatorTypeEnum => OperatorTypeEnum.Number;

        public double Number { get; set; }
    }
}
