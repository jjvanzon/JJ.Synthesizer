using JJ.Business.Synthesizer.Enums;

namespace JJ.Business.Synthesizer.Dto
{
    internal class Equal_OperatorDto : OperatorDtoBase_WithAAndB
    {
        public override OperatorTypeEnum OperatorTypeEnum => OperatorTypeEnum.Equal;
    }

    internal class Equal_OperatorDto_ConstA_ConstB : Equal_OperatorDto
    { }

    internal class Equal_OperatorDto_ConstA_VarB : Equal_OperatorDto
    { }

    internal class Equal_OperatorDto_VarA_ConstB : Equal_OperatorDto
    { }

    internal class Equal_OperatorDto_VarA_VarB : Equal_OperatorDto
    { }
}