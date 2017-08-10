using JJ.Business.Synthesizer.Enums;

namespace JJ.Business.Synthesizer.Dto
{
    internal class NotEqual_OperatorDto : OperatorDtoBase_WithAAndB
    {
        public override OperatorTypeEnum OperatorTypeEnum => OperatorTypeEnum.NotEqual;
    }

    internal class NotEqual_OperatorDto_ConstA_ConstB : NotEqual_OperatorDto
    { }

    internal class NotEqual_OperatorDto_ConstA_VarB : NotEqual_OperatorDto
    { }

    internal class NotEqual_OperatorDto_VarA_ConstB : NotEqual_OperatorDto
    { }

    internal class NotEqual_OperatorDto_VarA_VarB : NotEqual_OperatorDto
    { }
}