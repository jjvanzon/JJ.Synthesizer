using JJ.Business.Synthesizer.Enums;
using JJ.Business.Synthesizer.Helpers;

namespace JJ.Business.Synthesizer.Dto.Operators
{
    internal class MinOverInlets_OperatorDto : OperatorDtoBase_InputsOnly, IOperatorDto_WithAggregateInfo
    {
        public override OperatorTypeEnum OperatorTypeEnum => OperatorTypeEnum.MinOverInlets;

        public AggregateInfo GetAggregateInfo() => AggregateInfoFactory.CreateAggregateInfo(Inputs);
    }
}
