using JJ.Business.Synthesizer.Enums;
using JJ.Data.Synthesizer.Entities;

namespace JJ.Business.Synthesizer.Validation.Operators
{
    internal class MaxOverDimension_OperatorValidator : OperatorValidator_Base_AggregateOverDimension
    {
        public MaxOverDimension_OperatorValidator(Operator obj)
            : base(obj, OperatorTypeEnum.MaxOverDimension)
        { }
    }
}
