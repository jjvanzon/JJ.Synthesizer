using JJ.Business.Synthesizer.Enums;

namespace JJ.Business.Synthesizer.Dto
{
    internal class Subtract_OperatorDto : OperatorDtoBase_WithAAndB
    {
        public override OperatorTypeEnum OperatorTypeEnum => OperatorTypeEnum.Subtract;
    }

    internal class Subtract_OperatorDto_ConstA_ConstB : Subtract_OperatorDto
    { }

    internal class Subtract_OperatorDto_ConstA_VarB : Subtract_OperatorDto
    { }

    internal class Subtract_OperatorDto_VarA_ConstB : Subtract_OperatorDto
    { }

    internal class Subtract_OperatorDto_VarA_VarB : Subtract_OperatorDto
    { }
}
