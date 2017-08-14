using JJ.Business.Synthesizer.Enums;

namespace JJ.Business.Synthesizer.Dto
{
    internal class MaxOverInlets_OperatorDto : OperatorDtoBase_InputsOnly
    {
        public override OperatorTypeEnum OperatorTypeEnum => OperatorTypeEnum.MaxOverInlets;
    }
}
