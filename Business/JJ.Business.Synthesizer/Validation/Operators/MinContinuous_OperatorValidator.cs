using JJ.Data.Synthesizer;
using JJ.Business.Synthesizer.Enums;

namespace JJ.Business.Synthesizer.Validation.Operators
{
    internal class MinContinuous_OperatorValidator : OperatorValidator_Base_ContinuousAggregate
    {
        public MinContinuous_OperatorValidator(Operator obj)
            : base(obj, OperatorTypeEnum.MinContinuous)
        { }
    }
}
