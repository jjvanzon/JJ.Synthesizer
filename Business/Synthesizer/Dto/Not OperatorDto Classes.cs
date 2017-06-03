using JJ.Business.Synthesizer.Enums;

namespace JJ.Business.Synthesizer.Dto
{
    internal class Not_OperatorDto : Not_OperatorDto_VarNumber
    { }

    internal class Not_OperatorDto_VarNumber : OperatorDtoBase_VarNumber
    {
        public override OperatorTypeEnum OperatorTypeEnum => OperatorTypeEnum.Not;
    }

    internal class Not_OperatorDto_ConstNumber : OperatorDtoBase_ConstNumber
    {
        public override OperatorTypeEnum OperatorTypeEnum => OperatorTypeEnum.Not;
    }
}