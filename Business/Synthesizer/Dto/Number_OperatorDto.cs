using JJ.Business.Synthesizer.Enums;

namespace JJ.Business.Synthesizer.Dto
{
    internal class Number_OperatorDto : OperatorDto
    {
        public override string OperatorTypeName => nameof(OperatorTypeEnum.Number);

        public double Number { get; set; }

        public Number_OperatorDto(double number)
            : base(new OperatorDto[0])
        {
            Number = number;
        }
    }
}
