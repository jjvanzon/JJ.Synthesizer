using JJ.Business.Synthesizer.Enums;

namespace JJ.Business.Synthesizer.Dto.Operators
{
    internal class Or_OperatorDto : OperatorDtoBase_WithAAndB
    {
        public override OperatorTypeEnum OperatorTypeEnum => OperatorTypeEnum.Or;
    }
}