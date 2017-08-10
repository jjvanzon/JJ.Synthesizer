using JJ.Business.Synthesizer.Enums;

namespace JJ.Business.Synthesizer.Dto
{
    internal class LessThanOrEqual_OperatorDto : OperatorDtoBase_WithAAndB
    {
        public override OperatorTypeEnum OperatorTypeEnum => OperatorTypeEnum.LessThanOrEqual;
    }

    internal class LessThanOrEqual_OperatorDto_ConstA_ConstB : LessThanOrEqual_OperatorDto
    { }

    internal class LessThanOrEqual_OperatorDto_ConstA_VarB : LessThanOrEqual_OperatorDto
    { }

    internal class LessThanOrEqual_OperatorDto_VarA_ConstB : LessThanOrEqual_OperatorDto
    { }

    internal class LessThanOrEqual_OperatorDto_VarA_VarB : LessThanOrEqual_OperatorDto
    { }
}