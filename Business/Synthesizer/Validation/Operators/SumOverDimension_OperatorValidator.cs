using JJ.Business.Synthesizer.Enums;
using JJ.Data.Synthesizer.Entities;

namespace JJ.Business.Synthesizer.Validation.Operators
{
    internal class SumOverDimension_OperatorValidator : OperatorValidator_Base_AggregateOverDimension
    {
        public SumOverDimension_OperatorValidator(Operator obj)
            : base(obj, OperatorTypeEnum.SumOverDimension)
        { }
    }
}
