using JJ.Business.Synthesizer.Enums;

namespace JJ.Business.Synthesizer.Dto
{
    internal class Negative_OperatorDto : Negative_OperatorDto_VarNumber
    { }

    internal class Negative_OperatorDto_VarNumber : OperatorDtoBase_VarNumber
    {
        public override OperatorTypeEnum OperatorTypeEnum => OperatorTypeEnum.Negative;
    }

    internal class Negative_OperatorDto_ConstNumber : OperatorDtoBase_ConstNumber
    {
        public override OperatorTypeEnum OperatorTypeEnum => OperatorTypeEnum.Negative;
    }
}