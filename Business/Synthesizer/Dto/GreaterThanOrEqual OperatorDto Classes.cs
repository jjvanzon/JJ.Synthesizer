using JJ.Business.Synthesizer.Enums;

namespace JJ.Business.Synthesizer.Dto
{
    internal class GreaterThanOrEqual_OperatorDto : OperatorDtoBase_WithAAndB
    {
        public override OperatorTypeEnum OperatorTypeEnum => OperatorTypeEnum.GreaterThanOrEqual;
    }

    internal class GreaterThanOrEqual_OperatorDto_ConstA_ConstB : GreaterThanOrEqual_OperatorDto
    { }

    internal class GreaterThanOrEqual_OperatorDto_ConstA_VarB : GreaterThanOrEqual_OperatorDto
    { }

    internal class GreaterThanOrEqual_OperatorDto_VarA_ConstB : GreaterThanOrEqual_OperatorDto
    { }

    internal class GreaterThanOrEqual_OperatorDto_VarA_VarB : GreaterThanOrEqual_OperatorDto
    { }
}