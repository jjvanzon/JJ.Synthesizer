using JJ.Business.Synthesizer.Enums;

namespace JJ.Business.Synthesizer.Dto.Operators
{
    internal class GreaterThanOrEqual_OperatorDto : OperatorDtoBase_WithAAndB
    {
        public override OperatorTypeEnum OperatorTypeEnum => OperatorTypeEnum.GreaterThanOrEqual;
    }
}