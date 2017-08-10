using JJ.Business.Synthesizer.Enums;

namespace JJ.Business.Synthesizer.Dto
{
    internal class Not_OperatorDto : OperatorDtoBase_WithNumber
    {
        public override OperatorTypeEnum OperatorTypeEnum => OperatorTypeEnum.Not;
    }

    internal class Not_OperatorDto_VarNumber : Not_OperatorDto
    { }

    internal class Not_OperatorDto_ConstNumber : Not_OperatorDto
    { }
}