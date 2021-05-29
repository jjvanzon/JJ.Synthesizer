using JJ.Business.Synthesizer.Enums;
using JJ.Business.Synthesizer.Helpers;

namespace JJ.Business.Synthesizer.Dto.Operators
{
    internal class AverageOverInlets_OperatorDto : OperatorDtoBase_InputsOnly, IOperatorDto_WithAggregateInfo
    {
        public override OperatorTypeEnum OperatorTypeEnum => OperatorTypeEnum.AverageOverInlets;

        public AggregateInfo GetAggregateInfo() => AggregateInfoFactory.CreateAggregateInfo(Inputs);
    }
}
