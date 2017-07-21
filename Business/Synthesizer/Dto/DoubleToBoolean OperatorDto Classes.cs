using JJ.Business.Synthesizer.Enums;

namespace JJ.Business.Synthesizer.Dto
{
    internal class DoubleToBoolean_OperatorDto : DoubleToBoolean_OperatorDto_VarNumber
    { }

    internal class DoubleToBoolean_OperatorDto_VarNumber : OperatorDtoBase_VarNumber
    {
        public override OperatorTypeEnum OperatorTypeEnum => OperatorTypeEnum.DoubleToBoolean;
    }

    internal class DoubleToBoolean_OperatorDto_ConstNumber : OperatorDtoBase_ConstNumber
    {
        public override OperatorTypeEnum OperatorTypeEnum => OperatorTypeEnum.DoubleToBoolean;
    }
}
