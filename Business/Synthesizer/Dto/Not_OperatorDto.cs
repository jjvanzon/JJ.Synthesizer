using JJ.Business.Synthesizer.Enums;

namespace JJ.Business.Synthesizer.Dto
{
    internal class Not_OperatorDto : Not_OperatorDto_VarX
    { }

    internal class Not_OperatorDto_VarX : OperatorDtoBase_VarX
    {
        public override string OperatorTypeName => nameof(OperatorTypeEnum.Not);
    }

    internal class Not_OperatorDto_ConstX : OperatorDtoBase_ConstX
    {
        public override string OperatorTypeName => nameof(OperatorTypeEnum.Not);
    }
}