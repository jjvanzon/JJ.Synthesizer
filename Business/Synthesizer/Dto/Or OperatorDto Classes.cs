using JJ.Business.Synthesizer.Enums;

namespace JJ.Business.Synthesizer.Dto
{
    internal class Or_OperatorDto : OperatorDtoBase_WithAAndB
    {
        public override OperatorTypeEnum OperatorTypeEnum => OperatorTypeEnum.Or;
    }

    internal class Or_OperatorDto_ConstA_ConstB : Or_OperatorDto
    { }

    internal class Or_OperatorDto_ConstA_VarB : Or_OperatorDto
    { }

    internal class Or_OperatorDto_VarA_ConstB : Or_OperatorDto
    { }

    internal class Or_OperatorDto_VarA_VarB : Or_OperatorDto
    { }
}