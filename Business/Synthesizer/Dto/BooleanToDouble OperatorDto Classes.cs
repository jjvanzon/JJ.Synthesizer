using JJ.Business.Synthesizer.Enums;

namespace JJ.Business.Synthesizer.Dto
{
    internal class BooleanToDouble_OperatorDto : BooleanToDouble_OperatorDto_VarInput
    { }

    internal class BooleanToDouble_OperatorDto_VarInput : OperatorDtoBase_VarInput
    {
        public override OperatorTypeEnum OperatorTypeEnum => OperatorTypeEnum.BooleanToDouble;
    }

    internal class BooleanToDouble_OperatorDto_ConstInput : OperatorDtoBase_ConstInput
    {
        public override OperatorTypeEnum OperatorTypeEnum => OperatorTypeEnum.BooleanToDouble;
    }
}
