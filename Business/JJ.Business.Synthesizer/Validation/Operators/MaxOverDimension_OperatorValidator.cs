using JJ.Data.Synthesizer;
using JJ.Business.Synthesizer.Enums;

namespace JJ.Business.Synthesizer.Validation.Operators
{
    internal class MaxOverDimension_OperatorValidator : OperatorValidator_Base_AggregateOverDimension
    {
        public MaxOverDimension_OperatorValidator(Operator obj)
            : base(obj, OperatorTypeEnum.MaxOverDimension)
        { }
    }
}
