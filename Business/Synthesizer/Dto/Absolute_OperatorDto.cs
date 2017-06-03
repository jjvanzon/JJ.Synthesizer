using JJ.Business.Synthesizer.Enums;

namespace JJ.Business.Synthesizer.Dto
{
    internal class Absolute_OperatorDto : Absolute_OperatorDto_VarNumber
    { }

    internal class Absolute_OperatorDto_VarNumber : OperatorDtoBase_VarNumber
    {
        public override OperatorTypeEnum OperatorTypeEnum => OperatorTypeEnum.Absolute;
    }

    internal class Absolute_OperatorDto_ConstNumber : OperatorDtoBase_ConstNumber
    {
        public override OperatorTypeEnum OperatorTypeEnum => OperatorTypeEnum.Absolute;
    }
}