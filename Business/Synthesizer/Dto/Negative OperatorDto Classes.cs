using JJ.Business.Synthesizer.Enums;

namespace JJ.Business.Synthesizer.Dto
{
    internal class Negative_OperatorDto : Negative_OperatorDto_VarX
    { }

    internal class Negative_OperatorDto_VarX : OperatorDtoBase_VarX
    {
        public override string OperatorTypeName => nameof(OperatorTypeEnum.Negative);
    }

    internal class Negative_OperatorDto_ConstX : OperatorDtoBase_ConstX
    {
        public override string OperatorTypeName => nameof(OperatorTypeEnum.Negative);
    }
}