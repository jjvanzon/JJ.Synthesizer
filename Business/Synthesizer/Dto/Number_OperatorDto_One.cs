using JJ.Business.Synthesizer.Enums;

namespace JJ.Business.Synthesizer.Dto
{
    internal class Number_OperatorDto_One : Number_OperatorDto
    {
        public override string OperatorTypeName => nameof(OperatorTypeEnum.Number);

        public Number_OperatorDto_One()
            : base(1.0)
        { }
    }
}
