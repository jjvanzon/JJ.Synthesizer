using JJ.Data.Synthesizer;
using JJ.Business.Synthesizer.Enums;

namespace JJ.Business.Synthesizer.Validation.Operators
{
    internal class AverageContinuous_OperatorValidator : OperatorValidator_Base_ContinuousAggregate
    {
        public AverageContinuous_OperatorValidator(Operator obj)
            : base(obj, OperatorTypeEnum.AverageContinuous)
        { }
    }
}
