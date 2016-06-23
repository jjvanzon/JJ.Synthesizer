using JJ.Data.Synthesizer;
using JJ.Business.Synthesizer.Enums;

namespace JJ.Business.Synthesizer.Validation.Operators
{
    internal class SumContinuous_OperatorValidator : OperatorValidator_Base_ContinuousAggregate
    {
        public SumContinuous_OperatorValidator(Operator obj)
            : base(obj, OperatorTypeEnum.SumContinuous)
        { }
    }
}
