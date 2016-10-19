using JJ.Data.Synthesizer;
using JJ.Business.Synthesizer.Enums;

namespace JJ.Business.Synthesizer.Validation.Operators
{
    internal class AverageOverDimension_OperatorValidator : OperatorValidator_Base_AggregateOverDimension
    {
        public AverageOverDimension_OperatorValidator(Operator obj)
            : base(obj, OperatorTypeEnum.AverageOverDimension)
        { }
    }
}
