using JJ.Data.Synthesizer;
using JJ.Business.Synthesizer.Enums;

namespace JJ.Business.Synthesizer.Validation.Operators
{
    internal class MaxContinuous_OperatorValidator : OperatorValidator_Base_ContinuousAggregate
    {
        public MaxContinuous_OperatorValidator(Operator obj)
            : base(obj, OperatorTypeEnum.MaxContinuous)
        { }
    }
}
