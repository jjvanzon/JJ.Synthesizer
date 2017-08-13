using JJ.Business.Synthesizer.Enums;

namespace JJ.Business.Synthesizer.Dto
{
    internal class Add_OperatorDto : OperatorDtoBase_InputsOnly
    {
        public override OperatorTypeEnum OperatorTypeEnum => OperatorTypeEnum.Add;
    }
}
