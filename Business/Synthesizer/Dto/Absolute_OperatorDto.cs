using JJ.Business.Synthesizer.Enums;

namespace JJ.Business.Synthesizer.Dto
{
    internal class Absolute_OperatorDto : Absolute_OperatorDto_VarX
    { }

    internal class Absolute_OperatorDto_VarX : OperatorDtoBase_VarX
    {
        public override string OperatorTypeName => nameof(OperatorTypeEnum.Absolute);
    }

    internal class Absolute_OperatorDto_ConstX : OperatorDtoBase_ConstX
    {
        public override string OperatorTypeName => nameof(OperatorTypeEnum.Absolute);
    }
}