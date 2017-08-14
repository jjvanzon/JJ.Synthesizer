using JJ.Business.Synthesizer.Enums;

namespace JJ.Business.Synthesizer.Dto
{
    internal class MaxOverInlets_OperatorDto : OperatorDtoBase_InputsOnly, IOperatorDto_WithAggregateInfo
    {
        public override OperatorTypeEnum OperatorTypeEnum => OperatorTypeEnum.MaxOverInlets;

        public AggregateInfo AggregateInfo { get; set; }
    }
}
