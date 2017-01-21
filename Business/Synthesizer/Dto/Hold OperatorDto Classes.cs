using JJ.Business.Synthesizer.Enums;

namespace JJ.Business.Synthesizer.Dto
{
    internal class Hold_OperatorDto : OperatorDtoBase_VarSignal
    {
        public override OperatorTypeEnum OperatorTypeEnum => OperatorTypeEnum.Hold;
    }

    internal class Hold_OperatorDto_VarSignal : OperatorDtoBase_VarSignal
    {
        public override OperatorTypeEnum OperatorTypeEnum => OperatorTypeEnum.Hold;
    }

    internal class Hold_OperatorDto_ConstSignal : OperatorDtoBase_ConstSignal
    {
        public override OperatorTypeEnum OperatorTypeEnum => OperatorTypeEnum.Hold;
    }
}