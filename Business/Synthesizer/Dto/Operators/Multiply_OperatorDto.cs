using JJ.Business.Synthesizer.Enums;
using JJ.Business.Synthesizer.Helpers;

namespace JJ.Business.Synthesizer.Dto.Operators
{
    internal class Multiply_OperatorDto : OperatorDtoBase_InputsOnly, IOperatorDto_WithAggregateInfo
    {
        public override OperatorTypeEnum OperatorTypeEnum => OperatorTypeEnum.Multiply;

        public AggregateInfo GetAggregateInfo() => AggregateInfoFactory.CreateAggregateInfo(Inputs);
    }
}
