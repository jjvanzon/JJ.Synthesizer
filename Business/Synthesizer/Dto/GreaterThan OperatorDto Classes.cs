using JJ.Business.Synthesizer.Enums;

namespace JJ.Business.Synthesizer.Dto
{
    internal class GreaterThan_OperatorDto : OperatorDtoBase_WithAAndB
    {
        public override OperatorTypeEnum OperatorTypeEnum => OperatorTypeEnum.GreaterThan;
    }

    internal class GreaterThan_OperatorDto_ConstA_ConstB : GreaterThan_OperatorDto
    { }

    internal class GreaterThan_OperatorDto_ConstA_VarB : GreaterThan_OperatorDto
    { }

    internal class GreaterThan_OperatorDto_VarA_ConstB : GreaterThan_OperatorDto
    { }

    internal class GreaterThan_OperatorDto_VarA_VarB : GreaterThan_OperatorDto
    { }
}