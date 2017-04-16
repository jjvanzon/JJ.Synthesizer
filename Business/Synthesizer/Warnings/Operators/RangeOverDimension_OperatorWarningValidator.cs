using JJ.Data.Synthesizer.Entities;

namespace JJ.Business.Synthesizer.Warnings.Operators
{
    internal class RangeOverDimension_OperatorWarningValidator : OperatorWarningValidator_Base_AllInletsFilledInOrHaveDefaults
    {
        public RangeOverDimension_OperatorWarningValidator(Operator obj)
            : base(obj)
        { }
    }
}