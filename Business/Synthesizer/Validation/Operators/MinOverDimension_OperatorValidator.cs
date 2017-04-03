using JJ.Data.Synthesizer;
using JJ.Business.Synthesizer.Enums;
using JJ.Data.Synthesizer.Entities;

namespace JJ.Business.Synthesizer.Validation.Operators
{
    internal class MinOverDimension_OperatorValidator : OperatorValidator_Base_AggregateOverDimension
    {
        public MinOverDimension_OperatorValidator(Operator obj)
            : base(obj, OperatorTypeEnum.MinOverDimension)
        { }
    }
}
