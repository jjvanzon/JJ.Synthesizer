using JJ.Business.Synthesizer.Enums;

namespace JJ.Business.Synthesizer.Dto
{
    internal class MinOverInlets_OperatorDto : OperatorDtoBase_InputsOnly
    {
        public override OperatorTypeEnum OperatorTypeEnum => OperatorTypeEnum.MinOverInlets;
    }
}
