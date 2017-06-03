using JJ.Business.Synthesizer.Enums;

namespace JJ.Business.Synthesizer.Dto
{
    internal class OneOverX_OperatorDto : OneOverX_OperatorDto_VarNumber
    { }

    internal class OneOverX_OperatorDto_VarNumber : OperatorDtoBase_VarNumber
    {
        public override OperatorTypeEnum OperatorTypeEnum => OperatorTypeEnum.OneOverX;
    }

    internal class OneOverX_OperatorDto_ConstNumber : OperatorDtoBase_ConstNumber
    {
        public override OperatorTypeEnum OperatorTypeEnum => OperatorTypeEnum.OneOverX;
    }
}