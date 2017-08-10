using JJ.Business.Synthesizer.Enums;

namespace JJ.Business.Synthesizer.Dto
{
    internal class LessThan_OperatorDto : OperatorDtoBase_WithAAndB
    {
        public override OperatorTypeEnum OperatorTypeEnum => OperatorTypeEnum.LessThan;
    }

    internal class LessThan_OperatorDto_ConstA_ConstB : LessThan_OperatorDto
    { }

    internal class LessThan_OperatorDto_ConstA_VarB : LessThan_OperatorDto
    { }

    internal class LessThan_OperatorDto_VarA_ConstB : LessThan_OperatorDto
    { }

    internal class LessThan_OperatorDto_VarA_VarB : LessThan_OperatorDto
    { }
}