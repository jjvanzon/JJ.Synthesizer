using JJ.Business.Synthesizer.Enums;

namespace JJ.Business.Synthesizer.Dto
{
    internal class Hold_OperatorDto : OperatorDtoBase_WithSignal
    {
        public override OperatorTypeEnum OperatorTypeEnum => OperatorTypeEnum.Hold;
    }

    internal class Hold_OperatorDto_VarSignal : Hold_OperatorDto
    { }

    internal class Hold_OperatorDto_ConstSignal : Hold_OperatorDto
    { }
}